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
    public class NotificationTemplateDAL
    {
        #region Operations

        public bool ManageNotificationTemplate(NotificationTemplateDE _nTem, MySqlCommand cmd = null)
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
                cmd.CommandText = SPNames.NTF_Manage_NotificationTemplate.ToString ();
                cmd.Parameters.AddWithValue("@prm_id", _nTem.Id);
                cmd.Parameters.AddWithValue("@prm_keyCode", _nTem.KeyCode);
                cmd.Parameters.AddWithValue("@prm_templateName", _nTem.TemplateName);
                cmd.Parameters.AddWithValue("@prm_body", _nTem.Body);
                cmd.Parameters.AddWithValue("@prm_subject", _nTem.Subject);
                cmd.Parameters.AddWithValue("@prm_sMS", _nTem.SMS);
                cmd.Parameters.AddWithValue("@prm_createdOn", _nTem.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdById", _nTem.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", _nTem.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedById", _nTem.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", _nTem.IsActive);
                cmd.Parameters.AddWithValue("@prm_DBoperation", _nTem.DBoperation.ToString());

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
        public List<NotificationTemplateDE> SearchNotificationTemplates(string whereClause, MySqlCommand cmd = null)
        {
            List<NotificationTemplateDE> top = new List<NotificationTemplateDE>();
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
                top = cmd.Connection.Query<NotificationTemplateDE>("call "+SPNames.NTF_Search_NotificationTemplate.ToString () + "( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception exp)
            {

                return top;
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
