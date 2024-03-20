

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using QST.MicroERP.DAL;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Core.SearchCriteria;
using QST.MicroERP.Service.SEC;
using QST.MicroERP.DAL.TMS;
using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Core.Constants;
using System.Data;

namespace QST.MicroERP.Service.TMS
{
    public class TaskService : BaseService
    {

        #region Class Members/Class Variables
        private readonly string AppDirectory = Path.Combine (Directory.GetCurrentDirectory (), "wwwroot");
        private TaskDAL _taskDAL;
        private UserService _userSvc;

        #endregion
        #region Constructors
        public TaskService ( )
        {
            _taskDAL = new TaskDAL ();
            _userSvc = new UserService ();
        }

        #endregion
        #region Task
        public TaskDE ManagementTask ( TaskDE mod )
        {
            int fileId = 0;
            bool closeConnectionFlag = false;
            try
            {
                mod.HasErrors = false;
                bool check = true;
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                MicroERPDataContext.StartTransaction (cmd);
                _entity = TableNames.TMS_Task.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");
                if (mod.Id == 1)
                    mod.Id = 1001;

                _logger.Info ($"Going to Call:_taskDAL.ManageTask (mod)");
                if (_taskDAL.ManageTask (mod))
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));

                    _entity = TableNames.TMS_Attachments.ToString ();
                    fileId = _coreDAL.GetNextIdByClient (TableNames.TMS_Attachments.ToString (), mod.ClientId, "ClientId");
                    foreach (var file in mod.Attachments)
                    {
                        if (!Directory.Exists (AppDirectory))
                            Directory.CreateDirectory (AppDirectory);
                        var FileName = DateTime.Now.Ticks.ToString () + Path.GetExtension (file.Name);
                        var path = Path.Combine (AppDirectory, FileName);
                        if(file.DBoperation==DBoperations.Insert)
                        file.Id = fileId;
                        file.DocPath = path;
                        file.TaskId = mod.Id;
                        file.ClientId = mod.ClientId;
                       // file.DBoperation = mod.DBoperation;
                        _logger.Info ($"Going to Call:_taskDAL.ManageAttachments (file)");
                        if (_taskDAL.ManageAttachments (file))
                        {
                            file.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, file.DBoperation.ToString ()));
                            _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, file.DBoperation.ToString ()));
                        }
                        else
                        {
                            file.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                            _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                        }

                        if (file.DBoperation == DBoperations.Insert)
                            fileId += 1;
                    }
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                }

                MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                MicroERPDataContext.CancelTransaction (cmd);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return mod;
        }
        public List<UserTaskVM> SearchUserTasks ( TaskSearchCriteria mod )
        {
            List<UserTaskVM> tasks = new List<UserTaskVM> ();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                #region Search

                string whereClause;
                //whereClause = "where 1=1 AND ClaimId <> 1013001";
                whereClause = "where 1=1 ";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive={mod.IsActive}";
                if (mod.UserId != default)
                    whereClause += $" AND UserId like ''{mod.UserId}''";
                if (mod.ModuleId != default)
                    whereClause += $" AND ModuleId={mod.ModuleId}";
                if (mod.StatusId != default)
                    whereClause += $" AND StatusId={mod.StatusId}";
                if (mod.User != default)
                    whereClause += $" AND User like ''{mod.User}''";
                if (mod.PriorityId != default)
                    whereClause += $" AND PriorityId={mod.PriorityId}";
                if (mod.TaskPriority != default)
                    whereClause += $" AND TaskPriority like ''{mod.TaskPriority}''";
                if (mod.Module != default)
                    whereClause += $" AND Module like ''{mod.Module}''";
                if (mod.Status != default)
                    whereClause += $" AND Status like ''{mod.Status}''";
                if (mod.SP != default)
                    whereClause += $" AND SP={mod.SP}";
                if (mod.Title != default)
                {
                    if (mod.Title != "")
                        whereClause += $" AND Title like ''{mod.Title}''";
                }
                if (mod.Description != default)
                {
                    if (mod.Description != "")
                        whereClause += $" AND Description like ''{mod.Description}''";
                }
                var result = _taskDAL.SearchTasks (whereClause, TaskReturnTypes.UserTask.ToString ());
                tasks = result.UserTasks;
                tasks = FilterUserTasks (tasks);
                whereClause = "where 1=1";
                foreach (var line in tasks)
                {
                    float y = 100;
                    if (line.ClaimPercent > 0)
                    {
                        if (line.ApprovedClaimId > 0)
                            line.ClaimPercent = line.ApprovedClaim;
                    }
                    else
                    {
                        if (line.ApprovedClaimId > 0)
                            line.LastClaim = line.ApprovedClaim;
                        line.ClaimId = line.LastClaimId;
                        line.ClaimPercent = line.LastClaim;
                    }
                    line.RemainingSPs = Math.Round (line.SP - ((line.ClaimPercent / y) * line.SP), 2);
                }

                #endregion

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
            return tasks;
        }
        public List<TaskDE> SearchTasks ( TaskSearchCriteria mod )
        {
            List<TaskDE> tasks = new List<TaskDE> ();
            List<AttachmentsDE> attchmt = new List<AttachmentsDE> ();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                #region Search

                string whereClause;

                whereClause = "where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.ModuleId != default)
                    whereClause += $" AND ModuleId={mod.ModuleId}";
                if (mod.StatusId != default)
                    whereClause += $" AND StatusId={mod.StatusId}";
                if (mod.User != default)
                    whereClause += $" AND User like ''{mod.User}''";
                if (mod.PriorityId != default)
                    whereClause += $" AND PriorityId={mod.PriorityId}";
                if (mod.TaskPriority != default)
                    whereClause += $" AND TaskPriority like ''{mod.TaskPriority}''";
                if (mod.Module != default)
                    whereClause += $" AND Module like ''{mod.Module}''";
                if (mod.Status != default)
                    whereClause += $" AND Status like ''{mod.Status}''";
                if (mod.SP != default)
                    whereClause += $" AND SP={mod.SP}";
                if (mod.Title != default)
                {
                    if (mod.Title != "")
                        whereClause += $" AND Title like ''{mod.Title}''";
                }
                if (mod.Description != default)
                {
                    if (mod.Description != "")
                        whereClause += $" AND Description like ''{mod.Description}''";
                }
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
                }
                else
                {
                    if (mod.UserId != default)
                        whereClause += $" AND UserId like ''{mod.UserId}''";
                }
                var result = _taskDAL.SearchTasks (whereClause, TaskReturnTypes.Task.ToString ());
                tasks = result.Tasks;
                tasks = FilterTasks (tasks);
                whereClause = "where 1=1";
                foreach (var line in tasks)
                {
                    line.Attachments = _taskDAL.SearchAttachments (whereClause += $" AND TaskId={line.Id} and ClientId=" + line.ClientId + "");
                }

                #endregion

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
            return tasks;
        }
        public List<TaskDE> FilterTasks ( List<TaskDE> tasks )
        {
            var retVal = tasks
           .GroupBy (t => new { t.Id })
           .Select (group => group.OrderByDescending (t => t.UserTaskId).First ())
           .ToList ();
            //retVal = retVal.Where (x => x.ClaimId != 1013001).ToList ();
            return retVal;
        }
        public List<UserTaskVM> FilterUserTasks ( List<UserTaskVM> tasks )
        {
            var retVal = tasks
           .GroupBy (t => new { t.Id })
           .Select (group => group.OrderByDescending (t => t.UserTaskId).First ())
           .ToList ();
            //retVal = retVal.Where (x => x.ClaimId != 1013001).ToList ();
            return retVal;
        }

        #endregion
    }
}
