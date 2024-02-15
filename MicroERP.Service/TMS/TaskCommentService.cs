using MicroERP.Core.Entities.TMS;
using MicroERP.Core.Enums;
using MicroERP.Core.SearchCriteria;
using MicroERP.Core.ViewModel;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MicroERP.DAL.TMS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MicroERP.Service.TMS
{
    public class TaskCommentService
    {

        #region Class Members/Class Variables

        private TaskCommentDAL _taskDAL;
        private CoreDAL _corDAL;

        #endregion
        #region Constructors
        public TaskCommentService ( )
        {
            _taskDAL = new TaskCommentDAL ();
            _corDAL = new CoreDAL ();
        }

        #endregion
        #region TaskComment
        public bool ManagementTaskComment ( TaskCommentDE mod )
        {
            MySqlCommand cmd = null;
            try
            {
                bool check = true;
                cmd = MicroERPDataContext.OpenMySqlConnection ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId (TableNames.taskcomment.ToString ());
                check = _taskDAL.ManageTaskComment (mod);
                if (check == true)
                    mod.DBoperation = DBoperations.NA;
            }
            catch
            {
                MicroERPDataContext.CancelTransaction (cmd);
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return true;

        }
        public List<TaskCommentVM> SearchTaskComments ( TaskCommentSearchCriteria mod )
        {
            List<TaskCommentVM> Task = new List<TaskCommentVM> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                MicroERPDataContext.StartTransaction (cmd);

                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.TaskId != default)
                    whereClause += $" AND TaskId={mod.TaskId}";
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

                MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception exp)
            {
                MicroERPDataContext.CancelTransaction (cmd);
                throw exp;
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
