using Dapper;
using QST.MicroERP.Core.Entities.CTL;
using MySql.Data.MySqlClient;
using System.Data;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL.CTL
{
    public class SettingsDAL
    {
        #region Operations
        public bool ManageSettings ( SettingsDE stng, MySqlCommand? cmd )
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
                cmd.CommandText = SPNames.CTL_Manage_Settings.ToString ();
                cmd.Parameters.AddWithValue ("@prm_id", stng.Id);
                cmd.Parameters.AddWithValue ("@prm_clientId", stng.ClientId);
                cmd.Parameters.AddWithValue ("@prm_moduleId", stng.ModuleId);
                cmd.Parameters.AddWithValue ("@prm_name", stng.Name);
                cmd.Parameters.AddWithValue ("@prm_value", stng.Value);
                cmd.Parameters.AddWithValue ("@prm_accountCode", stng.AccountCode);
                cmd.Parameters.AddWithValue ("@prm_levelId", stng.LevelId);
                cmd.Parameters.AddWithValue ("@prm_description", stng.Description);
                cmd.Parameters.AddWithValue ("@prm_keyCode", stng.KeyCode);
                cmd.Parameters.AddWithValue ("@prm_isSystemDefined", stng.IsSystemDefined);
                cmd.Parameters.AddWithValue ("@prm_istAccountLevel", stng.IstAccountLevel);
                cmd.Parameters.AddWithValue ("@prm_parentId", stng.ParentId);
                cmd.Parameters.AddWithValue ("@prm_enumTypeId", stng.EnumTypeId);
                cmd.Parameters.AddWithValue ("@prm_createdOn", stng.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", stng.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", stng.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", stng.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", stng.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", stng.DBoperation.ToString ());

                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception ex)
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
        public List<SettingsDE> SearchSettingss ( string whereClause, MySqlCommand cmd = null )
        {
            List<SettingsDE> top = new List<SettingsDE> ();
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
                top = cmd.Connection.Query<SettingsDE> ("call "+SPNames.CTL_Search_Settings.ToString () + "( '" + whereClause + "')").ToList ();
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
