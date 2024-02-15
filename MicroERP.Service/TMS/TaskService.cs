

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MicroERP.DAL;
using MicroERP.Core.Enums;
using MicroERP.Core.ViewModel;
using MicroERP.Core.SearchCriteria;
using MicroERP.Service.SEC;
using MicroERP.DAL.TMS;
using MicroERP.Core.Entities.TMS;
using MicroERP.DAL.CTL;
using MicroERP.Core.Entities.SEC;

namespace MicroERP.Service.TMS
{
    public class TaskService
    {

        #region Class Members/Class Variables
        private readonly string AppDirectory = Path.Combine (Directory.GetCurrentDirectory (), "wwwroot");
        private TaskDAL _taskDAL;
        private CoreDAL _corDAL;
        private UserService _userSvc;

        #endregion
        #region Constructors
        public TaskService ( )
        {
            _taskDAL = new TaskDAL ();
            _corDAL = new CoreDAL ();
            _userSvc = new UserService ();
        }

        #endregion
        #region Task
        public TaskDE ManagementTask ( TaskDE mod )
        {
            MySqlCommand cmd = null;
            int fileId = 0;
            try
            {
                mod.HasErrors = false;
                bool check = true;
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                MicroERPDataContext.StartTransaction (cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId (TableNames.task.ToString ());
                if (mod.Id == 1)
                    mod.Id = 1001;
                check = _taskDAL.ManageTask (mod);
                fileId= _corDAL.GetnextId (TableNames.attachments.ToString ());
                foreach (var file in mod.Attachments)
                {
                    if (!Directory.Exists (AppDirectory))
                        Directory.CreateDirectory (AppDirectory);
                    var FileName = DateTime.Now.Ticks.ToString () + Path.GetExtension (file.Name);
                    var path = Path.Combine (AppDirectory, FileName);
                    file.DocPath = path;
                    file.TaskId = mod.Id;
                    file.DBoperation = mod.DBoperation;
                    check = _taskDAL.ManageAttachments (file);
                    if (file.DBoperation == DBoperations.Insert)
                        fileId += 1;
                }
                if (check == true)
                {
                    mod.HasErrors = false;
                    mod.DBoperation = DBoperations.NA;
                }

                MicroERPDataContext.EndTransaction (cmd);
            }
            catch
            {
                mod.HasErrors = true;
                MicroERPDataContext.CancelTransaction (cmd);
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return mod;

        }
        public List<UserTaskVM> SearchUserTasks ( TaskSearchCriteria mod )
        {
            List<UserTaskVM> tasks = new List<UserTaskVM> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();

                #region Search

                string whereClause;
                //whereClause = "where 1=1 AND ClaimId <> 1013001";
                whereClause = "where 1=1 ";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
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
                        line.RemainingSPs = (float)Math.Round (line.SP - line.ClaimPercent / y * line.SP, 2);
                    }
                    else
                    {
                        if (line.ApprovedClaimId > 0)
                            line.LastClaim = line.ApprovedClaim;
                        line.ClaimId = line.LastClaimId;
                        line.ClaimPercent = line.LastClaim;
                        line.RemainingSPs = (float)Math.Round (line.SP - line.LastClaim / y * line.SP, 2);
                    }
                    line.Attachments = _taskDAL.SearchAttachments (whereClause += $" AND TaskId={line.Id}");
                }

                #endregion

            }
            catch (Exception)
            {
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
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                MicroERPDataContext.StartTransaction (cmd);

                #region Search

                string whereClause;

                whereClause = "where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
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
                if (mod.IncludeSubordinatesData && mod.UserId != default)
                {
                    var user = new UserDE ();
                    user.Id = mod.UserId;
                    var subordinateUsers = _userSvc.GetSubordinates (user);

                    if (subordinateUsers.Count > 0)
                    {
                        string subordinateIds = string.Join ("'',''", subordinateUsers.Select (x => x.Id));
                        whereClause += $" or UserId IN (''{subordinateIds}'')";
                    }
                }
                var result = _taskDAL.SearchTasks (whereClause, TaskReturnTypes.Task.ToString ());
                tasks = result.Tasks;
                tasks = FilterTasks (tasks);
                whereClause = "where 1=1";
                foreach (var line in tasks)
                {
                    line.Attachments = _taskDAL.SearchAttachments (whereClause += $" AND TaskId={line.Id}");
                }

                #endregion

                MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception exp)
            {
                MicroERPDataContext.CancelTransaction (cmd);
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
