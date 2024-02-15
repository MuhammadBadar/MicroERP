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
    public class VoucherDAL
    {
        #region Header Operations
        public bool ManageVoucher(VoucherDE vch, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageVoucher";
                cmd.Parameters.AddWithValue("@id", vch.Id);
                cmd.Parameters.AddWithValue("@vchTypeId", vch.VchTypeId);
                cmd.Parameters.AddWithValue("@vchNo", vch.VchNo);
                cmd.Parameters.AddWithValue("@invNo", vch.InvNo);
                cmd.Parameters.AddWithValue("@docNo", vch.DocNo);
                cmd.Parameters.AddWithValue("@vchDate", vch.VchDate);
                cmd.Parameters.AddWithValue("@docDate", vch.DocDate);
                cmd.Parameters.AddWithValue("@vendorId", vch.VendorId);
                cmd.Parameters.AddWithValue("@salesmanId", vch.SalesmanId);
                cmd.Parameters.AddWithValue("@godownId", vch.GodownId);
                cmd.Parameters.AddWithValue("@approvedById", vch.ApprovedById);
                cmd.Parameters.AddWithValue("@statusId", vch.StatusId);
                cmd.Parameters.AddWithValue("@description", vch.Description);
                cmd.Parameters.AddWithValue("@isPosted", vch.IsPosted);
                cmd.Parameters.AddWithValue("@createdOn", vch.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", vch.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", vch.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", vch.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", vch.IsActive);
                cmd.Parameters.AddWithValue("@clientId", vch.ClientId);
                cmd.Parameters.AddWithValue("@DBoperation", vch.DBoperation.ToString());

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
        public bool ManageVoucherDetail(VoucherDetailDE vDetail, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageVoucherDetail";
                cmd.Parameters.AddWithValue("@id", vDetail.Id);
                cmd.Parameters.AddWithValue("@productId", vDetail.ProductId);
                cmd.Parameters.AddWithValue("@acId", vDetail.AcId);
                cmd.Parameters.AddWithValue("@vchId", vDetail.VchId);
                cmd.Parameters.AddWithValue("@billId", vDetail.BillId);
                cmd.Parameters.AddWithValue("@credit", vDetail.Credit);
                cmd.Parameters.AddWithValue("@debit", vDetail.Debit);
                cmd.Parameters.AddWithValue("@qty", vDetail.Qty);
                cmd.Parameters.AddWithValue("@rate", vDetail.Rate);
                cmd.Parameters.AddWithValue("@description", vDetail.Description);
                cmd.Parameters.AddWithValue ("@isDefaultDrCr", vDetail.IsDefaultDrCr);
                cmd.Parameters.AddWithValue("@createdOn", vDetail.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", vDetail.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", vDetail.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", vDetail.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", vDetail.IsActive);
                cmd.Parameters.AddWithValue("@clientId", vDetail.ClientId);
                cmd.Parameters.AddWithValue("@DBoperation", vDetail.DBoperation.ToString());

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
        #region Search Voucher
        public List<VoucherDE> SearchVouchers(string whereClause, MySqlCommand cmd = null)
        {
            List<VoucherDE> top = new List<VoucherDE>();
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
                top = cmd.Connection.Query<VoucherDE>("call MicroERP.SearchVoucher( '" + whereClause + "')").ToList();
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
        public List<VoucherDetailDE> SearchVoucherDetail(string whereClause, MySqlCommand cmd = null)
        {
            List<VoucherDetailDE> top = new List<VoucherDetailDE>();
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
                top = cmd.Connection.Query<VoucherDetailDE>("call MicroERP.SearchVoucherDetail( '" + whereClause + "')").ToList();
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
