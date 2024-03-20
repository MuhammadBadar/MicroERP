using Dapper;
using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL.TMS
{
    public class TaskCommentDAL
    {
        #region Operations

        public bool ManageTaskComment(TaskCommentDE taskc, MySqlCommand cmd = null)
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                cmd.CommandText = SPNames.TMS_Manage_TaskComment.ToString ();
                cmd.Parameters.AddWithValue("@prm_id", taskc.Id);
                cmd.Parameters.AddWithValue("@prm_clientId", taskc.ClientId);
                cmd.Parameters.AddWithValue("@prm_taskId", taskc.TaskId);
                cmd.Parameters.AddWithValue("@prm_userId", taskc.UserId != null ? taskc.UserId : string.Empty);
                cmd.Parameters.AddWithValue("@prm_comment", taskc.Comment);
                cmd.Parameters.AddWithValue("@prm_time", taskc.Time);
                cmd.Parameters.AddWithValue("@prm_createdOn", taskc.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdById", taskc.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", taskc.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedById", taskc.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", taskc.IsActive);
                cmd.Parameters.AddWithValue("@prm_filter", taskc.DBoperation.ToString());

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<TaskCommentVM> SearchTaskComments(string whereClause, MySqlCommand cmd = null)
        {
            List<TaskCommentVM> top = new List<TaskCommentVM>();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                top = cmd.Connection.Query<TaskCommentVM>("call "+SPNames.TMS_Search_TaskComment.ToString () + "( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }

        #endregion
    }
}
