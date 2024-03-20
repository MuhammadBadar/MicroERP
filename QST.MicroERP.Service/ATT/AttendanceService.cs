using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.SearchCriteria;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.DAL;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using QST.MicroERP.Core.Extensions;
using NLog;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;
using QST.MicroERP.Service.CLT;
using QST.MicroERP.Service.SCH;
using QST.MicroERP.DAL.ATT;
using QST.MicroERP.DAL.TMS;
using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.Core.Entities.ATT;
using QST.MicroERP.Core.Entities.SCH;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Service.SEC;

namespace QST.MicroERP.Service.ATT
{
    public class AttendanceService:BaseService
    {
        #region Class Members/Class Variables

        private AttendanceDAL _attDAL;
        private ScheduleService _schSvc;
        private LogEventService _logEventSvc;
        private UserTaskDAL _uTaskDAL;
        private UserService _userSvc;

        #endregion
        #region Constructors
        public AttendanceService()
        {
            _logEventSvc = new LogEventService();
            _attDAL = new AttendanceDAL();
            _schSvc = new ScheduleService();
            _uTaskDAL = new UserTaskDAL();
            _userSvc = new UserService ();
        }
        #endregion
        public AttendanceDE ManageAttendance(AttendanceDE mod, MySqlCommand? cmd =null)
        {
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                _entity = TableNames.ATT_Attendance.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");

                _logger.Info ($"Going to Call:_attDAL.ManageAttendance(mod, cmd)");
                if (_attDAL.ManageAttendance (mod, cmd))
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                }
                return mod;
            }
             catch (Exception ex)
            {
                mod.Id = 0;
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (cmd!=null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<AttendanceDE> SearchAttendance(AttendanceDE mod)
        {
            List<AttendanceDE> att = new List<AttendanceDE>();
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();

                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.Date.HasValue)
                    whereClause += $" AND DATE(Date) = ''{mod.Date.Value.ToString("yyyy-MM-dd")}''";
                if (mod.FromDate.HasValue)
                    whereClause += $" and Date >= ''{mod.FromDate.Value:yyyy-MM-dd} 00:00:00''";
                if (mod.ToDate.HasValue)
                    whereClause += $" and Date <= ''{mod.ToDate.Value:yyyy-MM-dd} 23:59:59''";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                if (mod.IncludeSubordinatesData && mod.UserId != default)
                {
                    var user = new UserDE ();
                    user.Id = mod.UserId;
                    user.ClientId = mod.ClientId;
                    var subordinateUsers = _userSvc.GetSubordinates (user);
                    if (subordinateUsers.Count > 0)
                    {
                        string subordinateIds = string.Join ("'',''", subordinateUsers.Select (x => x.Id));
                        whereClause += $" and (UserId like ''" + mod.UserId + "'' or UserId IN (''" + subordinateIds + "''))";
                    }
                    else
                    {
                        if (mod.UserId != default && mod.UserId != "")
                            whereClause += $" and UserId like ''" + mod.UserId + "''";
                    }
                }
                else
                {
                    if (mod.UserId != default && mod.UserId != "")
                        whereClause += $" and UserId like ''" + mod.UserId + "''";
                }
                att = _attDAL.SearchAttendance(whereClause, cmd);
                return att;
                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error(exp);
                throw;
            }
            finally
            {
                if (cmd!=null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<AttendanceDE> GetAttendanceReport ( AttendanceDE mod )
        {
            List<AttendanceDE> retVals = new List<AttendanceDE> ();
            retVals = SearchAttendance (new AttendanceDE { FromDate = mod.FromDate, ToDate = mod.ToDate, UserId = mod.UserId, IncludeSubordinatesData = mod.IncludeSubordinatesData, ClientId=mod.ClientId });
            foreach (var att in retVals)
            {
                List<ScheduleDayEventDE> schEvents = new List<ScheduleDayEventDE> ();
                List<UserTaskDE> userTasks = new List<UserTaskDE> ();
                att.Day = att.Date.Value.DayOfWeek.ToString ();
                if (att.DayEndTime == null)
                    att.DayEndTime = DateTime.Now;

                #region DayStart/End

                att.DayStartandEnd = GetDayStartandEndStr (att);

                #endregion
                #region In/OutTime

                att.InandOutTime = GetInOutTimeStr (att);

                #endregion
                #region ScheduleTime & Due Time

                if (att.SchDayId > 0)
                {
                    var sch = _schSvc.GetScheduleandDueTimeStr (att.UserId,(int)att.SchDayId, att.ClientId, (DateTime)att.Date);
                    att.SchTime = sch.Item1;
                    att.DueSPs = sch.Item2;
                }

                #endregion
                #region Late

                if(att.SchId>0)
                schEvents = _schSvc.GetDayEvents (att.SchDayId, att.ClientId);
                if (schEvents != null && schEvents.Count > 0)
                {
                    var firstSchEvt = schEvents[0];
                    if (att.DayStartTime != null && firstSchEvt.StartTime != null)
                    {
                        if (TimeSpan.TryParse (firstSchEvt.StartTime, out var schStartTime))
                        {
                            DateTime scheduleStartTime = att.DayStartTime.Value.Date + schStartTime;
                            if (att.DayStartTime > scheduleStartTime)
                                att.Late = (att.DayStartTime - scheduleStartTime).ToString ();
                        }
                    }
                }

                #endregion
                #region Day WorkedTime

                if (att.DayEndTime.Value < att.DayStartTime.Value)
                    att.DayEndTime = att.DayEndTime.Value.AddDays (1);
                TimeSpan workedTime = att.DayEndTime.Value - att.DayStartTime.Value;
                att.WorkedTime = new TimeSpan (workedTime.Hours, workedTime.Minutes, workedTime.Seconds).ToString ();

                #endregion
                #region Missing & Extra Time

                if (att.DueSPs != null && att.WorkedTime != null)
                {
                    TimeSpan.TryParse (att.DueSPs, out var dueSps);
                    TimeSpan.TryParse (att.WorkedTime, out var workTime);
                    if (workTime > dueSps)
                        att.ExtraTime = (workTime - dueSps).ToString ();
                    else if (dueSps > workTime)
                    {
                        att.MissingTime = (dueSps - workTime).ToString ();
                    }
                }

                #endregion
                #region User Targets, ClaimedSPs, DayEndStatus, Claimed%, SPsGap, ClaimError, FinalScore

                string WhereClause = $"where   DATE(Date) = ''{att.Date.Value.ToString ("yyyy-MM-dd")}''   and UserId = ''" + att.UserId + "'' and ClientId=" + att.ClientId + "";
                userTasks = _uTaskDAL.SearchUserTask (WhereClause);
                var _uTasks = _uTaskDAL.SearchUserTask ($"where  UserId = ''" + att.UserId + "'' and ClientId = " + att.ClientId + "");
                double workedSPs = 0;
                double totalSPs = 0;
                double approvedSps = 0;
                att.UserTasks = userTasks;
                foreach (var task in userTasks)
                {
                    var taskWithStatus = GetTaskStatus (task);
                    task.IsLastExistence = taskWithStatus.IsLastExistence;
                    task.ClaimWorkTime = taskWithStatus.ClaimWorkTime;
                    task.ExtraTime = taskWithStatus.ExtraTime;
                    task.IsOverdue = taskWithStatus.IsOverdue;
                    task.IsEarlyFinshed = taskWithStatus.IsEarlyFinshed;

                    #region Task Score & Task Completion Percentage
                    if (task.ClaimId > 0)
                    {
                        double currentClaim = 0;
                        if (task.LastClaim != null)
                            currentClaim = Math.Max (0, (double.Parse (task.ClaimPercent) - double.Parse (task.LastClaim)));
                        else
                            currentClaim = double.Parse (task.ClaimPercent);
                        task.TaskScore = Math.Round (((currentClaim * task.Sp) / 100), 2).ToString ();
                        task.TaskComPer = Math.Round ((double.Parse (task.TaskScore) * 100) / task.Sp, 2).ToString () + " %";
                    }
                    #endregion

                    if (task.ClaimPercent != null && task.ApprovedClaimId > 0)
                        task.ClaimError = Math.Max (0, ((int.Parse (task.ClaimPercent)) - (int.Parse (task.ApprovedClaim)))).ToString ();

                    #region Targets

                    var stringBuilder = new StringBuilder ();
                    if (task.Module != null)
                        stringBuilder.Append (task.Module + "-");
                    stringBuilder.Append (task.TaskId.ToString () + "(" + task.Sp + "SPs" + ")" + ": " + task.Title);
                    if (task != userTasks.Last ())
                        stringBuilder.Append (", ");
                    att.Targets += stringBuilder;

                    #endregion

                    if (task.ClaimPercent != null)
                    {
                        #region DayEndStatus

                        var dayEndStatus = new StringBuilder ();
                        if (task.Module != null)
                            dayEndStatus.Append (task.Module + "- ");
                        dayEndStatus.Append (task.TaskId.ToString () + " : " + task.ClaimPercent + "%");
                        if (task.Comments != null)
                            dayEndStatus.Append ("(" + task.Comments + ")");
                        if (task != userTasks.Last ())
                            dayEndStatus.Append (", ");
                        att.DayEndStatus += dayEndStatus;

                        #endregion
                        #region Claimed SPs

                        task.WorkSP = Math.Round (((double.Parse (task.ClaimPercent) * task.Sp) / 100), 2);
                        workedSPs += ((double.Parse (task.ClaimPercent) * task.Sp) / 100);
                        totalSPs += task.Sp;

                        #endregion
                        #region Approved Claim

                        String approveClaim = String.Empty;
                        if (task.ApprovedClaim != null)
                            approveClaim = task.ApprovedClaim;
                        else
                            approveClaim = task.ClaimPercent;
                        task.FinalScore = Math.Round ((double.Parse (approveClaim) * task.Sp / 100), 2).ToString ();
                        approvedSps += (double.Parse (approveClaim) * task.Sp / 100);

                        #endregion
                    }
                }
                if (workedSPs > 0 && totalSPs > 0)
                {
                    att.ClaimedSPs = Math.Round (workedSPs, 2).ToString () + "/" + Math.Round (totalSPs, 2).ToString ();
                    att.FinalScore = Math.Round (approvedSps, 2).ToString () + "/" + Math.Round (totalSPs, 2).ToString ();
                    #region Claimed %

                    att.ClaimPer = Math.Round ((workedSPs * 100) / totalSPs, 2).ToString () + " %";

                    #endregion
                    #region SPs Gap

                    att.SPsGap = Math.Round (totalSPs - approvedSps, 2).ToString ();

                    #endregion
                }
                #endregion
                #region Default Values
                if (att.Late == null)
                    att.Late = "00:00:00";
                if (att.DueSPs == null)
                    att.DueSPs = "N/A";
                if (att.SchTime == null)
                    att.SchTime = "N/A";
                if (att.ExtraTime == null)
                    att.ExtraTime = "00:00:00";
                if (att.MissingTime == null)
                    att.MissingTime = "00:00:00";
                if (att.ClaimedSPs == null)
                    att.ClaimedSPs = "N/A";
                if (att.ClaimPer == null)
                    att.ClaimPer = "N/A";
                if (att.SPsGap == null)
                    att.SPsGap = "N/A";
                if (att.FinalScore == null)
                    att.FinalScore = "N/A";
                if (att.DayEndStatus == null)
                    att.DayEndStatus = "N/A";

                #endregion
            }
            return retVals;
        }
        public string GetInOutTimeStr ( AttendanceDE att )
        {
            string inOutTime = String.Empty;
            var events = _logEventSvc.SearchLogEvents (new LogEventDE { UserId = att.UserId, Date = att.Date, ClientId=att.ClientId });
            if (events != null && events.Count > 0)
                foreach (var evt in events)
                {
                    if (evt.OutTime == null)
                        evt.OutTime = DateTime.Now;
                    if (evt.OutTime.Value.Date == evt.InTime.Value.Date)
                        inOutTime += evt.InTime.Value.TimeOfDay.ToString (AppConstants.TIME_FORMAT_HHMMSS) + " - " + evt.OutTime.Value.TimeOfDay.ToString (AppConstants.TIME_FORMAT_HHMMSS);
                    else
                        inOutTime += evt.InTime.Value.ConvertToDateWith24HFormat () + " - " + evt.OutTime.Value.ConvertToDateWith24HFormat ();
                    if (evt != events.Last ())
                        inOutTime += ",";
                }
            return inOutTime;
        }
        public string GetDayStartandEndStr ( AttendanceDE att )
        {
            string dayStartandEnd = String.Empty;
            if (att.DayStartTime.Value.Date == att.DayEndTime.Value.Date)
                dayStartandEnd = att.DayStartTime.Value.TimeOfDay.ToString (AppConstants.TIME_FORMAT_HHMMSS) + " - " + att.DayEndTime.Value.TimeOfDay.ToString (AppConstants.TIME_FORMAT_HHMMSS);
            else
                dayStartandEnd = att.DayStartTime.Value.ConvertToDateWith24HFormat () + " - " + att.DayEndTime.Value.ConvertToDateWith24HFormat ();
            return dayStartandEnd;
        }
        public UserTaskDE GetTaskStatus ( UserTaskDE task )
        {
            var _task = _uTaskDAL.SearchUserTask ($"where  TaskId = ''" + task.TaskId + "'' and ClientId=" + task.ClientId + "");
            if (_task != null)
            {
                var taskLastExistence = _task.OrderByDescending (x => x.Id).First ();
                if (task.Id == taskLastExistence.Id)
                    task.IsLastExistence = true;
                _task = _task.Where (x => x.Id <= task.Id).ToList ();
                float totalClaim = (float)_task.Select (obj => obj.WorkTime).Sum ();
                task.ClaimWorkTime = string.Join ("+ ", _task.Select (obj => obj.WorkTime)) + " = " + totalClaim;
                task.ExtraTime = Math.Max (0, totalClaim - task.Sp).ToString ();
                if (totalClaim > task.Sp)
                    task.IsOverdue = true;
                else if (totalClaim < task.Sp && (task.ApprovedClaimId > 0 ? task.ApprovedClaimId == (int)ClaimPer.Claim_100Per : task.ClaimId == (int)ClaimPer.Claim_100Per))
                {
                    task.IsOverdue = false;
                    task.IsEarlyFinshed = true;
                }
            }
            return task;
        }

    }
}
