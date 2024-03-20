using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Entities.ATT;
using QST.MicroERP.Core.Constants;
using System.Data;

namespace QST.MicroERP.Service.CLT
{
    public class LogEventService:BaseService
    {
        #region Class Members/Class Variables

        private LogEventDAL _LogEventDAL;

        #endregion
        #region Constructors
        public LogEventService()
        {
            _LogEventDAL = new LogEventDAL();
        }
        #endregion
        #region LogEvent
        public bool ManagementLogEvent(LogEventDE mod)
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                _entity = TableNames.CTL_LogEvent.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");

                _logger.Info ($"Going to Call: _LogEventDAL.ManageLogEvent(mod, cmd)");
                if (_LogEventDAL.ManageLogEvent (mod, cmd))
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
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<LogEventDE> SearchLogEvents(LogEventDE mod)
        {
            bool closeConnectionFlag = false;
            List<LogEventDE> LogEvents = new List<LogEventDE>();
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.UserId != default && mod.UserId != "")
                    whereClause += $" AND UserId like ''" + mod.UserId + "''";
                if (mod.Date.HasValue)
                    whereClause += $" AND DATE(Date) = ''{mod.Date.Value.ToString("yyyy-MM-dd")}''";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                LogEvents = _LogEventDAL.SearchLogEvent(whereClause);

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
            return LogEvents;
        }
        public bool MarkOutTime(string UserId, int ClientId)
        {
            bool retVal = false;
            try
            {
                string whereClause = "WHERE UserId like ''" + UserId + "''and ClientId="+ClientId+" ORDER BY Id DESC LIMIT 1";
                var events = _LogEventDAL.SearchLogEvent(whereClause);
                if (events != null && events.Count > 0)
                {
                    var logEvent = events[0];
                    logEvent.OutTime = DateTime.Now;
                    logEvent.DBoperation = DBoperations.Update;
                    retVal = ManagementLogEvent(logEvent);
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

            }
        }
        public void MarkInTime(string UserId, int ClientId )
        {
            try
            {
                var _event = new LogEventDE();
                _event.InTime = DateTime.Now;
                _event.Date = DateTime.Now;
                _event.UserId = UserId;
                _event.IsActive = true;
                _event.ClientId=ClientId;
                _event.DBoperation = DBoperations.Insert;
                ManagementLogEvent(_event);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {

            }
        }
        #endregion
    }
}
