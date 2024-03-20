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
    public class VoucherTypeDAL
    {
        #region VoucherType Operations
        public bool ManageVoucherType(VoucherTypeDE vouc, MySqlCommand cmd = null)
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
                cmd.CommandText =SPNames.ACC_Manage_VoucherType.ToString ();
                cmd.Parameters.AddWithValue("prm_id", vouc.Id);
                cmd.Parameters.AddWithValue("prm_clientId", vouc.ClientId);
                cmd.Parameters.AddWithValue("prm_name", vouc.Name);
                cmd.Parameters.AddWithValue("prm_defaultDrCrFirst", vouc.DefaultDrCrFirst);
                cmd.Parameters.AddWithValue("prm_keyCode", vouc.KeyCode);
                cmd.Parameters.AddWithValue("prm_defaultDrCrSecondId", vouc.DefaultDrCrSecondId);
                cmd.Parameters.AddWithValue("prm_createdOn", vouc.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", vouc.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", vouc.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", vouc.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", vouc.IsActive);
                cmd.Parameters.AddWithValue("prm_DBoperation", vouc.DBoperation.ToString());
                cmd.Parameters.AddWithValue("prm_Filter", vouc.DBoperation.ToString());

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
            }
        }
        public List<VoucherTypeDE> SearchVoucherType(string whereClause, MySqlCommand cmd = null)
        {
            List<VoucherTypeDE> list = new List<VoucherTypeDE>();
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
                list = cmd.Connection.Query<VoucherTypeDE>("call "+SPNames.ACC_Search_VoucherType.ToString () + "( '" + whereClause + "')").ToList();
                return list;
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
