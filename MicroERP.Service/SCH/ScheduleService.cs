using Google.Protobuf.WellKnownTypes;
using MicroERP.Core.Entities.SCH;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.ATT;
using MicroERP.DAL.CTL;
using MicroERP.DAL.SCH;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Service.SCH
{
    public class ScheduleService
    {
        #region Class Variables
        private ScheduleDAL _schDAL;
        private ScheduleDayEventDAL _schDayEventDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        private ScheduleDayEventService _schDayEvntSvc;
        private AttendanceDAL _attDAL;
        #endregion
        #region Constructor
        public ScheduleService()
        {
            _schDAL = new ScheduleDAL();
            _attDAL = new AttendanceDAL();
            _schDayEventDAL = new ScheduleDayEventDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
            _schDayEvntSvc = new ScheduleDayEventService();
        }
        #endregion
        #region  Schedule
        public ScheduleDE ManageSchedule(ScheduleDE mod)
        {
            bool retVal = false;
            string message = "";
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);
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
                {
                    mod.Id = _corDAL.GetnextId(TableNames.Schedule.ToString());
                    retVal = _schDAL.ManageSchedule(mod, cmd);
                }
                int schDayId = 0;
                schDayId = _corDAL.GetnextId(TableNames.scheduleday.ToString());
                var schedules = SearchSchedule(new ScheduleDE { Id = mod.Id });
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
                            _schDAL.ManageScheduleDay(schDay, cmd);
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
                        SchLine.DBoperation = DBoperations.Insert;
                        SchLine.IsActive = true;
                        retVal = _schDAL.ManageScheduleDay(SchLine, cmd);
                        schDayId += 1;
                    }
                }
                if (retVal == true)
                {
                    if (mod.DBoperation == DBoperations.Insert)
                        message = "Schedule Successfully Added";
                    else if (mod.DBoperation == DBoperations.Update)
                        message = "Schedule Successfully Updated";
                    mod.HasErrors = false;
                    _logger.Info(message);
                }
                else
                {
                    if (mod.DBoperation == DBoperations.Insert)
                        message = "Error Occurred while Saving the Schedule";
                    else if (mod.DBoperation == DBoperations.Update)
                        message = "Error Occurred while Updating the Schedule";
                    mod.HasErrors = true;
                    _logger.Error(message);
                }
                mod.ResponseMessage = message;
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
            bool retVal = false;
            string message = "";
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);
                if (mod.DBoperation == DBoperations.Insert)
                {
                    mod.Id = _corDAL.GetnextId(TableNames.Schedule.ToString());
                    retVal = _schDAL.ManageSchedule(mod, cmd);
                }
                int schDayId = 0;
                int dayEvtId = 0;
                schDayId = _corDAL.GetnextId(TableNames.scheduleday.ToString());
                dayEvtId = _corDAL.GetnextId(TableNames.ScheduleDayEvent.ToString());
                foreach (var day in mod.ScheduleDays)
                {
                    var SchLine = new ScheduleDayDE();
                    SchLine.Id = schDayId;
                    SchLine.DayId = day.DayId;
                    SchLine.SchId = mod.Id;
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
                        evt.StartTime = schEvt.StartTime;
                        evt.EndTime = schEvt.EndTime;
                        evt.IsActive = true;
                        evt.DBoperation = DBoperations.Insert;
                        retVal = _schDayEventDAL.ManageScheduleDayEvent(evt, cmd);
                        dayEvtId += 1;
                    }
                }
                if (retVal == true)
                {
                    if (mod.DBoperation == DBoperations.Insert)
                        message = "Schedule Successfully Added";
                    else if (mod.DBoperation == DBoperations.Update)
                        message = "Schedule Successfully Updated";
                    mod.HasErrors = false;
                    _logger.Info(message);
                }
                else
                {
                    if (mod.DBoperation == DBoperations.Insert)
                        message = "Error Occurred while Saving the Schedule";
                    else if (mod.DBoperation == DBoperations.Update)
                        message = "Error Occurred while Updating the Schedule";
                    mod.HasErrors = true;
                    _logger.Error(message);
                }
                mod.ResponseMessage = message;
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
            List<ScheduleDE> list = new List<ScheduleDE>();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
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
                MicroERPDataContext.CancelTransaction(cmd);
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
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                if (schDay.DBoperation == DBoperations.Insert)
                    schDay.Id = _corDAL.GetnextId(TableNames.scheduleday.ToString());
                if (schDay.DBoperation == DBoperations.Delete)
                {
                    var schDayEvents = _schDayEvntSvc.GetScheduleDayEvents(schDay.Id);
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
        public ScheduleDE GetScheduleByUserId(string userId)
        {
            ScheduleDE sch = new ScheduleDE();
            sch.HasErrors = true;
            try
            {
                string whereClause = " Where 1=1";
                if (!string.IsNullOrWhiteSpace(userId))
                    whereClause += $" AND UserId=\"{userId}\" AND IsActive ={true}";
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
            finally
            {
                // Close the database connection if necessary
            }
            sch.UserId = userId;
            return sch;
        }
        public ScheduleDE GetSchWithDays(ScheduleDE sch)
        {
            string whereClause = "where 1=1";
            sch.ScheduleDays = _schDAL.SearchScheduleDay(whereClause += $" AND SchId={sch.Id} AND IsActive ={true}");
            if (sch.ScheduleDays != null && sch.ScheduleDays.Count > 0)
            {
                foreach (var schDay in sch.ScheduleDays)
                {
                    if (schDay.DayId > 0)
                    {
                        sch.DayIds.Add((int)schDay.DayId);
                        whereClause = "where 1=1";
                        schDay.ScheduleDayEvents = _schDAL.SearchScheduleDayEvent(whereClause += $" AND SchDayId={schDay.Id} AND IsActive ={true}");
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
                            string eventString = $"{schDayEvent.StartTime} - {schDayEvent.EndTime} ({schDayEvent.Sp} Sp's) {schDayEvent.EventType} {schDayEvent.Location}";
                            if (schDayEvent != schDay.ScheduleDayEvents.Last())
                                eventString += " , ";
                            schDay.SchDayEvents += eventString;
                        }
                    }
                }
            }
            return sch;
        }
        public ScheduleDayDE GetSchDayByUserId(string userId, DateTime date)
        {
            ScheduleDayDE schDay = new ScheduleDayDE();
            ScheduleDE sch = new ScheduleDE();
            try
            {
                var list = SearchSchedule(new ScheduleDE { UserId = userId, IsActive = true });

                if (list != null && list.Count > 0)
                    sch = list.LastOrDefault();
                string whereClause = "where 1=1";
                var days = _schDAL.SearchScheduleDay(whereClause += $" AND SchId={sch.Id} " +
                  $" and DAY =  ''" + date.DayOfWeek + "''");

                if (days != null && days.Count > 0)
                { schDay = days[0]; }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {

            }
            return schDay;
        }
        public List<ScheduleDayEventDE> GetDayEvents(int? SchDayId)
        {
            try
            {
                string whereClause = "where 1=1";
                return _schDAL.SearchScheduleDayEvent(whereClause += $" AND SchDayId={SchDayId} ");
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
            var att = _attDAL.SearchAttendance($"where SchId={sch.Id}");
            if (att != null && att.Count > 0)
                inUse = true;
            return inUse;
        }
        public ScheduleDE UpdateSchedule(ScheduleDE Schedule)
        {
            MySqlCommand? cmd = null;
            var sch = new ScheduleDE();
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
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
            return sch;
        }
        #endregion
    }
}