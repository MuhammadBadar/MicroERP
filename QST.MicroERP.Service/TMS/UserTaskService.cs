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
using QST.MicroERP.Core.Constants;
using System.Data;

namespace QST.MicroERP.Service.TMS
{
    public class UserTaskService : BaseService
    {
        #region Class Variables
        private TaskService _taskSvc;
        private UserTaskDAL _tskDAL;
        private AttendanceService _attSvc;
        private ScheduleService _schSvc;
        private AttendanceDAL _attDAL;
        private NTF_NotificationService _notifSvc;
        private UserService _userSvc;
        private TaskDAL _taskDAL;
        #endregion
        #region Constructor
        public UserTaskService ( )
        {
            _attDAL = new AttendanceDAL ();
            _schSvc = new ScheduleService ();
            _attSvc = new AttendanceService ();
            _taskSvc = new TaskService ();
            _tskDAL = new UserTaskDAL ();
            _notifSvc = new NTF_NotificationService ();
            _userSvc = new UserService ();
            _taskDAL = new TaskDAL ();
        }
        #endregion
        #region  UserTask
        public bool ManageUserTask ( List<UserTaskDE> mod, bool? markAttendance, bool? markDayEnd )
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
                MicroERPDataContext.StartTransaction (cmd);
                _entity = TableNames.TMS_UserTask.ToString ();

