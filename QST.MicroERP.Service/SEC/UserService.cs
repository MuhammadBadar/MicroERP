using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL.SEC;
using MySql.Data.MySqlClient;
using NLog;
using System.Data;

namespace QST.MicroERP.Service.SEC
{
    public class UserService:BaseService
    {
        #region Class Members/Class Variables

        private UserDAL _userDAL;

        #endregion
        #region Constructors
        public UserService()
        {
            _userDAL = new UserDAL();
        }
        #endregion
        #region User
        public List<UserDE> SearchUsers(UserDE mod)
        {
            List<UserDE> Users = new List<UserDE>();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                #region Search
                string whereClause = " Where 1=1";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId ={mod.ClientId}";
                if (mod.CltId != default)
                    whereClause += $" AND CltId ={mod.CltId}";
                if (mod.ModuleId != default)
                    whereClause += $" AND ModuleId ={mod.ModuleId}";
                if (mod.Role != default)
                    whereClause += $" AND Role like ''" + mod.Role + "''";
                if (mod.RoleId != default)
                    whereClause += $" AND RoleId like ''" + mod.RoleId + "''";
                if (mod.UserPassword != default)
                    whereClause += $" AND UserPassword like ''" + mod.UserPassword + "''";
                if (mod.IncludeSubordinatesData && mod.Id != default)
                {
                    var user = new UserDE ();
                    user.Id = mod.Id;
                    var subordinateUsers = GetSubordinates (user);
                    if (subordinateUsers.Count > 0)
                    {
                        string subordinateIds = string.Join ("'',''", subordinateUsers.Select (x => x.Id));
                        whereClause += $" and (Id like ''" + mod.Id + "'' or Id IN (''" + subordinateIds + "''))";
                    }
                    else
                    {
                        if (mod.Id != default && mod.Id != "")
                            whereClause += $" and Id like ''" + mod.Id + "''";
                    }
                }
                else
                {
                    if (mod.Id != default)
                        whereClause += $" AND Id like ''" + mod.Id + "'' ";
                }
                Users = _userDAL.SearchUser (whereClause);

                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error(exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return Users;
        }
        public List<UserDE> GetSupervisor(UserDE mod)
        {
            List<UserDE> supervisors = new List<UserDE>();
            var userList = SearchUsers(new UserDE { IsActive = true, ClientId =mod.ClientId });
            GetSupervisorsRecursive(userList, mod.Id, supervisors);
            return supervisors;
        }
        static void GetSupervisorsRecursive(List<UserDE> userList, string userId, List<UserDE> supervisors)
        {
            UserDE user = userList.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                supervisors.Add(user);

                if (user.SupervisorId != null)
                    GetSupervisorsRecursive(userList, user.SupervisorId, supervisors);
            }
        }
        public List<UserDE> GetSubordinates(UserDE mod)
        {
            List<UserDE> subordinates = new List<UserDE>();
            var userList = SearchUsers(new UserDE { IsActive = true, ClientId = mod.ClientId });
            GetSubordinatesRecursive(userList, mod.Id, subordinates);
            return subordinates;
        }
        static void GetSubordinatesRecursive(List<UserDE> userList, string userId, List<UserDE> subordinates)
        {
            List<UserDE> directSubordinates = userList.Where(u => u.SupervisorId == userId).ToList();
            subordinates.AddRange(directSubordinates);

            foreach (var subordinate in directSubordinates)
            {
                GetSubordinatesRecursive(userList, subordinate.Id, subordinates);
            }
        }
        public List<UserDE> GetUserWithSubordinates ( UserDE mod )
        {
            mod.IncludeSubordinatesData = true;
            return SearchUsers (mod);
        }
        #endregion
    }
}
