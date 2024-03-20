using Dapper;
using MySql.Data.MySqlClient;
using QST.MicroERP.Core.Constants;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL
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
                cmd.CommandText = SPNames.CTL_Manage_Item.ToString ();
                cmd.Parameters.AddWithValue("@prm_id", item.Id);
                cmd.Parameters.AddWithValue("@prm_clientId", item.ClientId);
                cmd.Parameters.AddWithValue("@prm_moduleId", item.ModuleId);
                cmd.Parameters.AddWithValue("@prm_vendorId", item.VendorId);
                cmd.Parameters.AddWithValue("@prm_typeId", item.TypeId);
                cmd.Parameters.AddWithValue("@prm_name", item.Name);
                cmd.Parameters.AddWithValue("@prm_purRate", item.PurRate);
                cmd.Parameters.AddWithValue("@prm_saleRate", item.SaleRate);
                cmd.Parameters.AddWithValue("@prm_conversion", item.Conversion);
                cmd.Parameters.AddWithValue("@prm_gstSaleRate", item.GstSaleRate);
                cmd.Parameters.AddWithValue("@prm_retailRate", item.RetailRate);
                cmd.Parameters.AddWithValue("@prm_gstPurRate", item.GstPurRate);
                cmd.Parameters.AddWithValue("@prm_saleStRate", item.SaleStRate);
                cmd.Parameters.AddWithValue("@prm_purStRate", item.PurStRate);
                cmd.Parameters.AddWithValue("@prm_purUnit", item.PurUnits);
                cmd.Parameters.AddWithValue("@prm_saleUnit", item.SaleUnits);
                cmd.Parameters.AddWithValue("@prm_extraRate", item.ExtraRate);
                cmd.Parameters.AddWithValue("@prm_prMazdoori", item.PrMazdoori);             
                cmd.Parameters.AddWithValue("@prm_unitPrice", item.UnitPrice);
                cmd.Parameters.AddWithValue("@prm_unitsInStock", item.UnitsInStock);
                cmd.Parameters.AddWithValue("@prm_manufacturerId", item.ManufacturersId);
                cmd.Parameters.AddWithValue("@prm_categoryId", item.CategoryId);
                cmd.Parameters.AddWithValue("@prm_formula", item.Formula);
                cmd.Parameters.AddWithValue("@prm_remarks", item.Remarks);
                cmd.Parameters.AddWithValue("@prm_createdOn", item.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdById", item.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", item.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedById", item.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", item.IsActive);
                cmd.Parameters.AddWithValue("@prm_DBoperation", item.DBoperation.ToString());

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
                top = cmd.Connection.Query<ItemDE>("call "+SPNames.CTL_Search_Item.ToString () + "( '" + whereClause + "')").ToList();
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
