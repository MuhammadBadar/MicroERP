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

namespace QST.MicroERP.Services
{
    public class PermissionsService : IBaseService<PermissionDE>, IPermissionService
    {
        #region Class Members/Class Variables

        private IBaseDAL<PermissionDE> _permsDAL;
        private SettingsService _stngSvc;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public PermissionsService ( IBaseDAL<PermissionDE> permsDAL )
        {
            _permsDAL = permsDAL;
            _stngSvc = new SettingsService (null);
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }
        #endregion
        #region Permission
        public bool ManageData ( PermissionDE mod )
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId (TableNames.permission.ToString ());
                retVal = _permsDAL.ManageData (mod, cmd);
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
        public bool SavePermissions ( List<PermissionDE> permissions )
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                MicroERPDataContext.StartTransaction (cmd);

                closeConnectionFlag = true;
                var Id = _corDAL.GetMaxId (TableNames.permission.ToString ());
                foreach (var perm in permissions)
                {
                    //if (perm.PermissionId != 0)
                    //{
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
                    //}
                }
                MicroERPDataContext.EndTransaction (cmd);
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
                if (closeConnectionFlag)
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
                var Routes = GetRoutes ();
                if (Routes != null && Routes.Count > 0)
                {
                    foreach (var feat in Routes)
                    {
                        var perm = new PermissionDE ();
                        perm.RouteId = feat.Id;
                        perm.Route = feat.Name;
                        perm.IsActive = true;
                        perm.UserId = mod.UserId;
                        perm.RoleId = mod.RoleId;
                        perm.IsReadOnly = null;
                        if (mod.UserId != default && mod.UserId != "")
                        {
                            var userPerms = SearchData (new PermissionDE { UserId = mod.UserId, RouteId = feat.Id });
                            if (userPerms != null && userPerms.Count > 0)
                            {
                                perm.Id = userPerms[0].Id;
                                perm.PermissionId = userPerms[0].PermissionId;
                                perm.User = userPerms[0].User;
                            }
                        }
                        if (mod.RoleId > 0)
                        {
                            var userPerms = SearchData (new PermissionDE { RoleId = mod.RoleId, RouteId = feat.Id });
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
        public List<PermissionDE> GetPermsByUserOrRole ( string UserId, int RoleId )
        {
            List<PermissionDE> perms = new List<PermissionDE> ();
            try
            {
                var routes = GetRoutes ();
                if (routes != null && routes.Count > 0)
                {
                    var userPerms = new List<PermissionDE> ();
                    var rolePerms = new List<PermissionDE> ();
                    var perm = new PermissionDE ();
                    foreach (var route in routes)
                    {
                        userPerms = SearchData (new PermissionDE { UserId = UserId, RouteId = route.Id });
                        if (userPerms != null && userPerms.Count > 0 && userPerms[0].PermissionId > 0)
                        {
                                perm = userPerms[0];
                        }
                        else
                        {
                            rolePerms = SearchData (new PermissionDE { RoleId = RoleId, RouteId = route.Id });
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
        public List<SettingsDE> GetRoutes ( )
        {
            List<SettingsDE> Routes = new List<SettingsDE> ();
            try
            {
                var menu = _stngSvc.SearchMenu ().OrderBy (x => x.ParentId).ToList ();
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
