using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL
{
    public class SaleDAL
    {
        #region Header Operations
        public bool ManageSale(SaleDE sale, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageSale";
                cmd.Parameters.AddWithValue("@id", sale.Id);
                cmd.Parameters.AddWithValue("@invNo", sale.InvNo);
                cmd.Parameters.AddWithValue("@Date", sale.Date);
                cmd.Parameters.AddWithValue("supplierId", sale.SupplierId);
                cmd.Parameters.AddWithValue ("@custId", sale.CustId);
                cmd.Parameters.AddWithValue("@salesmanId", sale.SalesmanId);
                cmd.Parameters.AddWithValue("@acId", sale.AcId);
                cmd.Parameters.AddWithValue("@gross", sale.Gross);
                cmd.Parameters.AddWithValue("@discount", sale.Discount);
                cmd.Parameters.AddWithValue("@gst", sale.Gst);
                cmd.Parameters.AddWithValue("@debit", sale.Debit);
                cmd.Parameters.AddWithValue("@credit", sale.Credit);
                cmd.Parameters.AddWithValue ("@statusId", sale.StatusId);
                cmd.Parameters.AddWithValue ("@packChrgs", sale.PackChrgs);
                cmd.Parameters.AddWithValue ("@freightChrgs", sale.FreightChrgs);
                cmd.Parameters.AddWithValue ("@netPayable", sale.NetPayable);
                cmd.Parameters.AddWithValue ("@description", sale.Description);
                cmd.Parameters.AddWithValue ("@isPosted", sale.IsPosted);
                cmd.Parameters.AddWithValue("@createdOn", sale.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", sale.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", sale.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", sale.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", sale.IsActive);
                cmd.Parameters.AddWithValue("@clientId", sale.ClientId);
                cmd.Parameters.AddWithValue("@DBoperation", sale.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
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
        public List<SaleDE> SearchSale(string whereClause, MySqlCommand cmd = null)
        {
            List<SaleDE> top = new List<SaleDE>();
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
                top = cmd.Connection.Query<SaleDE>("call QST.MicroERP.SearchSale( '" + whereClause + "')").ToList();
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
        #region Line Operations
        public bool ManageSaleLine(SaleLineDE saleLine, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageSaleLine";
                cmd.Parameters.AddWithValue("@id", saleLine.Id);
                cmd.Parameters.AddWithValue("@saleId", saleLine.SaleId);
                cmd.Parameters.AddWithValue("@productAttribIds", saleLine.ProductAttribIds);
                cmd.Parameters.AddWithValue ("@itemVariantId", saleLine.ItemVariantId);
                cmd.Parameters.AddWithValue ("@saleUnitId", saleLine.SaleUnitId);
                cmd.Parameters.AddWithValue("@proId", saleLine.ProductId);
                cmd.Parameters.AddWithValue("@issueQty", saleLine.IssueQty);
                cmd.Parameters.AddWithValue("@saleRate", saleLine.SaleRate);
                cmd.Parameters.AddWithValue("@retQty", saleLine.RetQty);
                cmd.Parameters.AddWithValue("@saleQty", saleLine.SaleQty);
                cmd.Parameters.AddWithValue("@discRate", saleLine.DiscRate);
                cmd.Parameters.AddWithValue("@gSTRate", saleLine.GSTRate);
                cmd.Parameters.AddWithValue("@gSTRetailRate", saleLine.GSTRetailRate);
                cmd.Parameters.AddWithValue("@retailRate", saleLine.RetailRate);
                cmd.Parameters.AddWithValue("@amount", saleLine.Amount);
                cmd.Parameters.AddWithValue ("@fTaxRate", saleLine.FTaxRate);
                cmd.Parameters.AddWithValue ("@whtRate", saleLine.WhtRate);
                cmd.Parameters.AddWithValue ("@disc", saleLine.Disc);
                cmd.Parameters.AddWithValue ("@bulkDisc", saleLine.BulkDisc);
                cmd.Parameters.AddWithValue ("@qtyDisc", saleLine.QtyDisc);
                cmd.Parameters.AddWithValue ("@gST", saleLine.GST);
                cmd.Parameters.AddWithValue ("@gSTRet", saleLine.GSTRet);
                cmd.Parameters.AddWithValue ("@fTax", saleLine.FTax);
                cmd.Parameters.AddWithValue ("@wht", saleLine.Wht);
                cmd.Parameters.AddWithValue ("@chrgsAdd", saleLine.ChrgsAdd);
                cmd.Parameters.AddWithValue ("@chrgsLess", saleLine.ChrgsLess);
                cmd.Parameters.AddWithValue("@Description", saleLine.Description);
                cmd.Parameters.AddWithValue("@createdOn", saleLine.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", saleLine.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", saleLine.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", saleLine.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", saleLine.IsActive);
                cmd.Parameters.AddWithValue("@clientId", saleLine.ClientId);
                cmd.Parameters.AddWithValue("@DBoperation", saleLine.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
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
        public List<SaleLineDE> SearchSaleLine(string whereClause, MySqlCommand cmd = null)
        {
            List<SaleLineDE> top = new List<SaleLineDE>();
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
                top = cmd.Connection.Query<SaleLineDE>("call QST.MicroERP.SearchSaleLine( '" + whereClause + "')").ToList();
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
