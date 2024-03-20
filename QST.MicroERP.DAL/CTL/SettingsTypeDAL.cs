using Dapper;
using QST.MicroERP.Core.Entities.CTL;
using MySql.Data.MySqlClient;
using System.Data;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL.CTL
{
    public class SettingsTypeDAL
    {
        #region Operations
        public bool ManageSettingsType ( SettingsTypeDE stngType, MySqlCommand? cmd )
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
                cmd.CommandText = SPNames.CTL_Manage_SettingsType.ToString ();
                cmd.Parameters.AddWithValue ("@prm_id", stngType.Id);
                cmd.Parameters.AddWithValue ("@prm_clientId", stngType.ClientId);
                cmd.Parameters.AddWithValue ("@prm_moduleId", stngType.ModuleId);
                cmd.Parameters.AddWithValue ("@prm_name", stngType.Name);
                cmd.Parameters.AddWithValue ("@prm_description", stngType.Description);
                cmd.Parameters.AddWithValue ("@prm_keyCode", stngType.KeyCode);
                cmd.Parameters.AddWithValue ("@prm_isSystemDefined", stngType.IsSystemDefined);
                cmd.Parameters.AddWithValue ("@prm_istAccountLevel", stngType.IstAccountLevel);
                cmd.Parameters.AddWithValue ("@prm_parentId", stngType.ParentId);
                cmd.Parameters.AddWithValue ("@prm_isRequired", stngType.IsRequired);
                cmd.Parameters.AddWithValue ("@prm_createdOn", stngType.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", stngType.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", stngType.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", stngType.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", stngType.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", stngType.DBoperation.ToString ());

                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
                cmd.Parameters.Clear ();
            }
        }
        public List<SettingsTypeDE> SearchSettingsTypes ( string whereClause, MySqlCommand cmd = null )
        {
            List<SettingsTypeDE> top = new List<SettingsTypeDE> ();
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
                top = cmd.Connection.Query<SettingsTypeDE> ("call "+SPNames.CTL_Search_SettingsType.ToString () + "( '" + whereClause + "')").ToList ();
                return top;
            }
            catch (Exception)
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
