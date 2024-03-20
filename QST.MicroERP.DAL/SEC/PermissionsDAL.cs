using Dapper;
using QST.MicroERP.Core.Entities.Security;
using QST.MicroERP.DAL.IDAL;
using MySql.Data.MySqlClient;
using System.Data;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL
{
    public class PermissionsDAL:IBaseDAL<PermissionDE>
    {
        #region Operations
        public bool ManageData ( PermissionDE Perm, MySqlCommand? cmd )
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                cmd.CommandText = SPNames.SEC_Manage_Permission.ToString ();
                cmd.Parameters.AddWithValue ("@prm_id", Perm.Id);
                cmd.Parameters.AddWithValue ("@prm_clientId", Perm.ClientId);
                cmd.Parameters.AddWithValue ("@prm_routeId", Perm.RouteId);
                cmd.Parameters.AddWithValue ("@prm_userId", Perm.UserId);
                cmd.Parameters.AddWithValue ("@prm_roleId", Perm.RoleId);
                cmd.Parameters.AddWithValue ("@prm_permissionId", Perm.PermissionId);
                cmd.Parameters.AddWithValue ("@prm_createdOn", Perm.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", Perm.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", Perm.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", Perm.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", Perm.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", Perm.DBoperation.ToString ());

                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                cmd.Parameters.Clear ();
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<PermissionDE> SearchData ( string whereClause, MySqlCommand? cmd )
        {
            List<PermissionDE> top = new List<PermissionDE> ();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                top = cmd.Connection.Query<PermissionDE> ("call "+SPNames.SEC_Search_Permission.ToString () + "( '" + whereClause + "')").ToList ();
                return top;
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        #endregion
    }
}
