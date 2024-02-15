using MicroERP.Core.Entities.SEC;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MicroERP.DAL.SEC;
using MySql.Data.MySqlClient;
using NLog;

namespace MicroERP.Service.SEC
{
    public class UserService
    {
        #region Class Members/Class Variables

        private UserDAL _userDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public UserService()
        {
            _userDAL = new UserDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region User
        public List<UserDE> SearchUsers(UserDE mod)
        {
            List<UserDE> Users = new List<UserDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                #region Search
                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id like ''" + mod.Id + "''";
                if (mod.CLTId != default)
                    whereClause += $" AND CLTId like ''" + mod.CLTId + "''";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId ={mod.ClientId}";
                if (mod.ModuleId != default)
                    whereClause += $" AND ModuleId ={mod.ModuleId}";
                if (mod.Role != default)
                    whereClause += $" AND Role like ''" + mod.Role + "''";
                if (mod.RoleId != default)
                    whereClause += $" AND RoleId like ''" + mod.RoleId + "''";
                if (mod.UserPassword != default)
                    whereClause += $" AND UserPassword like ''" + mod.UserPassword + "''";
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
            var userList = SearchUsers(new UserDE { IsActive = true });
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
            var userList = SearchUsers(new UserDE { IsActive = true });
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

        #endregion
    }
}
