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
    public class SaleStockDAL
    {
        #region Operations
        public bool ManageSaleStock(SaleStockDE saleStk, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageSaleStock";
                cmd.Parameters.AddWithValue("@id", saleStk.Id);
                cmd.Parameters.AddWithValue("@saleTransId", saleStk.SaleTransId);
                cmd.Parameters.AddWithValue("@productId", saleStk.ProductId);
                cmd.Parameters.AddWithValue("@godownId", saleStk.GodownId);
                cmd.Parameters.AddWithValue("@debitId", saleStk.DebitId);
                cmd.Parameters.AddWithValue("@jobId", saleStk.JobId);
                cmd.Parameters.AddWithValue("@purchaseQty", saleStk.PurchaseQty);
                cmd.Parameters.AddWithValue("@issueQty", saleStk.IssueQty);
                cmd.Parameters.AddWithValue("@returnQty", saleStk.ReturnQty);
                cmd.Parameters.AddWithValue("@saleQty", saleStk.SaleQty);
                cmd.Parameters.AddWithValue("@freeQty", saleStk.FreeQty);
                cmd.Parameters.AddWithValue("@saleRate", saleStk.SaleRate);
                cmd.Parameters.AddWithValue("@saleGstRate", saleStk.SaleGstRate);
                cmd.Parameters.AddWithValue("@returnGstRate", saleStk.ReturnGstRate);
                cmd.Parameters.AddWithValue("@discRate", saleStk.DiscRate);
                cmd.Parameters.AddWithValue("@discAmt", saleStk.DiscAmt);
                cmd.Parameters.AddWithValue("@extraRate", saleStk.ExtraRate);
                cmd.Parameters.AddWithValue("@description", saleStk.Description);
                cmd.Parameters.AddWithValue("@retailRate", saleStk.RetailRate);
                cmd.Parameters.AddWithValue("@soNo", saleStk.SoNo);
                cmd.Parameters.AddWithValue("@createdOn", saleStk.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", saleStk.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", saleStk.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", saleStk.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", saleStk.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", saleStk.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<SaleStockDE> SearchSaleStocks(string whereClause, MySqlCommand cmd = null)
        {
            List<SaleStockDE> top = new List<SaleStockDE>();
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
                top = cmd.Connection.Query<SaleStockDE>("call QST.MicroERP.SearchSaleStock( '" + whereClause + "')").ToList();
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