                if (mod != null && mod.Count > 0)
                {
                    var ClientId = mod[0].ClientId;
                    int counter = 0;
                    int lastId = 0;
                    var date = DateTime.Now;
                    #region Delete Previous Tasks
                    if (mod[0].DBoperation == DBoperations.Insert)
                    {
                        var pickedTasks = SearchUserTask (new TaskSearchCriteria { UserId = mod[0].UserId, Date = DateTime.Now, ClientId = ClientId });
                        pickedTasks = pickedTasks.Where (x => x.StatusId != (int)Core.Enums.TaskStatus.Stalled).ToList ();
                        if (pickedTasks != null && pickedTasks.Count > 0)
                        {
                            date = pickedTasks[0].Date;
                            foreach (var task in pickedTasks)
                            {
                                task.DBoperation = DBoperations.Delete;
                                _tskDAL.ManageUserTask (task, cmd);
                            }
                        }
                    }
                    #endregion
                    #region Insert & Update Task
                    foreach (var _uTask in mod)
                    {
                        if (_uTask.DBoperation == DBoperations.Insert)
                        {
                            if (counter == 0)
                            {
                                _uTask.Id = _coreDAL.GetNextIdByClient (_entity.ToString (), ClientId, "ClientId");
                                lastId = _uTask.Id;
                            }
                            else
                            {
                                lastId += 1;
                                _uTask.Id = lastId;
                            }
                            counter += 1;
                            _uTask.Date = date;
                        }
                        #region Change Task Status
                        var retVals = _taskSvc.SearchTasks (new TaskSearchCriteria { Id = _uTask.TaskId, ClientId = ClientId });
                        if (_uTask.DBoperation == DBoperations.Insert)
                        {
                            if (retVals != null && retVals.Count > 0)
                            {
                                var task = retVals.FirstOrDefault ();
                                if (task.StatusId == (int)Core.Enums.TaskStatus.Open)
                                {
                                    task.StatusId = (int)Core.Enums.TaskStatus.InProgress;
                                    task.DBoperation = DBoperations.Update;
                                    _taskSvc.ManagementTask (task);
                                }
                            }
                        }
                        if (retVals != null && retVals.Count > 0)
                        {
                            var task = retVals[0];
                            if (_uTask.ClaimId == (int)ClaimPer.Claim_100Per)
                            {
                                {
                                    if (_uTask.ApprovedClaimId > 0 && _uTask.ApprovedClaimId != (int)ClaimPer.Claim_100Per)
                                    {
                                        task.StatusId = (int)Core.Enums.TaskStatus.ReOpen;
                                        _uTask.StatusId = (int)Core.Enums.TaskStatus.ReOpen;
                                    }
                                    else
                                    {
                                        task.StatusId = (int)Core.Enums.TaskStatus.InTesting;
                                        _uTask.StatusId = (int)Core.Enums.TaskStatus.InTesting;
                                    }
                                }
                            }
                            else
                            {
                                if (_uTask.ApprovedClaimId > 0 && _uTask.ApprovedClaimId == (int)ClaimPer.Claim_100Per)
                                {
                                    task.StatusId = (int)Core.Enums.TaskStatus.InTesting;
                                    _uTask.StatusId = (int)Core.Enums.TaskStatus.InTesting;
                                }
                            }
                            if (_uTask.DBoperation == DBoperations.Insert && (task.StatusId == (int)Core.Enums.TaskStatus.Stalled || task.StatusId == (int)Core.Enums.TaskStatus.ReOpen))
                            {
                                task.Reason = "";
                                task.StatusId = (int)Core.Enums.TaskStatus.InProgress;
                            }
                            task.DBoperation = DBoperations.Update;
                            _logger.Info ($"Going to Call:_taskDAL.ManageTask (task, cmd)");
                            if (_taskDAL.ManageTask (task, cmd))
                            {
                                task.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, TableNames.TMS_Task.ToString (), task.DBoperation.ToString ()));
                                _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, TableNames.TMS_Task.ToString (), task.DBoperation.ToString ()));
                            }
                            else
                            {
                                task.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, TableNames.TMS_Task.ToString ()));
                                _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, TableNames.TMS_Task.ToString ()));
                            }
                        }
                        #endregion
                        _logger.Info ($"Going to Call:_tskDAL.ManageUserTask (_tsk, cmd)");
                        if (_tskDAL.ManageUserTask (_uTask, cmd))
                        {
                            _uTask.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, _uTask.DBoperation.ToString ()));
                            _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, _uTask.DBoperation.ToString ()));
                        }
                        else
                        {
                            _uTask.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                            _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                        }
                    }
                    #endregion

                    #region MarkAttendance
                    if (mod != null && mod.Count > 0)
                    {
                        if (markAttendance == true)
                            DayStart (mod[0].UserId, mod[0].ClientId, mod);
                        if (markDayEnd == true)
                            DayEnd (mod[0].UserId, mod[0].ClientId);
                    }
                    #endregion
                }

                MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception ex)
            {
                retVal = false;
                _logger.Error (ex);
                MicroERPDataContext.CancelTransaction (cmd);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                  MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return retVal;
        }
        public void DayEnd ( string UserId, int clientId )
        {
            var ntf = new NotificationLogDE ();
            try
            {
                string whereClause = "WHERE UserId like ''" + UserId + "'' AND ClientId = " + clientId + " ORDER BY Id DESC LIMIT 1";
                var _att = _attDAL.SearchAttendance (whereClause);
                if (_att != null && _att.Count > 0)
                {
                    var att = _att[0];
                    att.DayEndTime = DateTime.Now;
                    att.DBoperation = DBoperations.Update;
                    if (_attSvc.ManageAttendance (att, cmd).Id > 0)
                    {
                        //att = _attSvc.SearchAttendance (new AttendanceDE { Id = att.Id, ClientId = clientId }).FirstOrDefault ();
                        if (att != null && att.Id > 0)
                        {
                            att.UserTasks = SearchUserTask (new TaskSearchCriteria { Date = att.Date.Value, UserId = att.UserId, ClientId = att.ClientId });
                            var user = _userSvc.SearchUsers (new UserDE { Id = UserId, ClientId = clientId }).FirstOrDefault ();
                            if (user != null)
                            {
                                att.User = user.UserName;
                                var supervisors = _userSvc.GetSupervisor (user);
                                supervisors = supervisors.Where (x => x.Id != user.Id).ToList ();
                                foreach (var supv in supervisors)
                                {
                                    att.Supervisor = supv.UserName;
                                    ntf = _notifSvc.GenerateNotification (NotificationTemplates.ATT_NotificationToSupervisor_OnDayEnd, att);
                                    ntf.To = supv.Email;
                                    ntf.UserId = att.UserId;
                                    ntf.ClientId = att.ClientId;
                                    ntf.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayEnd.ToString ();
                                    ntf.DBoperation = DBoperations.Insert;
                                    bool retVal = _notifSvc.ManagementNotificationLog (ntf);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
        }
        public void DayStart ( string UserId, int clientId, List<UserTaskDE> userTasks )
        {
            var ntf = new NotificationLogDE ();
            try
            {
                var att = new AttendanceDE ();
                att.ClientId = clientId;
                att.UserId = UserId;
                att.IsActive = true;
                att.DayStartTime = DateTime.Now;
                att.Date = DateTime.Now;
                var schDay = _schSvc.GetSchDayByUserId (att.UserId, clientId, DateTime.Now);
                if (schDay != null)
                    att.SchDayId = schDay.Id;
                att.DBoperation = DBoperations.Insert;
                att.IsActive = true;
                att.CreatedOn = DateTime.Now;
                att.ModifiedOn = DateTime.Now;
                att.UserTasks = userTasks;
                if (_attSvc.ManageAttendance (att, cmd).Id > 0)
                {
                    att= _attSvc.SearchAttendance(new AttendanceDE { Id = att.Id, ClientId = clientId }).FirstOrDefault();
                    if (att != null && att.Id > 0)
                    {
                        att.UserTasks = SearchUserTask (new TaskSearchCriteria { Date = att.Date.Value, UserId = att.UserId, ClientId = att.ClientId });
                        var user = _userSvc.SearchUsers (new UserDE { Id = UserId, ClientId=clientId }).FirstOrDefault ();
                        if (user != null)
                        {
                            att.User = user.UserName;
                            var supervisors = _userSvc.GetSupervisor (user);
                            supervisors = supervisors.Where (x => x.Id != user.Id).ToList();
                            foreach (var supv in supervisors)
                            {
                                att.Supervisor = supv.UserName;
                                ntf = _notifSvc.GenerateNotification (NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart, att);
                                ntf.To = supv.Email;
                                ntf.UserId = att.UserId;
                                ntf.ClientId = att.ClientId;
                                ntf.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart.ToString ();
                                ntf.DBoperation = DBoperations.Insert;
                                bool retVal = _notifSvc.ManagementNotificationLog (ntf);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
        }
        public List<UserTaskDE> SearchUserTask ( TaskSearchCriteria _tsk )
        {
            bool closeConnectionFlag = false;
            List<UserTaskDE> retVal = new List<UserTaskDE> ();
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                string WhereClause = " Where 1=1";
                if (_tsk.Id != default)
                    WhereClause += $" AND Id={_tsk.Id}";
                if (_tsk.ClientId != default && _tsk.ClientId != 0)
                    WhereClause += $" AND ClientId={_tsk.ClientId}";
                if (_tsk.TaskId != default)
                    WhereClause += $" AND TaskId={_tsk.TaskId}";
                if (_tsk.ClaimId != default)
                    WhereClause += $" AND ClaimId={_tsk.ClaimId}";
                if (_tsk.ApprovedClaimId != default)
                    WhereClause += $" AND ApprovedClaimId={_tsk.ApprovedClaimId}";
                if (_tsk.SP != default)
                    WhereClause += $" AND sp={_tsk.SP}";
                if (_tsk.Date != default)
                    WhereClause += $" AND DATE(Date) = ''{_tsk.Date.ToString ("yyyy-MM-dd")}''";
                if (_tsk.FromDate.HasValue)
                    WhereClause += $" and Date >= ''{_tsk.FromDate.Value:yyyy-MM-dd} 00:00:00''";
                if (_tsk.ToDate.HasValue)
                    WhereClause += $" and Date <= ''{_tsk.ToDate.Value:yyyy-MM-dd} 23:59:59''";
                if (_tsk.IsActive != default && _tsk.IsActive == true)
                    WhereClause += $" AND IsActive=1";
                if (_tsk.IsDayEnded.HasValue)
                    WhereClause += $" AND IsDayEnded={_tsk.IsDayEnded}";
                if (_tsk.IncludeSubordinatesData && _tsk.UserId != default)
                {
                    var user = new UserDE ();
                    user.Id = _tsk.UserId;
                    user.ClientId = _tsk.ClientId;
                    var subordinateUsers = _userSvc.GetSubordinates (user);
                    if (subordinateUsers.Count > 0)
                    {
                        string subordinateIds = string.Join ("'',''", subordinateUsers.Select (x => x.Id));
                        WhereClause += $"and (UserId like ''" + _tsk.UserId + "'' or UserId IN (''" + subordinateIds + "''))";
                    }
                    else
                    {
                        if (_tsk.UserId != default && _tsk.UserId != "")
                            WhereClause += $" and UserId like ''" + _tsk.UserId + "''";
                    }
                }
                else
                {
                    if (_tsk.UserId != default && _tsk.UserId != "")
                        WhereClause += $" and UserId like ''" + _tsk.UserId + "''";
                }
                retVal = _tskDAL.SearchUserTask (WhereClause, cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public bool IsDayStarted ( string UserId, int ClientId )
        {
            bool isStarted = false;
            if (UserId != null)
            {
                var tasks = SearchUserTask (new TaskSearchCriteria { UserId = UserId, IsDayEnded = false, ClientId = ClientId });
                if (tasks != null && tasks.Count > 0)
                    isStarted = true;
            }
            return isStarted;
        }
        public bool HasTodaysTasks ( string UserId, int ClientId )
        {
            bool hasTargets = false;
            if (UserId != null)
            {
                var tasks = SearchUserTask (new TaskSearchCriteria { UserId = UserId, Date = DateTime.Now, ClientId = ClientId, IsDayEnded=false });
                if (tasks != null && tasks.Count > 0)
                    hasTargets = true;
            }
            return hasTargets;
        }
        public bool ChangeTaskStatus ( TaskDE task )
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
                MicroERPDataContext.StartTransaction (cmd);

                var userTasks = SearchUserTask (new TaskSearchCriteria { TaskId = task.Id, ClientId = task.ClientId });
                if (userTasks != null)
                {
                    var latestTask = userTasks.OrderByDescending (x => x.Id).FirstOrDefault ();
                    if (latestTask != null)
                    {
                        latestTask.DBoperation = DBoperations.Update;
                        latestTask.IsDayEnded = true;
                        latestTask.StatusId = (int)Core.Enums.TaskStatus.Stalled;
                        latestTask.StalledReason = task.Reason;
                        _tskDAL.ManageUserTask (latestTask, cmd);
                    }
                }
                task.StatusId = (int)Core.Enums.TaskStatus.Stalled;
                retVal = _taskDAL.ManageTask (task, cmd);
                if (retVal)
                {
                    var users = _userSvc.SearchUsers (new UserDE { Id = task.UserId, ClientId = task.ClientId });
                    if (users != null && users.Count > 0)
                    {
                        //var user= users.FirstOrDefault ();
                        //NotificationDE notif = new NotificationDE ();
                        //notif.ReceiverMail = user.SupervisorEmail
                    }
                }
                MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception ex)
            {
                retVal = false;
                _logger.Error (ex);
                MicroERPDataContext.CancelTransaction (cmd);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return retVal;
        }
        public List<TaskDE> GetTaskActivities ( TaskSearchCriteria mod )
        {
            try
            {
                List<TaskDE> tasks = new List<TaskDE> ();
                var searchCriteria = new TaskSearchCriteria ();
                searchCriteria.Id = mod.Id;
                searchCriteria.ClientId = mod.ClientId;
                tasks = _taskSvc.SearchTasks (searchCriteria);
                foreach (var task in tasks)
                {
                    float? totalWork = 0;
                    task.TaskActivities = SearchUserTask (new TaskSearchCriteria { TaskId = task.Id, ClientId = mod.ClientId, UserId = mod.UserId, FromDate = mod.FromDate, ToDate = mod.ToDate, IncludeSubordinatesData = mod.IncludeSubordinatesData });
                    foreach (var activity in task.TaskActivities)
                    {
                        var status = _attSvc.GetTaskStatus (activity);
                        activity.IsEarlyFinshed = status.IsEarlyFinshed;
                        activity.IsOverdue = status.IsOverdue;
                        if (activity.WorkTime != null)
                            totalWork += activity.WorkTime;
                    }
                    if (totalWork > 0)
                    {
                        task.ExtraTime = Math.Max (0, (float)(totalWork - task.SP));
                        task.TotalWorkedTime = totalWork;
                    }
                    var isOverDue = task.TaskActivities.Any (x => x.IsOverdue);
                    task.IsOverdue = isOverDue;
                    var isEarlyFinshed = task.TaskActivities.Any (x => x.IsEarlyFinshed);
                    task.IsEarlyFinshed = isEarlyFinshed;
                }
           
                tasks = tasks.OrderByDescending (x => x.Date).ToList ();
                tasks = tasks.Where (x => x.TaskActivities.Count > 0).ToList ();
                return tasks;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion
    }
}
