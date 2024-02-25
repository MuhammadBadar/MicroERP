using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.SearchCriteria;
using QST.MicroERP.DAL;
using MySql.Data.MySqlClient;
using NLog;
using AutoMapper;
using System.Threading.Tasks;
using System.Net.Mail;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Service.ATT;
using QST.MicroERP.Service.SEC;
using QST.MicroERP.Service.SCH;
using QST.MicroERP.DAL.ATT;
using QST.MicroERP.DAL.TMS;
using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.Core.Entities.ATT;
using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Core.Entities.NTF;

namespace QST.MicroERP.Service.TMS
{
    public class UserTaskService
    {
        #region Class Variables
        private TaskService _taskSvc;
        private UserTaskDAL _tskDAL;
        private CoreDAL _coreDAL;
        private Logger _logger;
        private AttendanceService _attSvc;
        private ScheduleService _schSvc;
        private AttendanceDAL _attDAL;
        private NTF_NotificationService _notifSvc;
        private UserService _userSvc;
        private TaskDAL _taskDAL;
        #endregion
        #region Constructor
        public UserTaskService()
        {
            _attDAL = new AttendanceDAL();
            _schSvc = new ScheduleService();
            _attSvc = new AttendanceService();
            _taskSvc = new TaskService();
            _tskDAL = new UserTaskDAL();
            _coreDAL = new CoreDAL();
            _notifSvc = new NTF_NotificationService();
            _userSvc = new UserService();
            _taskDAL = new TaskDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region  UserTask
        public bool ManageUserTask(List<UserTaskDE> _tsks, bool? markAttendance, bool? markDayEnd)
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);
                closeConnectionFlag = true;

                int counter = 0;
                int lastId = 0;
                var date = DateTime.Now;
                #region Delete Previous Tasks
                if (_tsks[0].DBoperation == DBoperations.Insert)
                {
                    var pickedTasks = SearchUserTask(new TaskSearchCriteria { UserId = _tsks[0].UserId, Date = DateTime.Now });
                    if (pickedTasks != null && pickedTasks.Count > 0)
                    {
                        date = pickedTasks[0].Date;
                        foreach (var task in pickedTasks)
                        {
                            task.DBoperation = DBoperations.Delete;
                            _tskDAL.ManageUserTask(task, cmd);
                        }
                    }
                }
                #endregion
                #region Insert & Update Task
                foreach (var _tsk in _tsks)
                {
                    if (_tsk.DBoperation == DBoperations.Insert)
                    {
                        if (counter == 0)
                        {
                            _tsk.Id = _coreDAL.GetnextId(TableNames.TMS_UserTask.ToString());
                            lastId = _tsk.Id;
                        }
                        else
                        {
                            lastId += 1;
                            _tsk.Id = lastId;
                        }
                        counter += 1;
                        _tsk.Date = date;
                    }
                    retVal = _tskDAL.ManageUserTask(_tsk, cmd);
                    #region Change Task Status
                    var retVals = _taskSvc.SearchTasks(new TaskSearchCriteria { Id = _tsk.TaskId });
                    if (_tsk.DBoperation == DBoperations.Insert && retVal == true)
                    {
                        if (retVals != null && retVals.Count > 0)
                        {
                            var task = retVals.FirstOrDefault();
                            if (task.StatusId == (int)Status.Open)
                            {
                                task.StatusId = (int)Status.InProgress;
                                task.DBoperation = DBoperations.Update;
                                _taskSvc.ManagementTask(task);
                            }
                        }
                    }
                    if (retVals != null && retVals.Count > 0)
                    {
                        var task = retVals[0];
                        if (_tsk.ClaimId == (int)ClaimPer.Claim_100Per)
                        {
                            {
                                if (_tsk.ApprovedClaimId != (int)ClaimPer.Claim_100Per) task.StatusId = (int)Status.ReOpen;
                                else
                                    task.StatusId = (int)Status.InTesting;
                                task.DBoperation = DBoperations.Update;
                                _taskDAL.ManageTask(task, cmd);
                            }
                        }
                        if (_tsk.DBoperation == DBoperations.Insert && task.StatusId == (int)Status.Stalled)
                        {
                            //task.Reason="";
                            task.StatusId = (int)Status.InProgress;
                            task.DBoperation = DBoperations.Update;
                            _taskDAL.ManageTask(task, cmd);
                        }
                    }
                    #endregion
                }
                #endregion
                #region MarkAttendance
                if (_tsks != null && _tsks.Count > 0)
                    if (markAttendance == true)
                        DayStart(_tsks[0].UserId, _tsks[0].ClientId, _tsks);
                if (markDayEnd == true)
                    DayEnd(_tsks[0].UserId, _tsks[0].ClientId);
                #endregion

