using Dapper;
using MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL
{
    public class PurchaseDAL
    {
        #region Header Operations
        public bool ManagePurchase(PurchaseDE prch, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManagePurchase";
                cmd.Parameters.AddWithValue("@id", prch.Id);
                cmd.Parameters.AddWithValue("@invNo", prch.InvNo);
                cmd.Parameters.AddWithValue("@Date", prch.Date);
                cmd.Parameters.AddWithValue("@SupplierNameId", prch.SupplierId);
                cmd.Parameters.AddWithValue("@AcId", prch.AcId);
                cmd.Parameters.AddWithValue("@gross", prch.Gross);
                cmd.Parameters.AddWithValue("@discount", prch.Discount);
                cmd.Parameters.AddWithValue("@gst", prch.Gst);
                cmd.Parameters.AddWithValue("@debit", prch.Debit);
                cmd.Parameters.AddWithValue("@credit", prch.Credit);
                cmd.Parameters.AddWithValue ("@statusId", prch.StatusId);
                cmd.Parameters.AddWithValue ("@description", prch.Description);
                cmd.Parameters.AddWithValue ("@isPosted", prch.IsPosted);
                cmd.Parameters.AddWithValue("@createdOn", prch.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", prch.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", prch.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", prch.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", prch.IsActive);
                cmd.Parameters.AddWithValue("@clientId", prch.ClientId);
                cmd.Parameters.AddWithValue("@DBoperation", prch.DBoperation.ToString());

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
        public List<PurchaseDE> SearchPurchase(string whereClause, MySqlCommand cmd = null)
        {
            List<PurchaseDE> top = new List<PurchaseDE>();
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
                top = cmd.Connection.Query<PurchaseDE>("call MicroERP.SearchPurchase( '" + whereClause + "')").ToList();
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
        public bool ManagePurchaseLine(PurchaseLineDE prchLine, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManagePurchaseLine";
                cmd.Parameters.AddWithValue("@id", prchLine.Id);
                cmd.Parameters.AddWithValue("@prchId", prchLine.PrchId);
                cmd.Parameters.AddWithValue("@proNameId", prchLine.ProductId);
                cmd.Parameters.AddWithValue ("@purUnitId", prchLine.PurUnitId);
                cmd.Parameters.AddWithValue ("@productAttribIds", prchLine.ProductAttribIds);
                cmd.Parameters.AddWithValue ("@itemVariantId", prchLine.ItemVariantId);
                cmd.Parameters.AddWithValue("@qty", prchLine.Qty);
                cmd.Parameters.AddWithValue("@purchaseRate", prchLine.PurchaseRate);
                cmd.Parameters.AddWithValue("@discPer", prchLine.DiscPer);
                cmd.Parameters.AddWithValue("@gSTRate", prchLine.GSTRate);
                cmd.Parameters.AddWithValue("@gSTRetailRate", prchLine.GSTRetailRate);
                cmd.Parameters.AddWithValue("@retailRate", prchLine.RetailRate);
                cmd.Parameters.AddWithValue("@amount", prchLine.Amount);
                cmd.Parameters.AddWithValue("@Description", prchLine.Description);
                cmd.Parameters.AddWithValue("@createdOn", prchLine.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", prchLine.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", prchLine.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", prchLine.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", prchLine.IsActive);
                cmd.Parameters.AddWithValue("@clientId", prchLine.ClientId);
                cmd.Parameters.AddWithValue("@DBoperation", prchLine.DBoperation.ToString());

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
        public List<PurchaseLineDE> SearchPurchaseLine(string whereClause, MySqlCommand cmd = null)
        {
            List<PurchaseLineDE> top = new List<PurchaseLineDE>();
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
                top = cmd.Connection.Query<PurchaseLineDE>("call MicroERP.SearchPurchaseLine( '" + whereClause + "')").ToList();
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
