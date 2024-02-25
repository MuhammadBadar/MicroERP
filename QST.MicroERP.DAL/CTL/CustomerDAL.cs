using Dapper;
using MySql.Data.MySqlClient;
using QST.MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL
{
    public class CustomerDAL
    {
        #region Operations
        public bool ManageCustomer(CustomerDE cust, MySqlCommand? cmd )
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
                cmd.CommandText = "ManageCustomer";
                cmd.Parameters.AddWithValue("@id", cust.Id);
                cmd.Parameters.AddWithValue("@accId", cust.AccId);
                cmd.Parameters.AddWithValue("@countryId", cust.CountryId);
                cmd.Parameters.AddWithValue("@name", cust.Name);
                cmd.Parameters.AddWithValue("@isSupplier", cust.IsSupplier);
                cmd.Parameters.AddWithValue("@supplierId", cust.SupplierId);
                cmd.Parameters.AddWithValue("@email", cust.Email);
                cmd.Parameters.AddWithValue("@phone", cust.Phone);
                cmd.Parameters.AddWithValue("@cityId", cust.CityId);
                cmd.Parameters.AddWithValue("@region", cust.Region);
                cmd.Parameters.AddWithValue("@sendEmail", cust.SendEmail);
                cmd.Parameters.AddWithValue("@address", cust.Address);
                cmd.Parameters.AddWithValue("@createdOn", cust.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", cust.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", cust.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", cust.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", cust.IsActive);
                cmd.Parameters.AddWithValue("@clientId", cust.ClientId);
                cmd.Parameters.AddWithValue("@DBoperation", cust.DBoperation.ToString());

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
                cmd.Parameters.Clear();
            }
        }
        public List<CustomerDE> SearchCustomers(string whereClause, MySqlCommand cmd = null)
        {
            List<CustomerDE> top = new List<CustomerDE>();
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
                top = cmd.Connection.Query<CustomerDE>("call SearchCustomer( '" + whereClause + "')").ToList();
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

        #endregion
    }
}
