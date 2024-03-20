using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using MySql.Data.MySqlClient;
using NLog;
using QST.MicroERP.Core.Models;
using QST.MicroERP.DAL.SCH;
using QST.MicroERP.Core.Entities.SCH;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.Core.Constants;
using static Dapper.SqlMapper;
using System.Data;

namespace QST.MicroERP.Service.SCH
{
    public class ScheduleDayEventService:BaseService
    {
        #region Class Variables
        private ScheduleDayEventDAL _schEvtDAL;
        #endregion
        #region Constructor
        public ScheduleDayEventService()
        {
            _schEvtDAL = new ScheduleDayEventDAL();
        }
        #endregion
        #region  Schedule Day Events
        public ScheduleDayEventDE ManageScheduleDayEvent(ScheduleDayEventDE mod)
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                _entity = TableNames.SCH_ScheduleDayEvent.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");

                _logger.Info ($"Going to Call: _schEvtDAL.ManageScheduleDayEvent (mod, cmd)");
                if (_schEvtDAL.ManageScheduleDayEvent (mod, cmd))
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                }

                return mod;
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
        public List<ScheduleDayEventDE> GetScheduleDayEvents(int schDayId, int clientId)
        {
            ScheduleDayEventSearchCriteria sc = new ScheduleDayEventSearchCriteria();
            sc.SchDayId = schDayId;
            sc.ClientId = clientId;
            //sc.IsActive = true;
            return SearchScheduleDayEvent (sc);
        }
        public List<ScheduleDayEventDE> SearchScheduleDayEvent(ScheduleDayEventSearchCriteria _schEvt)
        {
            bool closeConnectionFlag = false;
            List<ScheduleDayEventDE> retVal = new List<ScheduleDayEventDE>();
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                string WhereClause = " Where 1=1";
                if (_schEvt.Id != default)
                    WhereClause += $" AND Id={_schEvt.Id}";
                if (_schEvt.ClientId != default && _schEvt.ClientId != 0)
                    WhereClause += $" AND ClientId={_schEvt.ClientId}";
                if (_schEvt.SchId != default)
                    WhereClause += $" AND SchId={_schEvt.SchId}";
                if (_schEvt.SchDayId != default)
                    WhereClause += $" AND SchDayId={_schEvt.SchDayId}";
                if (_schEvt.StartTime != default)
                    WhereClause += $" and StartTime like ''" + _schEvt.StartTime + "''";
                if (_schEvt.EndTime != default)
                    WhereClause += $" and EndTime like ''" + _schEvt.EndTime + "''";
                if (_schEvt.LocationId != default)
                    WhereClause += $" AND LocationId={_schEvt.LocationId}";
                if (_schEvt.IsActive != default && _schEvt.IsActive == true)
                    WhereClause += $" AND IsActive=1";

                retVal = _schEvtDAL.SearchScheduleDayEvent(WhereClause, cmd);
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
