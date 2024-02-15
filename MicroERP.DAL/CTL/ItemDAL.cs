using Dapper;
using MySql.Data.MySqlClient;
using MicroERP.Core.Entities;
using MicroERP.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL
{
    public class ItemDAL
    {
        #region Operations
        public bool ManageItem(ItemDE item, MySqlCommand? cmd =null)
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
                cmd.CommandText = "ManageItem";
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue ("@clientId", item.ClientId);
                cmd.Parameters.AddWithValue ("@moduleId", item.ModuleId);
                cmd.Parameters.AddWithValue("@vendorId", item.VendorId);
                cmd.Parameters.AddWithValue("@typeId", item.TypeId);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@purRate", item.PurRate);
                cmd.Parameters.AddWithValue("@saleRate", item.SaleRate);
                cmd.Parameters.AddWithValue("@conversion", item.Conversion);
                cmd.Parameters.AddWithValue("@gstSaleRate", item.GstSaleRate);
                cmd.Parameters.AddWithValue("@retailRate", item.RetailRate);
                cmd.Parameters.AddWithValue("@gstPurRate", item.GstPurRate);
                cmd.Parameters.AddWithValue("@saleStRate", item.SaleStRate);
                cmd.Parameters.AddWithValue("@purStRate", item.PurStRate);
                cmd.Parameters.AddWithValue("@purUnit", item.PurUnits);
                cmd.Parameters.AddWithValue("@saleUnit", item.SaleUnits);
                cmd.Parameters.AddWithValue("@extraRate", item.ExtraRate);
                cmd.Parameters.AddWithValue("@prMazdoori", item.PrMazdoori);             
                cmd.Parameters.AddWithValue("@unitPrice", item.UnitPrice);
                cmd.Parameters.AddWithValue("@unitsInStock", item.UnitsInStock);
                cmd.Parameters.AddWithValue("@manufacturerId", item.ManufacturersId);
                cmd.Parameters.AddWithValue("@categoryId", item.CategoryId);
                cmd.Parameters.AddWithValue("@formula", item.Formula);
                cmd.Parameters.AddWithValue("@remarks", item.Remarks);
                cmd.Parameters.AddWithValue("@createdOn", item.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", item.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", item.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", item.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", item.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", item.DBoperation.ToString());

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
        public List<ItemDE> SearchItems(string whereClause, MySqlCommand? cmd = null)
        {
            List<ItemDE> top = new List<ItemDE>();
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
                top = cmd.Connection.Query<ItemDE>("call MicroERP.SearchItem( '" + whereClause + "')").ToList();
                return top;
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

        #endregion
    }
}
