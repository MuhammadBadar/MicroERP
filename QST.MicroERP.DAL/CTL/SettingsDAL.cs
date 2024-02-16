using Dapper;
using QST.MicroERP.Core.Entities.CTL;
using MySql.Data.MySqlClient;
using System.Data;

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
                cmd.CommandText = "ManageSettings";
                cmd.Parameters.AddWithValue ("@id", stng.Id);
                cmd.Parameters.AddWithValue ("@clientId", stng.ClientId);
                cmd.Parameters.AddWithValue ("@moduleId", stng.ModuleId);
                cmd.Parameters.AddWithValue ("@name", stng.Name);
                cmd.Parameters.AddWithValue ("@value", stng.Value);
                cmd.Parameters.AddWithValue ("@accountCode", stng.AccountCode);
                cmd.Parameters.AddWithValue ("@levelId", stng.LevelId);
                cmd.Parameters.AddWithValue ("@description", stng.Description);
                cmd.Parameters.AddWithValue ("@keyCode", stng.KeyCode);
                cmd.Parameters.AddWithValue ("@isSystemDefined", stng.IsSystemDefined);
                cmd.Parameters.AddWithValue ("@istAccountLevel", stng.IstAccountLevel);
                cmd.Parameters.AddWithValue ("@parentId", stng.ParentId);
                cmd.Parameters.AddWithValue ("@enumTypeId", stng.EnumTypeId);
                cmd.Parameters.AddWithValue ("@createdOn", stng.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", stng.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", stng.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", stng.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", stng.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", stng.DBoperation.ToString ());

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
                top = cmd.Connection.Query<SettingsDE> ("call QST.MicroERP.SearchSettings( '" + whereClause + "')").ToList ();
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
                cmd.Parameters.Clear ();
            }
        }
        #endregion
    }
}
