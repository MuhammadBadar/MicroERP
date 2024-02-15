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
                cmd.CommandText = "Manage_VoucherType";
                cmd.Parameters.AddWithValue("id", vouc.Id);
                cmd.Parameters.AddWithValue ("clientId", vouc.ClientId);
                cmd.Parameters.AddWithValue("name", vouc.Name);
                cmd.Parameters.AddWithValue("defaultDrCrFirst", vouc.DefaultDrCrFirst);
                cmd.Parameters.AddWithValue("keyCode", vouc.KeyCode);
                cmd.Parameters.AddWithValue("defaultDrCrSecondId", vouc.DefaultDrCrSecondId);
                cmd.Parameters.AddWithValue("createdOn", vouc.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", vouc.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", vouc.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", vouc.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", vouc.IsActive);
                cmd.Parameters.AddWithValue("DBoperation", vouc.DBoperation.ToString());
                cmd.Parameters.AddWithValue("Filter", vouc.DBoperation.ToString());

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
                list = cmd.Connection.Query<VoucherTypeDE>("call MicroERP.Get_VoucherType( '" + whereClause + "')").ToList();
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
