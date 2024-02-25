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
    public class StockTransferDAL
    {
        #region Header Operations
        public bool ManageStockTransfer(StockTransferDE st, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageStockTransfer";
                cmd.Parameters.AddWithValue("@id", st.Id);
                cmd.Parameters.AddWithValue("@date", st.Date);
                cmd.Parameters.AddWithValue("@invNo", st.InvNo);
                cmd.Parameters.AddWithValue("@transferFrom", st.TransferFrom);
                cmd.Parameters.AddWithValue("@transferTo", st.TransferTo);
                cmd.Parameters.AddWithValue("@createdOn", st.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", st.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", st.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", st.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", st.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", st.DBoperation.ToString());

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
                cmd.Parameters.Clear();
            }
        }
        #endregion
        #region Line Operations
        public bool ManageStockTransferLine(StockTransferLineDE stLine, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageSTLine";
                cmd.Parameters.AddWithValue("@id", stLine.Id);
                cmd.Parameters.AddWithValue("@productId", stLine.ProductId);
                cmd.Parameters.AddWithValue("@godownId", stLine.GodownId);
                cmd.Parameters.AddWithValue("@sTId", stLine.STId);
                cmd.Parameters.AddWithValue("@qty", stLine.Qty);
                cmd.Parameters.AddWithValue("@description", stLine.Description);
                cmd.Parameters.AddWithValue("@productUnits", stLine.ProductUnits);
                cmd.Parameters.AddWithValue("@createdOn", stLine.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", stLine.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", stLine.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", stLine.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", stLine.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", stLine.DBoperation.ToString());

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
                cmd.Parameters.Clear();
            }
        }
        #endregion
        #region Search StockTransfer
        public List<StockTransferDE> SearchStockTransfers(string whereClause, MySqlCommand cmd = null)
        {
            List<StockTransferDE> top = new List<StockTransferDE>();
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
                top = cmd.Connection.Query<StockTransferDE>("call SearchStockTransfer( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<StockTransferLineDE> SearchStockTransferLine(string whereClause, MySqlCommand cmd = null)
        {
            List<StockTransferLineDE> top = new List<StockTransferLineDE>();
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
                top = cmd.Connection.Query<StockTransferLineDE>("call SearchSTLine( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception exp)
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
