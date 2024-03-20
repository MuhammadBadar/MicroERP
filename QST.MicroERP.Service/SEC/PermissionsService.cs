using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Entities.Security;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.SearchCriteria;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL.IDAL;
using QST.MicroERP.Service;
using QST.MicroERP.Service.CLT;
using QST.MicroERP.Service.IServices;
using MySql.Data.MySqlClient;
using NLog;
using Org.BouncyCastle.Bcpg.Sig;
using Org.BouncyCastle.Crypto.Prng;
using QST.MicroERP.Core.Constants;
using QST.MicroERP.Service.CTL;
using QST.MicroERP.Core.Extensions;

namespace QST.MicroERP.Services
{
    public class PermissionsService :BaseService, IBaseService<PermissionDE>, IPermissionService
    {
        #region Class Members/Class Variables

        private IBaseDAL<PermissionDE> _permsDAL;
        private SettingsService _stngSvc;
        private CoreDAL _corDAL;
        private CatalogueService _ctlSvc;

        #endregion
        #region Constructors
        public PermissionsService ( IBaseDAL<PermissionDE> permsDAL )
        {
            _permsDAL = permsDAL;
            _ctlSvc = new CatalogueService ();
            _stngSvc = new SettingsService (null);
            _corDAL = new CoreDAL ();
        }
        #endregion
        #region Permission
        public bool ManageData ( PermissionDE mod )
        {
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                _entity = TableNames.SEC_Permission.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");

                _logger.Info ($"Going to Call:_permsDAL.ManageData (mod, cmd)");
                if (_permsDAL.ManageData (mod, cmd))
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    return true;
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd!=null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public bool SavePermissions ( List<PermissionDE> permissions )
        {
            bool retVal = false;
            try
            {
                if (permissions != null && permissions.Count > 0)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    MicroERPDataContext.StartTransaction (cmd);
                    _entity = TableNames.SEC_Permission.ToString ();

                    var Id = _coreDAL.GetNextIdByClient (_entity, permissions[0].ClientId, "ClientId");
                    foreach (var perm in permissions)
                    {
                        if (perm.Id > 0)
                        {
                            perm.DBoperation = DBoperations.Update;
                            retVal = _permsDAL.ManageData (perm, cmd);
                        }
                        else
                        {
                            if (perm.PermissionId != 0)
                            {
                                perm.DBoperation = DBoperations.Insert;
                                Id += 1;
                                perm.Id = Id;
                                retVal = _permsDAL.ManageData (perm, cmd);
                            }
                        }
                    }
                    MicroERPDataContext.EndTransaction (cmd);
                }
                return retVal;
            }
            catch (Exception ex)
            {
                MicroERPDataContext.CancelTransaction (cmd);
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd!=null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<PermissionDE> SearchData ( PermissionDE mod )
        {
            List<PermissionDE> retVal = new List<PermissionDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (mod.Id != default)
                    WhereClause += $" AND Id={mod.Id}";
                if (mod.RouteId != default)
                    WhereClause += $" AND RouteId={mod.RouteId}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    WhereClause += $" AND ClientId={mod.ClientId}";
                if (mod.RoleId != default)
                    WhereClause += $" AND RoleId={mod.RoleId}";
                if (mod.PermissionId != default)
                    WhereClause += $" AND PermissionId={mod.PermissionId}";
                if (mod.UserId != default)
                    WhereClause += $" and UserId like ''" + mod.UserId + "''";
                if (mod.IsActive != default && mod.IsActive == true)
                    WhereClause += $" AND IsActive=1";
                retVal = _permsDAL.SearchData (WhereClause, cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<PermissionDE> SearchPermissions ( PermissionDE mod )
        {
            List<PermissionDE> retVal = new List<PermissionDE> ();
            try
            {
                var Routes = GetRoutes (mod.ClientId);
                if (Routes != null && Routes.Count > 0)
                {
                    foreach (var route in Routes)
                    {
                        var perm = new PermissionDE ();
                        perm.RouteId = route.Id;
                        perm.Route = route.Name;
                        perm.IsActive = true;
                        perm.UserId = mod.UserId;
                        perm.RoleId = mod.RoleId;
                        perm.ClientId = mod.CltId;
                        perm.IsReadOnly = null;
                        if (mod.UserId != default && mod.UserId != "")
                        {
                            var userPerms = SearchData (new PermissionDE { UserId = mod.UserId, RouteId = route.Id, ClientId=mod.CltId });
                            if (userPerms != null && userPerms.Count > 0)
                            {
                                perm.Id = userPerms[0].Id;
                                perm.PermissionId = userPerms[0].PermissionId;
                                perm.User = userPerms[0].User;
                            }
                        }
                        if (mod.RoleId > 0)
                        {
                            var userPerms = SearchData (new PermissionDE { RoleId = mod.RoleId, RouteId = route.Id, ClientId = mod.CltId });
                            if (userPerms != null && userPerms.Count > 0)
                            {
                                perm.Id = userPerms[0].Id;
                                perm.PermissionId = userPerms[0].PermissionId;
                                perm.Role = userPerms[0].Role;
                            }
                        }
                        retVal.Add (perm);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
            return retVal;
        }
        public List<PermissionDE> GetPermsByUserOrRole ( string UserId, int RoleId, int ClientId )
        {
            List<PermissionDE> perms = new List<PermissionDE> ();
            try
            {
                var routes = GetRoutes ();
                if (routes != null && routes.Count > 0)
                {
                    var userPerms = new List<PermissionDE> ();
                    var rolePerms = new List<PermissionDE> ();
                    foreach (var route in routes)
                    {
                        var perm = new PermissionDE ();
                        userPerms = SearchData (new PermissionDE { UserId = UserId, RouteId = route.Id, ClientId=ClientId });
                        if (userPerms != null && userPerms.Count > 0 && userPerms[0].PermissionId > 0)
                        {
                                perm = userPerms[0];
                        }
                        else
                        {
                            rolePerms = SearchData (new PermissionDE { RoleId = RoleId, RouteId = route.Id, ClientId = ClientId });
                            if (rolePerms != null && rolePerms.Count > 0)
                                if (rolePerms[0].PermissionId > 0)
                                {
                                    perm = rolePerms[0];
                                }
                        }
                        if (perm.Id == 0)
                        {
                            perm.RouteId = route.Id;
                            perm.Route = route.Name;
                            perm.IsActive = true;
                            perm.ClientId = ClientId;
                            perm.UserId = UserId;
                            perm.RoleId = RoleId;
                            perm.IsReadOnly = null;
                        }
                        perms.Add (perm);
                    }
                }
                if (perms != null && perms.Count > 0)
                    perms = Translate (perms);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
            return perms;
        }
        public List<SettingsDE> GetRoutes (int? clientId=null )
        {
            List<SettingsDE> Routes = new List<SettingsDE> ();
            try
            {
                if (clientId!=null && clientId > 0)
                {
                   
                }
                var menu = _stngSvc.SearchMenu ().OrderBy (x => x.ParentId).ToList ();
                if (clientId != null && clientId > 0) 
                {
                    var client = _ctlSvc.SearchClients (new ClientDE { Id = (int)clientId }).FirstOrDefault ();
                    if (client != null) 
                    {
                        menu = menu.Where (x => client.ModuleIds.ToStringList ().Contains(x.PParentId.ToString()) ||
                         client.ModuleIds.ToStringList ().Contains (x.ParentId.ToString ()) ||
                         client.ModuleIds.ToStringList ().Contains (x.Id.ToString ())).ToList();
                    }                   
                }
                if (menu != null && menu.Count > 0)
                    foreach (var item in menu)
                    {
                        var find = menu.FindAll (x => x.ParentId == item.Id);
                        if (find.Count == 0)
                            Routes.Add (item);
                    }
                return Routes;
            }
            catch (Exception) { throw; }
        }
        public List<PermissionDE> Translate ( List<PermissionDE> perms )
        {
            foreach (var perm in perms)
            {
                if (perm.PermissionId > 0 && perm.PermissionId != (int)Permissions.Deny)
                    perm.IsReadOnly = perm.PermissionId == (int)Permissions.ReadOnly ? true : false;
                else
                    perm.IsReadOnly = null;
            }
            return perms;
        }
        #endregion
    }
}
