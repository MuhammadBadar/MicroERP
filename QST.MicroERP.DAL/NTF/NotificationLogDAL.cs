using Dapper;
using QST.MicroERP.Core.Entities.NTF;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL.NTF
{
    public class NotificationLogDAL
    {
        #region Operations

        public bool ManageNotificationLog(NotificationLogDE log, MySqlCommand cmd = null)
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
                cmd.CommandText = "NTF_Manage_NotificationLog";
                cmd.Parameters.AddWithValue("@prm_Id", log.Id);
                cmd.Parameters.AddWithValue("@prm_ClientId", log.ClientId);
                cmd.Parameters.AddWithValue("@prm_UserId", log.UserId);
                cmd.Parameters.AddWithValue("@prm_KeyCode", log.KeyCode);
                cmd.Parameters.AddWithValue("@prm_SMS", log.SMS);
                cmd.Parameters.AddWithValue("@prm_IsSent", log.IsSent);
                cmd.Parameters.AddWithValue("@prm_Subject", log.Subject);
                cmd.Parameters.AddWithValue("@prm_Body", log.Body);
                cmd.Parameters.AddWithValue("@prm_CreatedOn", log.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_CreatedById", log.CreatedById);
                cmd.Parameters.AddWithValue("@prm_ModifiedOn", log.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_ModifiedById", log.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_IsActive", log.IsActive);
                cmd.Parameters.AddWithValue("@prm_Filter", log.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public bool AlterNotificationLog(NotificationLogDE NotificationLog, int? Id = null, MySqlCommand cmd = null)
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
                cmd.CommandText = "AlterNotificationLog";
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@filter", NotificationLog.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<NotificationLogDE> SearchNotificationLogs(string whereClause, MySqlCommand cmd = null)
        {
            List<NotificationLogDE> top = new List<NotificationLogDE>();
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
                top = cmd.Connection.Query<NotificationLogDE>("call NTF_Search_NotificationLog( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception exp)
            {
                throw exp;
                //return top;
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
