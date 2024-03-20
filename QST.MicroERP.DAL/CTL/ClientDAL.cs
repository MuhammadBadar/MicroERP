using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL
{
    public class ClientDAL
    {
        #region DbOperations
        public bool ManageClients(ClientDE _clients, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = SPNames.CTL_Manage_Client;
                cmd.Parameters.AddWithValue("prm_id", _clients.Id);
                cmd.Parameters.AddWithValue("prm_moduleIds", _clients.ModuleIds);
                cmd.Parameters.AddWithValue("prm_userId", _clients.UserId);
                cmd.Parameters.AddWithValue("prm_ClientName", _clients.ClientName);
                cmd.Parameters.AddWithValue("prm_categoryId", _clients.CategoryId);
                cmd.Parameters.AddWithValue("prm_address", _clients.Address);
                cmd.Parameters.AddWithValue("prm_cityId", _clients.CityId);
                cmd.Parameters.AddWithValue("prm_countryId", _clients.CountryId);
                cmd.Parameters.AddWithValue("prm_contact", _clients.Contact);
                cmd.Parameters.AddWithValue("prm_owner", _clients.Owner);
                cmd.Parameters.AddWithValue("prm_releventPerson", _clients.RelevantPerson);
                cmd.Parameters.AddWithValue("prm_createdOn", _clients.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", _clients.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", _clients.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", _clients.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", _clients.IsActive);
                cmd.Parameters.AddWithValue("prm_DbOperation", _clients.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<ClientDE> SearchClients(string WhereClause, MySqlCommand cmd)
        {
            bool closeConnection = false;
            //WhereClause = string.Empty;
            List<ClientDE> clients = new List<ClientDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                clients = cmd.Connection.Query<ClientDE>("call "+SPNames.CTL_Serach_Client+"('" + WhereClause + "')").ToList();
                return clients;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        #endregion
    }
}
