using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.SearchCriteria;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL.TMS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;
using System.Data;

namespace QST.MicroERP.Service.TMS
{
    public class TaskCommentService:BaseService
    {

        #region Class Members/Class Variables

        private TaskCommentDAL _taskDAL;

        #endregion
        #region Constructors
        public TaskCommentService ( )
        {
            _taskDAL = new TaskCommentDAL ();
        }

        #endregion
        #region TaskComment
        public bool ManagementTaskComment ( TaskCommentDE mod )
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                _entity = TableNames.TMS_TaskComment.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                {
                    mod.Id = _coreDAL.GetNextIdByClient (TableNames.TMS_TaskComment.ToString (), mod.ClientId, "ClientId");
                    mod.Time = DateTime.Now;
                }

                _logger.Info ($"Going to Call:_taskDAL.ManageTaskComment (mod)");
                if (_taskDAL.ManageTaskComment (mod))
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    return true;
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                    return false;
                }
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
        public List<TaskCommentVM> SearchTaskComments ( TaskCommentSearchCriteria mod )
        {
            bool closeConnectionFlag = false;
            List<TaskCommentVM> Task = new List<TaskCommentVM> ();
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.TaskId != default)
                    whereClause += $" AND TaskId={mod.TaskId}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.Comment != default)
                    whereClause += $" AND Comment like ''{mod.Comment}''";
                if (mod.User != default)
                    whereClause += $" AND User like ''{mod.User}''";
                if (mod.TaskTitle != default)
                    whereClause += $" AND TaskTitle like ''{mod.TaskTitle}''";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                Task = _taskDAL.SearchTaskComments (whereClause);

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
            return Task;
        }

        #endregion

    }
}
