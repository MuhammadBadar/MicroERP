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
                cmd.CommandText = "Managetask";
                cmd.Parameters.AddWithValue("@id", task.Id);
                cmd.Parameters.AddWithValue("@userId", task.UserId);
                cmd.Parameters.AddWithValue("@moduleId", task.ModuleId);
                cmd.Parameters.AddWithValue("@statusId", task.StatusId);
                cmd.Parameters.AddWithValue("@priorityId", task.PriorityId);
                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@sp", task.SP);
                cmd.Parameters.AddWithValue("@description", task.Description);
                cmd.Parameters.AddWithValue("@reason", task.Reason);
                cmd.Parameters.AddWithValue("@createdOn", task.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", task.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", task.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", task.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", task.IsActive);
                cmd.Parameters.AddWithValue("@filter", task.DBoperation.ToString());

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



                cmd.CommandText = "ManageAttachments";
                cmd.Parameters.AddWithValue("@id", atchmnt.Id);
                cmd.Parameters.AddWithValue("@taskId", atchmnt.TaskId);
                cmd.Parameters.AddWithValue("@name", atchmnt.Name);
                cmd.Parameters.AddWithValue("@size", atchmnt.Size);
                cmd.Parameters.AddWithValue("@docPath", atchmnt.DocPath);
                cmd.Parameters.AddWithValue("@base64File", atchmnt.Base64File);
                cmd.Parameters.AddWithValue("@createdOn", atchmnt.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", atchmnt.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", atchmnt.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", atchmnt.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", atchmnt.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", atchmnt.DBoperation.ToString());

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

                top = cmd.Connection.Query<AttachmentsDE>("call SearchAttachments( '" + whereClause + "'  ) ").ToList();
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
                    result.Tasks = cmd.Connection.Query<TaskDE>("call TMS_Search_Tasks( '" + whereClause + "'  ) ").ToList();
                    return result;
                }
                else
                {
                    result.UserTasks = cmd.Connection.Query<UserTaskVM>("call TMS_Search_Tasks( '" + whereClause + "'  ) ").ToList();
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
