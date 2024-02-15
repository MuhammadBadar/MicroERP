using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MicroERP.Core.Entities;
using MySql.Data.MySqlClient;

namespace MicroERP.DAL
{
    public class ClientsDAL
    {
        #region DbOperations
        public bool ManageClients(ClientsDE _clients, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageClients";
                cmd.Parameters.AddWithValue("id", _clients.Id);
                cmd.Parameters.AddWithValue ("moduleIds", _clients.ModuleIds);
                cmd.Parameters.AddWithValue ("userId", _clients.UserId);
                cmd.Parameters.AddWithValue("clientsName", _clients.ClientName);
                cmd.Parameters.AddWithValue("categoryId", _clients.CategoryId);
                cmd.Parameters.AddWithValue("address", _clients.Address);
                cmd.Parameters.AddWithValue("cityId", _clients.CityId);
                cmd.Parameters.AddWithValue("countryId", _clients.CountryId);
                cmd.Parameters.AddWithValue("contact", _clients.Contact);
                cmd.Parameters.AddWithValue("owner", _clients.Owner);
                cmd.Parameters.AddWithValue("releventPerson", _clients.RelevantPerson);
                cmd.Parameters.AddWithValue("createdOn", _clients.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", _clients.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", _clients.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", _clients.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", _clients.IsActive);
                cmd.Parameters.AddWithValue("DbOperation", _clients.DBoperation.ToString());
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
        public List<ClientsDE> SearchClients(string WhereClause, MySqlCommand cmd)
        {
            bool closeConnection = false;
            //WhereClause = string.Empty;
            List<ClientsDE> clients = new List<ClientsDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                clients = cmd.Connection.Query<ClientsDE>("call MicroERP.SearchClients('" + WhereClause + "')").ToList();
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
