using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using MySql.Data.MySqlClient;
using NLog;

namespace QST.MicroERP.Service.CLT
{
    public class SettingsTypeService
    {
        #region Class Members/Class Variables

        private SettingsTypeDAL _settingsTypeDAL;
        private SettingsDAL _settingsDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public SettingsTypeService ( )
        {
            _settingsTypeDAL = new SettingsTypeDAL ();
            _settingsDAL = new SettingsDAL ();
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }
        #endregion
        #region SettingsType
        public SettingsTypeDE ManagementSettingsType ( SettingsTypeDE mod )
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                // MicroERPDataContext.StartTransaction (cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetNextIdByClient (TableNames.enums.ToString (), mod.ClientId, "ClientId");
                if (mod.Id == 1)
                    mod.Id = 1001;
                retVal = _settingsTypeDAL.ManageSettingsType (mod, cmd);
                var _Id = _corDAL.GetMaxLineIdByClt (TableNames.enumline.ToString (), "EnumTypeId", (int)mod.Id, mod.ClientId);
                foreach (var line in mod.SettingList)
                {
                    if (line.DBoperation == DBoperations.Insert)
                    {
                        if (_Id == 0)
                            _Id = int.Parse (mod.Id + "001");
                        else
                            _Id += 1;
                        line.Id = _Id;
                    }
                    line.EnumTypeId = mod.Id;
                    line.ClientId = mod.ClientId;
                    line.ModuleId = (int)mod.ModuleId;
                    _settingsDAL.ManageSettings (line, cmd);
                }
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                //MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                //MicroERPDataContext.CancelTransaction (cmd);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
                string whereClause = " Where 1=1";
                mod.SettingList = _settingsDAL.SearchSettingss (whereClause += $" AND EnumTypeId={mod.Id} AND IsActive ={true} and ClientId={mod.ClientId} and ModuleId={mod.ModuleId}");
            }
            return mod;
        }
        public List<SettingsTypeDE> SearchSettingsTypes ( SettingsTypeDE mod )
        {
            List<SettingsTypeDE> SettingsTypes = new List<SettingsTypeDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.ModuleId != default)
                    whereClause += $" AND ModuleId={mod.ModuleId}";
                if (mod.Name != default)
                    whereClause += $" AND Name like ''" + mod.Name + "''";
                if (mod.ParentId != default && mod.ParentId != 0)
                    whereClause += $" AND ParentId={mod.ParentId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                if (mod.IstAccountLevel != default)
                    whereClause += $" AND IstAccountLevel ={mod.IstAccountLevel}";
                SettingsTypes = _settingsTypeDAL.SearchSettingsTypes (whereClause);

                #endregion
                foreach (var line in SettingsTypes)
                {
                    whereClause = " Where 1=1";
                    line.SettingList = _settingsDAL.SearchSettingss (whereClause += $" AND EnumTypeId={line.Id} AND IsActive ={true} and ClientId={line.ClientId} and ModuleId={line.ModuleId}");
                }
            }
            catch (Exception exp)
            {
                _logger.Error (exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return SettingsTypes;
        }
        public List<SettingsTypeDE> SearchEnums ( SettingsTypeDE mod )
        {
            List<SettingsTypeDE> SettingsTypes = new List<SettingsTypeDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId={mod.ClientId}";
                //if (mod.ModuleId != default)
                whereClause += $" AND ModuleId={mod.ModuleId}";
                if (mod.Name != default)
                    whereClause += $" AND Name like ''" + mod.Name + "''";
                if (mod.ParentId != default && mod.ParentId != 0)
                    whereClause += $" AND ParentId={mod.ParentId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                if (mod.IstAccountLevel != default)
                    whereClause += $" AND IstAccountLevel ={mod.IstAccountLevel}";
                SettingsTypes = _settingsTypeDAL.SearchSettingsTypes (whereClause);

                #endregion
                foreach (var line in SettingsTypes)
                {
                    whereClause = " Where 1=1";
                    line.SettingList = _settingsDAL.SearchSettingss (whereClause += $" AND EnumTypeId={line.Id} AND IsActive ={true} and ClientId={line.ClientId} and ModuleId={line.ModuleId}");
                }
            }
            catch (Exception exp)
            {
                _logger.Error (exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return SettingsTypes;
        }
        #endregion
    }
}
