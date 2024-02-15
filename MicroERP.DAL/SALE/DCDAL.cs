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
    public class DCDAL
    {
        #region Header Operations
        public bool ManageDC(DCDE dc, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageDC";
                cmd.Parameters.AddWithValue("@id", dc.Id);
                cmd.Parameters.AddWithValue("@acId", dc.AcId);
                cmd.Parameters.AddWithValue("@invNo", dc.InvNo);
                cmd.Parameters.AddWithValue("@custId", dc.CustId);
                cmd.Parameters.AddWithValue("@date", dc.Date);
                cmd.Parameters.AddWithValue("@createdOn", dc.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", dc.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", dc.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", dc.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", dc.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", dc.DBoperation.ToString());

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
        public bool ManageDCDetail(DCDetailDE dcDetail, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageDCDetail";
                cmd.Parameters.AddWithValue("@id", dcDetail.Id);
                cmd.Parameters.AddWithValue("@productId", dcDetail.ProductId);
                cmd.Parameters.AddWithValue("@qty", dcDetail.Qty);
                cmd.Parameters.AddWithValue("@description", dcDetail.Description);
                cmd.Parameters.AddWithValue("@dCId", dcDetail.DCId);
                cmd.Parameters.AddWithValue("@createdOn", dcDetail.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", dcDetail.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", dcDetail.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", dcDetail.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", dcDetail.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", dcDetail.DBoperation.ToString());

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
        #region Search DC
        public List<DCDE> SearchDC(string whereClause, MySqlCommand cmd = null)
        {
            List<DCDE> top = new List<DCDE>();
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
                top = cmd.Connection.Query<DCDE>("call MicroERP.SearchDC( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception exp)
            {
                return top;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<DCDetailDE> SearchDCDetail(string whereClause, MySqlCommand cmd = null)
        {
            List<DCDetailDE> top = new List<DCDetailDE>();
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
                top = cmd.Connection.Query<DCDetailDE>("call MicroERP.SearchDCDetail( '" + whereClause + "')").ToList();
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
