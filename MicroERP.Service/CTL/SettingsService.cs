using MicroERP.Core.Entities.CTL;
using MicroERP.Core.Entities.SEC;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MicroERP.Service.IServices;
using MySql.Data.MySqlClient;
using NLog;

namespace MicroERP.Service.CLT
{
    public class SettingsService
    {
        #region Class Members/Class Variables

        private SettingsDAL _settingsDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        private SettingsDE _searchStng;
        private readonly IPermissionService _permSvc;

        #endregion
        #region Constructors
        public SettingsService ( IPermissionService permSvc = null )
        {
            _permSvc = permSvc;
            _settingsDAL = new SettingsDAL ();
            _corDAL = new CoreDAL ();
            _searchStng = new SettingsDE ();
            _logger = LogManager.GetLogger ("fileLogger");
        }
        #endregion
        #region Settings
        public bool ManagementSettings ( SettingsDE mod )
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                if (mod.DBoperation == DBoperations.Insert)
                {
                    mod.Id = _corDAL.GetNextLineIdByClt (TableNames.enumline.ToString (), "EnumTypeId", (int)mod.EnumTypeId, mod.ClientId);
                    if (mod.Id == 1)
                        mod.Id = int.Parse (mod.EnumTypeId + "001");
                    //else
                    //    mod.Id += 1;
                }
                if (mod.DBoperation == DBoperations.Update)
                {
                    List<SettingsDE> List = _settingsDAL.SearchSettingss ($"where Id={mod.Id} and EnumTypeId={mod.EnumTypeId} and ClientId={mod.ClientId}");
                    if (List.Count > 0)
                    {
                        _searchStng = List.First ();
                        if (mod.KeyCode != _searchStng.KeyCode)
                        {
                            List<SettingsDE> _list = _settingsDAL.SearchSettingss ($"where " +
                                $"ClientId={mod.ClientId} and EnumTypeId={mod.EnumTypeId} AND AccountCode  like ''%" + _searchStng.AccountCode + "%''");
                            if (mod.LevelId == (int)AccountLevels.Main)
                            {
                                List<SettingsDE> accountUseLevel1 = _list.FindAll (x => _searchStng.KeyCode ==
                                x.AccountCode?.Substring (0, 2));
                                foreach (var account in accountUseLevel1)
                                {
                                    account.DBoperation = DBoperations.Update;
                                    account.AccountCode = account.AccountCode?.Remove (0, 2).Insert (0, mod.KeyCode);
                                    _settingsDAL.ManageSettings (account, cmd);
                                }
                            }
                            else if (mod.LevelId == (int)AccountLevels.Sub)
                            {
                                List<SettingsDE> accountUseLevel1 = _list.FindAll (x => _searchStng.KeyCode ==
                                x.AccountCode?.Substring (3, 3));
                                foreach (var account in accountUseLevel1)
                                {
                                    account.DBoperation = DBoperations.Update;
                                    account.AccountCode = account.AccountCode?.Remove (3, 3).Insert (3, mod.KeyCode);
                                    _settingsDAL.ManageSettings (account, cmd);
                                }
                            }
                            else if (mod.LevelId == (int)AccountLevels.Subsidiary)
                            {
                                List<SettingsDE> accountUseLevel1 = _list.FindAll (x => _searchStng.KeyCode ==
                                x.AccountCode?.Substring (7, 4));
                                foreach (var account in accountUseLevel1)
                                {
                                    account.DBoperation = DBoperations.Update;
                                    account.AccountCode = account.AccountCode?.Remove (7, 4).Insert (6, mod.KeyCode);
                                    _settingsDAL.ManageSettings (account, cmd);
                                }
                            }
                        }
                    }
                }
                retVal = _settingsDAL.ManageSettings (mod, cmd);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<SettingsDE> SearchSettingss ( SettingsDE mod )
        {
            List<SettingsDE> Settingss = new List<SettingsDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
                //if (mod.ClientId != default )
                whereClause += $" AND ClientId={mod.ClientId}";
                //if (mod.ModuleId != default && mod.ModuleId != 0)
                whereClause += $" AND ModuleId={mod.ModuleId}";
                if (mod.TypeModuleId != default && mod.TypeModuleId != 0)
                    whereClause += $" AND TypeModuleId={mod.TypeModuleId}";
                if (mod.Name != default && mod.Name != "")
                    whereClause += $" AND Name like ''" + mod.Name + "''";
                if (mod.Level != default && mod.Level != "")
                    whereClause += $" AND Level like ''" + mod.Level + "''";
                if (mod.ParentName != default && mod.ParentName != "")
                    whereClause += $" AND ParentName like ''" + mod.ParentName + "''";
                if (mod.AccountCode != default && mod.AccountCode != "")
                    whereClause += $" AND AccountCode  like ''" + mod.AccountCode + "''";
                if (mod.KeyCode != default && mod.KeyCode != "")
                    whereClause += $" AND KeyCode like ''" + mod.KeyCode + "''";
                if (mod.TypeKeyCode != default && mod.Name != "")
                    whereClause += $" AND TypeKeyCode like ''" + mod.TypeKeyCode + "''";
                if (mod.ParentId != default && mod.ParentId != 0)
                    whereClause += $" AND ParentId={mod.ParentId}";
                if (mod.LevelId != default && mod.LevelId != 0)
                    whereClause += $" AND LevelId={mod.LevelId}";
                if (mod.EnumTypeId != default && mod.EnumTypeId != 0)
                    whereClause += $" AND EnumTypeId={mod.EnumTypeId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                Settingss = _settingsDAL.SearchSettingss (whereClause);

                #endregion
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
            return Settingss;
        }
        public List<SettingsDE> SearchEnumLine ( SettingsDE mod )
        {
            List<SettingsDE> Settingss = new List<SettingsDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.ModuleId != default && mod.ModuleId != 0)
                    whereClause += $" AND ModuleId={mod.ModuleId}";
                if (mod.TypeModuleId != default && mod.TypeModuleId != 0)
                    whereClause += $" AND TypeModuleId={mod.TypeModuleId}";
                if (mod.Name != default && mod.Name != "")
                    whereClause += $" AND Name like ''" + mod.Name + "''";
                if (mod.Level != default && mod.Level != "")
                    whereClause += $" AND Level like ''" + mod.Level + "''";
                if (mod.ParentName != default && mod.ParentName != "")
                    whereClause += $" AND ParentName like ''" + mod.ParentName + "''";
                if (mod.AccountCode != default && mod.AccountCode != "")
                    whereClause += $" AND AccountCode  like ''" + mod.AccountCode + "''";
                if (mod.KeyCode != default && mod.KeyCode != "")
                    whereClause += $" AND KeyCode like ''" + mod.KeyCode + "''";
                if (mod.TypeKeyCode != default && mod.Name != "")
                    whereClause += $" AND TypeKeyCode like ''" + mod.TypeKeyCode + "''";
                if (mod.ParentId != default && mod.ParentId != 0)
                    whereClause += $" AND ParentId={mod.ParentId}";
                if (mod.LevelId != default && mod.LevelId != 0)
                    whereClause += $" AND LevelId={mod.LevelId}";
                if (mod.EnumTypeId != default && mod.EnumTypeId != 0)
                    whereClause += $" AND EnumTypeId={mod.EnumTypeId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                Settingss = _settingsDAL.SearchSettingss (whereClause);

