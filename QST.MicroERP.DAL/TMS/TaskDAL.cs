using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Http.Headers;
using QST.MicroERP.DAL;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.Entities.TMS;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL.TMS
{
    public class TaskDAL
    {
        public class SearchResult
        {
            public SearchResult()
            {
                UserTasks = new List<UserTaskVM>();
                Tasks = new List<TaskDE>();
            }
            public List<UserTaskVM> UserTasks { get; set; }
            public List<TaskDE> Tasks { get; set; }
        }
        #region Operations

        public bool ManageTask(TaskDE task, MySqlCommand cmd = null)
        {
            bool retVal = false;
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
                cmd.CommandText = SPNames.TMS_Manage_Task.ToString ();
                cmd.Parameters.AddWithValue("@prm_id", task.Id);
                cmd.Parameters.AddWithValue("@prm_clientId", task.ClientId);
                cmd.Parameters.AddWithValue("@prm_userId", task.UserId);
                cmd.Parameters.AddWithValue("@prm_moduleId", task.ModuleId);
                cmd.Parameters.AddWithValue("@prm_statusId", task.StatusId);
                cmd.Parameters.AddWithValue("@prm_priorityId", task.PriorityId);
                cmd.Parameters.AddWithValue("@prm_title", task.Title);
                cmd.Parameters.AddWithValue("@prm_sp", task.SP);
                cmd.Parameters.AddWithValue("@prm_description", task.Description);
                cmd.Parameters.AddWithValue("@prm_reason", task.Reason);
                cmd.Parameters.AddWithValue("@prm_createdOn", task.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdById", task.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", task.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedById", task.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", task.IsActive);
                cmd.Parameters.AddWithValue("@prm_filter", task.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                retVal = true;
                return retVal;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public bool ManageAttachments(AttachmentsDE atchmnt, MySqlCommand cmd = null)
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
                cmd.CommandText = SPNames.TMS_Manage_Attachment.ToString ();
                cmd.Parameters.AddWithValue("@prm_id", atchmnt.Id);
                cmd.Parameters.AddWithValue("@prm_clientId", atchmnt.ClientId);
                cmd.Parameters.AddWithValue("@prm_taskId", atchmnt.TaskId);
                cmd.Parameters.AddWithValue("@prm_name", atchmnt.Name);
                cmd.Parameters.AddWithValue("@prm_size", atchmnt.Size);
                cmd.Parameters.AddWithValue("@prm_docPath", atchmnt.DocPath);
                cmd.Parameters.AddWithValue("@prm_base64File", atchmnt.Base64File);
                cmd.Parameters.AddWithValue("@prm_createdOn", atchmnt.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdById", atchmnt.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", atchmnt.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedById", atchmnt.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", atchmnt.IsActive);
                cmd.Parameters.AddWithValue("@prm_DBoperation", atchmnt.DBoperation.ToString());

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<AttachmentsDE> SearchAttachments(string whereClause, MySqlCommand cmd = null)
        {
            // string filter = "FreeSearch";
            List<AttachmentsDE> top = new List<AttachmentsDE>();
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

                top = cmd.Connection.Query<AttachmentsDE>("call "+SPNames.TMS_Search_Attachment.ToString () + "( '" + whereClause + "'  ) ").ToList();
                return top;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public SearchResult SearchTasks(string whereClause, string retType, MySqlCommand cmd = null)
        {
            SearchResult result = new SearchResult();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection has been created");
                else
                    Console.WriteLine("Connection error");
                whereClause = " " + whereClause + " order by Id desc";
                if (retType == TaskReturnTypes.Task.ToString())
                {
                    result.Tasks = cmd.Connection.Query<TaskDE>("call "+SPNames.TMS_Search_Task.ToString () + "( '" + whereClause + "'  ) ").ToList();
                    return result;
                }
                else
                {
                    result.UserTasks = cmd.Connection.Query<UserTaskVM>("call "+SPNames.TMS_Search_Task.ToString () + "( '" + whereClause + "'  ) ").ToList();
                    return result;
                }
            }
            catch (Exception)
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
