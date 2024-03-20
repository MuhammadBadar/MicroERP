using Google.Protobuf.WellKnownTypes;
using QST.MicroERP.Core.Entities.SCH;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.ATT;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL.SCH;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;
using System.Data;
using QST.MicroERP.Core.Extensions;

namespace QST.MicroERP.Service.SCH
{
    public class ScheduleService:BaseService
    {
        #region Class Variables
        private ScheduleDAL _schDAL;
        private ScheduleDayEventDAL _schDayEventDAL;
        private ScheduleDayEventService _schDayEvntSvc;
        private AttendanceDAL _attDAL;
        #endregion
        #region Constructor
        public ScheduleService()
        {
            _schDAL = new ScheduleDAL();
            _attDAL = new AttendanceDAL();
            _schDayEventDAL = new ScheduleDayEventDAL();
            _schDayEvntSvc = new ScheduleDayEventService();
        }
        #endregion
        #region  Schedule
        public ScheduleDE ManageSchedule(ScheduleDE mod)
        {
            bool closeConnectionFlag = false;
            bool retVal = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                MicroERPDataContext.StartTransaction(cmd);
                _entity = TableNames.SCH_Schedule.ToString (); 

                if (mod.DBoperation == DBoperations.Update)
                {
                    var inUse = IsScheInUse(mod);
                    if (inUse)
                    {
                        mod.IsActive = false;
                        retVal = _schDAL.ManageSchedule(mod, cmd);
                        mod.IsActive = true;
                        mod.DBoperation = DBoperations.Insert;
                    }
                    else
                        mod.DBoperation = DBoperations.Update;
                }
                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");
                int schDayId = 0;
                schDayId = _coreDAL.GetNextIdByClient (TableNames.SCH_ScheduleDay.ToString(), mod.ClientId, "ClientId");
                var schedules = SearchSchedule(new ScheduleDE { Id = mod.Id,ClientId=mod.ClientId });
                var sch = new ScheduleDE();
                if (schedules != null && schedules.Count > 0) { sch = schedules[0]; }

                if (mod.DBoperation == DBoperations.Update)
                {
                    foreach (var schDay in sch.ScheduleDays)
                    {
                        bool shouldDelete = true;
                        foreach (var scheduleDayId in mod.DayIds)
                        {
                            if (schDay.DayId == scheduleDayId)
                                shouldDelete = false;
                        }
                        if (shouldDelete)
                        {
                            schDay.DBoperation = DBoperations.Delete;
                            retVal = _schDAL.ManageScheduleDay(schDay, cmd);
                        }
                    }
                }
                foreach (var day in mod.DayIds)
                {
                    bool shouldAdd = true;
                    foreach (var schDay in sch.ScheduleDays)
                    {
                        if (day == schDay.DayId)
                            shouldAdd = false;
                    }
                    if (shouldAdd)
                    {
                        var SchLine = new ScheduleDayDE();
                        SchLine.Id = schDayId;
                        SchLine.DayId = day;
                        SchLine.SchId = mod.Id;
                        SchLine.ClientId = mod.ClientId;
                        SchLine.DBoperation = DBoperations.Insert;
                        SchLine.IsActive = true;
                        retVal = _schDAL.ManageScheduleDay(SchLine, cmd);
                        schDayId += 1;
                    }
                }
                retVal = _schDAL.ManageSchedule (mod, cmd);
                if (retVal)
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    mod.DayIds.Clear();
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                }
                MicroERPDataContext.EndTransaction(cmd);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
                mod = GetSchWithDays(mod);
            }
            return mod;
        }
        public ScheduleDE CopySchedule(ScheduleDE mod)
        {
            bool closeConnectionFlag = false;
            bool retVal = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                MicroERPDataContext.StartTransaction(cmd);
                _entity=TableNames.SCH_Schedule.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                {
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");
                    retVal = _schDAL.ManageSchedule(mod, cmd);
                }
                int schDayId = 0;
                int dayEvtId = 0;
                schDayId = _coreDAL.GetNextIdByClient (TableNames.SCH_ScheduleDay.ToString (), mod.ClientId, "ClientId");
                dayEvtId = _coreDAL.GetNextIdByClient (TableNames.SCH_ScheduleDayEvent.ToString (), mod.ClientId, "ClientId");
                foreach (var day in mod.ScheduleDays)
                {
                    var SchLine = new ScheduleDayDE();
                    SchLine.Id = schDayId;
                    SchLine.DayId = day.DayId;
                    SchLine.SchId = mod.Id;
                    SchLine.ClientId = mod.ClientId;
                    SchLine.DBoperation = DBoperations.Insert;
                    SchLine.IsActive = true;
                    retVal = _schDAL.ManageScheduleDay(SchLine, cmd);
                    schDayId += 1;
                    foreach (var schEvt in day.ScheduleDayEvents)
                    {
                        var evt = new ScheduleDayEventDE();
                        evt.Id = dayEvtId;
                        evt.SchId = mod.Id;
                        evt.SchDayId = schDayId;
                        evt.ClientId= mod.ClientId;
                        evt.StartTime = schEvt.StartTime;
                        evt.EndTime = schEvt.EndTime;
                        evt.IsActive = true;
                        evt.DBoperation = DBoperations.Insert;
                        retVal = _schDayEventDAL.ManageScheduleDayEvent(evt, cmd);
                        dayEvtId += 1;
                    }
                }
                if (retVal)
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                }
                MicroERPDataContext.EndTransaction(cmd);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
                mod = GetSchWithDays(mod);
            }
            return mod;
        }
        public List<ScheduleDE> SearchSchedule(ScheduleDE mod)
        {
            bool closeConnectionFlag = false;
            List<ScheduleDE> list = new List<ScheduleDE>();

            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.UserId != default && mod.UserId != null)
                    whereClause += $" AND UserId=\"{mod.UserId}\"";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                list = _schDAL.SearchSchedule(whereClause);
                foreach (ScheduleDE line in list)
                {
                    line.ScheduleDays = GetSchWithDays(line).ScheduleDays;
                }

                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error ("Error:" + exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return list;
        }
        public bool ManageScheduleDay(ScheduleDayDE schDay)
        {
            bool closeConnectionFlag = false;
            bool retVal = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                if (schDay.DBoperation == DBoperations.Insert)
                    schDay.Id = _coreDAL.GetNextIdByClient (TableNames.SCH_ScheduleDay.ToString (), schDay.ClientId, "ClientId");

                if (schDay.DBoperation == DBoperations.Delete)
                {
                    var schDayEvents = _schDayEvntSvc.GetScheduleDayEvents(schDay.Id, schDay.ClientId);
                    foreach (var schDayEvent in schDayEvents)
                    {
                        schDayEvent.DBoperation = DBoperations.Delete;
                        _schDayEventDAL.ManageScheduleDayEvent(schDayEvent);
                    }
                }
                retVal = _schDAL.ManageScheduleDay(schDay, cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public ScheduleDE GetScheduleByUserId(string userId, int ClientId)
        {
            ScheduleDE sch = new ScheduleDE();
            sch.HasErrors = true;
            try
            {
                string whereClause = " Where 1=1";
                if (!string.IsNullOrWhiteSpace(userId))
                    whereClause += $" AND UserId=\"{userId}\" and ClientId={ClientId} AND IsActive ={true}";
                var list = _schDAL.SearchSchedule(whereClause);
                if (list.Count > 0)
                    sch = list.LastOrDefault();
                sch = GetSchWithDays(sch);
                sch.HasErrors = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            sch.UserId = userId;
            return sch;
        }
        public ScheduleDE GetSchWithDays(ScheduleDE sch)
        {
            string whereClause = "where 1=1";
            sch.ScheduleDays = _schDAL.SearchScheduleDay(whereClause += $" AND SchId={sch.Id} and ClientId="+sch.ClientId+" AND IsActive =true");
            if (sch.ScheduleDays != null && sch.ScheduleDays.Count > 0)
            {
                foreach (var schDay in sch.ScheduleDays)
                {
                    if (schDay.DayId > 0)
                    {
                        sch.DayIds.Add((int)schDay.DayId);
                        whereClause = "where 1=1";
                        schDay.ScheduleDayEvents = _schDAL.SearchScheduleDayEvent(whereClause += $" AND SchDayId={schDay.Id} and ClientId="+sch.ClientId+" AND IsActive =true");
                        foreach (var schDayEvent in schDay.ScheduleDayEvents)
                        {
                            schDay.Location = schDayEvent.Location;
                            schDay.EventType = schDayEvent.EventType;
                            if (!string.IsNullOrEmpty(schDayEvent.StartTime) && !string.IsNullOrEmpty(schDayEvent.EndTime))
                            {
                                if (DateTime.TryParse(schDayEvent.StartTime, out DateTime startTime) && DateTime.TryParse(schDayEvent.EndTime, out DateTime endTime))
                                {
                                    TimeSpan timeDifference = endTime - startTime;
                                    schDayEvent.Sp = Math.Round(timeDifference.TotalHours, 2);
                                }
                            }
                            string eventString = $"{schDayEvent.StartTime} - {schDayEvent.EndTime} ({schDayEvent.Sp} Hr) {schDayEvent.EventType} {schDayEvent.Location}";
                            if (schDayEvent != schDay.ScheduleDayEvents.Last())
                                eventString += " , ";
                            schDay.SchDayEvents += eventString;
                        }
                    }
                    if (schDay.WorkTime != null && schDay.WorkTime != "")
                        schDay.SchDayType = (int)ScheduleDayTypes.FlexibleHoursSchDay;
                    else if(schDay.ScheduleDayEvents.Count>0)
                        schDay.SchDayType = (int)ScheduleDayTypes.FixedHoursSchDay;
                }
            }
            return sch;
        }
        public ScheduleDayDE GetSchDayByUserId(string userId,int clientId, DateTime date)
        {
            ScheduleDayDE schDay = new ScheduleDayDE();
            ScheduleDE sch = new ScheduleDE();
            try
            {
                var list = SearchSchedule(new ScheduleDE { UserId = userId, IsActive = true, ClientId=clientId });

                if (list != null && list.Count > 0)
                    sch = list.LastOrDefault();
                string whereClause = "where 1=1";
                var days = _schDAL.SearchScheduleDay(whereClause += $" AND SchId={sch.Id} and ClientId={clientId} " +
                  $" and DAY =  ''" + date.DayOfWeek + "''");

                if (days != null && days.Count > 0)
                { schDay = days[0]; }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            return schDay;
        }
        public List<ScheduleDayEventDE> GetDayEvents(int? SchDayId, int ClientId)
        {
            try
            {
                string whereClause = "where 1=1";
                return _schDAL.SearchScheduleDayEvent(whereClause += $" AND SchDayId={SchDayId} and ClientId=" + ClientId + " ");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
        public bool IsScheInUse(ScheduleDE sch)
        {
            bool inUse = false;
            var att = _attDAL.SearchAttendance($"where SchId={sch.Id} and ClientId={sch.ClientId}");
            if (att != null && att.Count > 0)
                inUse = true;
            return inUse;
        }
        public ScheduleDE UpdateSchedule(ScheduleDE Schedule)
        {
            bool closeConnectionFlag = false;
            var sch = new ScheduleDE();
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                var inUse = IsScheInUse(Schedule);
                if (inUse)
                {
                    Schedule.DBoperation = DBoperations.DeActivate;
                    _schDAL.ManageSchedule(Schedule, cmd);
                    Schedule.DBoperation = DBoperations.Insert;
                    sch = ManageSchedule(Schedule);
                }
                else
                {
                    Schedule.DBoperation = DBoperations.Update;
                    sch = ManageSchedule(Schedule);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return sch;
        }
        public double GetDueSPs ( string user,int clientId, DateTime inputDate )
        {
            double dueSPs = 0.00;
            string schTimes = GetScheduleTime (user, clientId, inputDate);
            var timeIntervals = schTimes.Split (',');
            foreach (var timeInterval in timeIntervals)
            {
                var times = timeInterval.Split ("-");
                if (times.Length > 1)
                {
                    {
                        var timeParts = times[0].Split (':');
                        TimeSpan startTime = new TimeSpan (Convert.ToInt32 (timeParts[0]), Convert.ToInt32 (timeParts[1]), 0);

                        timeParts = times[1].Split (':');
                        TimeSpan endTime = new TimeSpan (Convert.ToInt32 (timeParts[0]), Convert.ToInt32 (timeParts[1]), 0);

                        dueSPs += (endTime - startTime).TotalHours;
                    }
                }
                else if (times != null && times.Length > 0 && times[0]!="")
                    return Double.Parse( times[0]);
            }
            return dueSPs;
        }
        public string GetScheduleTime ( string userId,int clientId, DateTime inputDate )
        {
            string dayEvents = string.Empty;
            var userSchedule = GetScheduleByUserId (userId, clientId);
            int dayOfWeekId = (int)inputDate.DayOfWeek;
            int dayId = -1;
            switch (dayOfWeekId)
            {
                // Map days of the week to specific IDs
                case 0: dayId = (int)WeekDays.Sunday; break; // Sunday
                case 1: dayId = (int)WeekDays.Monday; break; // Monday
                case 2: dayId = (int)WeekDays.Tuesday; break; // Tuesday
                case 3: dayId = (int)WeekDays.Wednesday; break; // Wednesday
                case 4: dayId = (int)WeekDays.Thursday; break; // Thursday
                case 5: dayId = (int)WeekDays.Friday; break; // Friday
                case 6: dayId = (int)WeekDays.Saturday; break; // Saturday
            }
            var daySchedule = userSchedule.ScheduleDays.Where (m => m.DayId == dayId).FirstOrDefault ();
            if (daySchedule != null)
            {
                if(daySchedule.WorkTime!=null && daySchedule.WorkTime!="")
                    return daySchedule.WorkTime;
                foreach (var eventDetail in daySchedule.ScheduleDayEvents)
                {
                    dayEvents += eventDetail.StartTime + " - " + eventDetail.EndTime + " ,";
                }
                if (!string.IsNullOrEmpty (dayEvents))
                    dayEvents = dayEvents.TrimEnd (',', ' '); 
            }
            return dayEvents;
        }
        public (string, string) GetScheduleandDueTimeStr (string userId, int dayId, int clientId, DateTime date )
        {
            string schTime = String.Empty;
            string dueTime = String.Empty;
            if (dayId > 0)
            {
                var schEvents = GetDayEvents (dayId, clientId);
                if (schEvents != null && schEvents.Count > 0)
                {
                    TimeSpan dueSps = new TimeSpan ();
                    foreach (var schEvt in schEvents)
                    {
                        #region ScheduleTime 
                        schTime += schEvt.StartTime.ToString () + " - " + schEvt.EndTime.ToString ();
                        if (schEvt != schEvents.Last ())
                            schTime += ",";
                        schTime += Environment.NewLine;
                        #endregion
                        #region DueTime
                        dueSps += (TimeSpan.Parse (schEvt.EndTime)) - (TimeSpan.Parse (schEvt.StartTime));
                        dueTime = dueSps.TotalHours.ToHHMMSS();
                        #endregion
                    }
                }
                else
                {
                    var schDay = GetSchDayByUserId (userId, clientId, date);
                    if (schDay != null)
                        dueTime = Double.Parse((schDay.WorkTime?? String.Empty)).ToHHMMSS();
                }
            }
            return (schTime, dueTime);
        }
        #endregion
    }
}