                #endregion
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
            return Settingss;
        }
        public List<SettingsDE> SearchMenu ( )
        {
            List<SettingsDE> Settingss = new List<SettingsDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";

                whereClause += $" AND ClientId={0}";
                whereClause += $" AND ModuleId={0}";
                whereClause += $" AND (EnumTypeId={(int)EnumType.Modules} or EnumTypeId={(int)EnumType.SideMenu} or EnumTypeId={(int)EnumType.SideSubMenu})";
                whereClause += $" AND IsActive =1";
                Settingss = _settingsDAL.SearchSettingss (whereClause);
                var routes = new List<SettingsDE> ();

                #endregion
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
            return Settingss;
        }
        public List<SettingsDE> SearchAccounts ( SettingsDE mod )
        {
            List<SettingsDE> Settingss = new List<SettingsDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.Name != default && mod.Name != "")
                    whereClause += $" AND Name like ''%" + mod.Name + "%''";
                if (mod.Level != default && mod.Level != "")
                    whereClause += $" AND Level like ''%" + mod.Level + "%''";
                if (mod.ParentName != default && mod.ParentName != "")
                    whereClause += $" AND ParentName like ''%" + mod.ParentName + "%''";
                if (mod.AccountCode != default && mod.AccountCode != "")
                    whereClause += $" AND AccountCode  like ''%" + mod.AccountCode + "%''";
                if (mod.KeyCode != default && mod.KeyCode != "")
                    whereClause += $" AND KeyCode like ''%" + mod.KeyCode + "%''";
                if (mod.TypeKeyCode != default && mod.Name != "")
                    whereClause += $" AND TypeKeyCode like ''" + mod.TypeKeyCode + "''";
                if (mod.ParentId != default && mod.ParentId != 0)
                    whereClause += $" AND ParentId={mod.ParentId}";
                if (mod.LevelId != default && mod.LevelId != 0)
                    whereClause += $" AND LevelId={mod.LevelId}";
                if (mod.EnumTypeId != default && mod.EnumTypeId != 0)
                    whereClause += $" AND EnumTypeId={mod.EnumTypeId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                Settingss = _settingsDAL.SearchSettingss (whereClause);

                #endregion
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
            return Settingss;
        }
        public List<SettingsDE> SearchMenuByUser ( UserVM user )
        {
            List<SettingsDE> Settings = new List<SettingsDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";

                whereClause += $" AND ClientId={0}";
                whereClause += $" AND ModuleId={0}";
                whereClause += $" AND (EnumTypeId={(int)EnumType.Modules} or EnumTypeId={(int)EnumType.SideMenu} or EnumTypeId={(int)EnumType.SideSubMenu})";
                whereClause += $" AND IsActive =1";
                Settings = _settingsDAL.SearchSettingss (whereClause);
                if (_permSvc != null && user.Id != null && user.RoleId > 0)
                {
                    var perm = _permSvc.GetPermsByUserOrRole (user.Id, user.RoleId);
                    foreach (var item in Settings)
                    {
                        item.ShouldDisplay = true;
                        var find = Settings.FindAll (x => x.ParentId == item.Id);
                        if (find.Count == 0)
                        {
                            var serach = perm.Find (x => x.RouteId == item.Id);
                            if (serach != null && serach.PermissionId > 0 && serach.PermissionId! != (int)Permissions.Deny)
                            { }
                            else item.ShouldDisplay = false;
                        }
                    }
                    Settings = Settings.OrderByDescending (x => x.ParentId).ToList ();
                    foreach (var item in Settings)
                    {
                        var hasChildren = Settings.FindAll (x => x.ParentId == item.Id);
                        bool condition = hasChildren.Count > 0 && !hasChildren.Any (x => x.ShouldDisplay);
                        if (condition)
                            item.ShouldDisplay = false;
                    }
                    Settings = Settings.Where (x => x.ShouldDisplay == true).ToList ();
                }

                #endregion
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
            return Settings;
        }

        #endregion
    }
}
