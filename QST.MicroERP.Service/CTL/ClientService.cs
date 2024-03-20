using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using MySql.Data.MySqlClient;
using NLog;

namespace QST.MicroERP.Service.CTL
{
    public partial class CatalogueService
    {
        #region Class Variables
        //private ClientsDAL _clientsDAL;
        //private CoreDAL _coreDAL;
        //private Logger _logger;
        #endregion
        #region Constructor
        //public ClientsService()
        //{
        //    _clientsDAL = new ClientsDAL();
        //    _coreDAL = new CoreDAL();
        //    _logger = LogManager.GetLogger("fileLogger");
        //}
        #endregion
        #region  Clients
        public ClientDE ManageClients(ClientDE _client)
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                if (_client.DBoperation == DBoperations.Insert)
                    _client.Id =  _coreDAL.GetnextId(TableNames.CTL_Client.ToString());
                if (_client.ModuleIdList != null)
                    if (_client.ModuleIdList.Count > 0)
                        _client.ModuleIds = string.Join (",", _client.ModuleIdList.ToArray ());
                    else
                        _client.ModuleIds = null;
                else _client.ModuleIds = null;
                retVal = _clientsDAL.ManageClients(_client, cmd);
                return _client;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<ClientDE> SearchClients(ClientDE _clients)
        {
            List<ClientDE> retVal = new List<ClientDE>();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_clients.Id != default)
                    WhereClause += $" AND Id={_clients.Id}";
                if (_clients.ClientName != default)
                    WhereClause += $" and ClientName like ''" + _clients.ClientName + "''";
                if (_clients.UserId != default)
                    WhereClause += $" and UserId like ''" + _clients.UserId + "''";
                if (_clients.Category != default)
                    WhereClause += $" and Category like ''" + _clients.Category + "''";
                if (_clients.Address != default)
                    WhereClause += $" and Address like ''" + _clients.Address + "''";
                if (_clients.City != default)
                    WhereClause += $" and City like ''" + _clients.City + "''";
                if (_clients.Country != default)
                    WhereClause += $" and Country like ''" + _clients.Country + "''";
                if (_clients.Contact != default)
                    WhereClause += $" and Contact like ''" + _clients.Contact + "''";
                if (_clients.Owner != default)
                    WhereClause += $" and Owner like ''" + _clients.Owner + "''";
                if (_clients.RelevantPerson != default)
                    WhereClause += $" and ReleventPerson like ''" + _clients.RelevantPerson + "''";
                if (_clients.IsActive != default && _clients.IsActive == true)
                    WhereClause += $" AND IsActive=1";


                retVal = _clientsDAL.SearchClients(WhereClause, cmd);
                foreach (var item in retVal)
                {
                    if (item.ModuleIds != null)
                    {
                        List<string> result = item.ModuleIds.Split (',').ToList ();
                        item.ModuleIdList = new List<int> ();
                        foreach (var unit in result)
                        {
                            item.ModuleIdList.Add (int.Parse (unit));
                        }
                    }

                }
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        #endregion
    }
}
