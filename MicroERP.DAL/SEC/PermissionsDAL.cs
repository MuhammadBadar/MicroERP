using Dapper;
using MicroERP.Core.Entities.Security;
using MicroERP.DAL.IDAL;
using MySql.Data.MySqlClient;
using System.Data;

namespace MicroERP.DAL
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
                cmd.CommandText = "ManagePermission";
                cmd.Parameters.AddWithValue ("@id", Perm.Id);
                cmd.Parameters.AddWithValue ("@routeId", Perm.RouteId);
                cmd.Parameters.AddWithValue ("@userId", Perm.UserId);
                cmd.Parameters.AddWithValue ("@roleId", Perm.RoleId);
                cmd.Parameters.AddWithValue ("@permissionId", Perm.PermissionId);
                cmd.Parameters.AddWithValue ("@createdOn", Perm.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", Perm.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", Perm.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", Perm.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", Perm.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", Perm.DBoperation.ToString ());

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
                top = cmd.Connection.Query<PermissionDE> ("call MicroERP.SearchPermissions( '" + whereClause + "')").ToList ();
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
