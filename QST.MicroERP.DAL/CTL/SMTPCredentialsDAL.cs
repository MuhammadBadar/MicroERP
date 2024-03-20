using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.DAL.IDAL;
using MySql.Data.MySqlClient;

namespace QST.MicroERP.DAL
{
    public class SMTPCredentialsDAL:IBaseDAL<SMTPCredentialsDE>
    {
        #region DbOperations
        public List<SMTPCredentialsDE> SearchData ( string whereClause, MySqlCommand? cmd )
        {
            bool closeConnection = false;
            List<SMTPCredentialsDE> SMTPCredentials = new List<SMTPCredentialsDE> ();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                SMTPCredentials = cmd.Connection.Query<SMTPCredentialsDE> ("call CTL_Search_SMTPCredentials('" + whereClause + "')").ToList ();
                return SMTPCredentials;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public bool ManageData ( SMTPCredentialsDE _smtp, MySqlCommand? cmd )
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                cmd.CommandText = "CTL_Manage_SMTPCredentials";
                cmd.Parameters.AddWithValue ("prm_id", _smtp.Id);
                cmd.Parameters.AddWithValue ("prm_clientId", _smtp.ClientId);
                cmd.Parameters.AddWithValue ("prm_server", _smtp.Server);
                cmd.Parameters.AddWithValue ("prm_port", _smtp.Port);
                cmd.Parameters.AddWithValue ("prm_userName", _smtp.UserName);
                cmd.Parameters.AddWithValue ("prm_password", _smtp.Password);
                cmd.Parameters.AddWithValue ("prm_createdOn", _smtp.CreatedOn);
                cmd.Parameters.AddWithValue ("prm_createdById", _smtp.CreatedById);
                cmd.Parameters.AddWithValue ("prm_modifiedOn", _smtp.ModifiedOn);
                cmd.Parameters.AddWithValue ("prm_modifiedById", _smtp.ModifiedById);
                cmd.Parameters.AddWithValue ("prm_isActive", _smtp.IsActive);
                cmd.Parameters.AddWithValue ("prm_DbOperation", _smtp.DBoperation.ToString ());
                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        #endregion
    }
}
