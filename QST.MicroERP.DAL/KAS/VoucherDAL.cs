using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL
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
                cmd.CommandText = SPNames.ACC_Manage_Voucher;
                cmd.Parameters.AddWithValue("@prm_id", vch.Id);
                cmd.Parameters.AddWithValue("@prm_vchTypeId", vch.VchTypeId);
                cmd.Parameters.AddWithValue("@prm_vchNo", vch.VchNo);
                cmd.Parameters.AddWithValue("@prm_invNo", vch.InvNo);
                cmd.Parameters.AddWithValue("@prm_docNo", vch.DocNo);
                cmd.Parameters.AddWithValue("@prm_vchDate", vch.VchDate);
                cmd.Parameters.AddWithValue("@prm_docDate", vch.DocDate);
                cmd.Parameters.AddWithValue("@prm_vendorId", vch.VendorId);
                cmd.Parameters.AddWithValue("@prm_salesmanId", vch.SalesmanId);
                cmd.Parameters.AddWithValue("@prm_godownId", vch.GodownId);
                cmd.Parameters.AddWithValue("@prm_approvedById", vch.ApprovedById);
                cmd.Parameters.AddWithValue("@prm_statusId", vch.StatusId);
                cmd.Parameters.AddWithValue("@prm_description", vch.Description);
                cmd.Parameters.AddWithValue("@prm_isPosted", vch.IsPosted);
                cmd.Parameters.AddWithValue("@prm_createdOn", vch.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdById", vch.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", vch.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedById", vch.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", vch.IsActive);
                cmd.Parameters.AddWithValue("@prm_clientId", vch.ClientId);
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
                cmd.CommandText = SPNames.ACC_Manage_VoucherDetail.ToString ();
                cmd.Parameters.AddWithValue("@prm_id", vDetail.Id);
                cmd.Parameters.AddWithValue("@prm_productId", vDetail.ProductId);
                cmd.Parameters.AddWithValue("@prm_acId", vDetail.AcId);
                cmd.Parameters.AddWithValue("@prm_vchId", vDetail.VchId);
                cmd.Parameters.AddWithValue("@prm_billId", vDetail.BillId);
                cmd.Parameters.AddWithValue("@prm_credit", vDetail.Credit);
                cmd.Parameters.AddWithValue("@prm_debit", vDetail.Debit);
                cmd.Parameters.AddWithValue("@prm_qty", vDetail.Qty);
                cmd.Parameters.AddWithValue("@prm_rate", vDetail.Rate);
                cmd.Parameters.AddWithValue("@prm_description", vDetail.Description);
                cmd.Parameters.AddWithValue("@prm_isDefaultDrCr", vDetail.IsDefaultDrCr);
                cmd.Parameters.AddWithValue("@prm_createdOn", vDetail.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdById", vDetail.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", vDetail.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedById", vDetail.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", vDetail.IsActive);
                cmd.Parameters.AddWithValue("@prm_clientId", vDetail.ClientId);
                cmd.Parameters.AddWithValue("@prm_DBoperation", vDetail.DBoperation.ToString());

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
                top = cmd.Connection.Query<VoucherDE>("call"+SPNames.ACC_Search_Voucher+"( '" + whereClause + "')").ToList();
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
                top = cmd.Connection.Query<VoucherDetailDE>("call "+SPNames.ACC_Search_VoucherDetail.ToString () + "( '" + whereClause + "')").ToList();
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
