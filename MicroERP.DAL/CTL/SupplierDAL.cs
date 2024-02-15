using Dapper;
using MySql.Data.MySqlClient;
using MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL
{
    public class SupplierDAL
    {
        #region Operations

        public bool ManageSupplier(SupplierDE suplr, MySqlCommand? cmd )
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
                cmd.CommandText = "ManageSupplier";
                cmd.Parameters.AddWithValue("@id", suplr.Id);
                cmd.Parameters.AddWithValue("@accId", suplr.AccId);
                cmd.Parameters.AddWithValue("@isCustomer", suplr.IsCustomer);
                cmd.Parameters.AddWithValue("@customerId", suplr.CustomerId);
                cmd.Parameters.AddWithValue("@DiscRate", suplr.DiscRate);
                cmd.Parameters.AddWithValue("@contactName", suplr.ContactName);
                cmd.Parameters.AddWithValue("@companyName", suplr.CompanyName);
                cmd.Parameters.AddWithValue("@phone", suplr.Phone);
                cmd.Parameters.AddWithValue("@countryId", suplr.CountryId);
                cmd.Parameters.AddWithValue("@cityId", suplr.CityId);
                cmd.Parameters.AddWithValue("@address", suplr.Address);
                cmd.Parameters.AddWithValue("@createdOn", suplr.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", suplr.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", suplr.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", suplr.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", suplr.IsActive);
                cmd.Parameters.AddWithValue("@clientId", suplr.ClientId);
                cmd.Parameters.AddWithValue("@DBoperation", suplr.DBoperation.ToString());

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
        public List<SupplierDE> SearchSuppliers(string whereClause, MySqlCommand cmd = null)
        {
            List<SupplierDE> top = new List<SupplierDE>();
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
                top = cmd.Connection.Query<SupplierDE>("call MicroERP.SearchSupplier( '" + whereClause + "')").ToList();
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
