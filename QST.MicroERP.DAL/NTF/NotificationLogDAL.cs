using Dapper;
using QST.MicroERP.Core.Entities.NTF;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

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
                cmd.CommandText = SPNames.NTF_Manage_NotificationLog.ToString ();
                cmd.Parameters.AddWithValue("@prm_Id", log.Id);
                cmd.Parameters.AddWithValue("@prm_ClientId", log.ClientId);
                cmd.Parameters.AddWithValue("@prm_UserId", log.UserId);
                cmd.Parameters.AddWithValue("@prm_KeyCode", log.KeyCode);
                cmd.Parameters.AddWithValue("@prm_SMS", log.SMS);
                cmd.Parameters.AddWithValue("@prm_IsSent", log.IsSent);
                cmd.Parameters.AddWithValue("@prm_Subject", log.Subject);
                cmd.Parameters.AddWithValue("@prm_Body", log.Body);
                cmd.Parameters.AddWithValue ("@prm_From", log.From);
                cmd.Parameters.AddWithValue ("@prm_To", log.To);
                cmd.Parameters.AddWithValue("@prm_CreatedOn", log.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_CreatedById", log.CreatedById);
                cmd.Parameters.AddWithValue("@prm_ModifiedOn", log.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_ModifiedById", log.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_IsActive", log.IsActive);
                cmd.Parameters.AddWithValue("@prm_Filter", log.DBoperation.ToString());

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
                top = cmd.Connection.Query<NotificationLogDE>("call "+SPNames.NTF_Search_NotificationLog.ToString () + "( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception )
            {
                throw ;
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