                MicroERPDataContext.EndTransaction(cmd);
            }
            catch (Exception ex)
            {
                retVal = false;
                _logger.Error(ex);
                MicroERPDataContext.CancelTransaction(cmd);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return retVal;
        }
        public void DayEnd(string UserId, int clientId)
        {
            try
            {
                string whereClause = "WHERE UserId like ''" + UserId + "'' AND ClientId = " + clientId + " ORDER BY Id DESC LIMIT 1";
                var att = _attDAL.SearchAttendance(whereClause);
                if (att != null && att.Count > 0)
                {
                    var _att = att[0];
                    _att.DayEndTime = DateTime.Now;
                    _att.DBoperation = DBoperations.Update;
                    _attSvc.ManageAttendance(_att);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
        public void DayStart(string UserId, int clientId, List<UserTaskDE> userTasks)
        {
            try
            {
                var att = new AttendanceDE();
                att.ClientId = clientId;
                att.UserId = UserId;
                att.IsActive = true;
                att.DayStartTime = DateTime.Now;
                att.Date = DateTime.Now;
                var schDay = _schSvc.GetSchDayByUserId(att.UserId, DateTime.Now);
                if (schDay != null)
                    att.SchDayId = schDay.Id;
                att.DBoperation = DBoperations.Insert;
                att.IsActive = true;
                att.CreatedOn = DateTime.Now;
                att.ModifiedOn = DateTime.Now;
                att.User = "Sumaira";
                att.Supervisor = "Badar";
                att.UserTasks = userTasks;
                _attSvc.ManageAttendance(att);

                
                var ntf = _notifSvc.GenerateNotification(NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart, att);
                ntf.ClientId = att.ClientId;
                ntf.UserId = att.UserId;
                ntf.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart.ToString();
                ntf.DBoperation = DBoperations.Insert;
                bool retVal = _notifSvc.ManagementNotificationLog(ntf);
                

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
        public List<UserTaskDE> SearchUserTask(TaskSearchCriteria _tsk)
        {
            List<UserTaskDE> retVal = new List<UserTaskDE>();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_tsk.Id != default)
                    WhereClause += $" AND Id={_tsk.Id}";
                if (_tsk.UserId != default)
                    WhereClause += $" and UserId = ''" + _tsk.UserId + "''";
                if (_tsk.TaskId != default)
                    WhereClause += $" AND TaskId={_tsk.TaskId}";
                if (_tsk.ClaimId != default)
                    WhereClause += $" AND ClaimId={_tsk.ClaimId}";
                if (_tsk.ApprovedClaimId != default)
                    WhereClause += $" AND ApprovedClaimId={_tsk.ApprovedClaimId}";
                if (_tsk.SP != default)
                    WhereClause += $" AND sp={_tsk.SP}";
                if (_tsk.Date != default)
                    WhereClause += $" AND DATE(Date) = ''{_tsk.Date.ToString("yyyy-MM-dd")}''";
                if (_tsk.IsActive != default && _tsk.IsActive == true)
                    WhereClause += $" AND IsActive=1";
                if (_tsk.IsDayEnded.HasValue)
                    WhereClause += $" AND IsDayEnded={_tsk.IsDayEnded}";

                retVal = _tskDAL.SearchUserTask(WhereClause, cmd);
                //foreach (var line in retVal)
                //{
                //    if (line.ClaimPercent != null)
                //    {
                //        if (line.ApprovedClaimId > 0)
                //            line.ClaimPercent = line.ApprovedClaim;
                //    }
                //    else
                //    {
                //        if (line.ApprovedClaimId > 0)
                //            line.LastClaim = line.ApprovedClaim;
                //        line.ClaimId = line.LastClaimId;
                //        line.ClaimPercent = line.LastClaim;
                //    }
                //}
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
        public bool IsDayStarted(string UserId)
        {
            bool isStarted = false;
            if (UserId != null)
            {
                var tasks = SearchUserTask(new TaskSearchCriteria { UserId = UserId, IsDayEnded = false });
                if (tasks != null && tasks.Count > 0)
                    isStarted = true;
            }
            return isStarted;
        }
        public bool HasTodaysTasks(string UserId)
        {
            bool hasTargets = false;
            if (UserId != null)
            {
                var tasks = SearchUserTask(new TaskSearchCriteria { UserId = UserId, Date = DateTime.Now });
                if (tasks != null && tasks.Count > 0)
                    hasTargets = true;
            }
            return hasTargets;
        }
        public bool ChangeTaskStatus(TaskDE task)
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);
                closeConnectionFlag = true;
                var userTasks = SearchUserTask(new TaskSearchCriteria { TaskId = task.Id });
                if (userTasks != null)
                {
                    var latestTask = userTasks.OrderByDescending(x => x.Id).LastOrDefault();
                    if (latestTask != null)
                    {
                        latestTask.DBoperation = DBoperations.Update;
                        latestTask.IsDayEnded = true;
                        _tskDAL.ManageUserTask(latestTask, cmd);
                    }
                }
                task.StatusId = (int)Status.Stalled;
                retVal = _taskDAL.ManageTask(task, cmd);
                if (retVal)
                {
                    var users = _userSvc.SearchUsers(new UserDE { Id = task.UserId });
                    if (users != null && users.Count > 0)
                    {
                        //var user= users.FirstOrDefault ();
                        //NotificationDE notif = new NotificationDE ();
                        //notif.ReceiverMail = user.SupervisorEmail
                    }
                }
                MicroERPDataContext.EndTransaction(cmd);
            }
            catch (Exception ex)
            {
                retVal = false;
                _logger.Error(ex);
                MicroERPDataContext.CancelTransaction(cmd);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return retVal;
        }
        #endregion
    }
}
