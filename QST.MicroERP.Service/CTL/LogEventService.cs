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

namespace QST.MicroERP.Service.CLT
{
    public class LogEventService
    {
        #region Class Members/Class Variables

        private LogEventDAL _LogEventDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public LogEventService()
        {
            _LogEventDAL = new LogEventDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region LogEvent
        public bool ManagementLogEvent(LogEventDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.CTL_LogEvent.ToString());
                retVal = _LogEventDAL.ManageLogEvent(mod, cmd);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<LogEventDE> SearchLogEvents(LogEventDE mod)
        {
            List<LogEventDE> LogEvents = new List<LogEventDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
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
        public bool MarkOutTime(string UserId)
        {
            bool retVal = false;
            try
            {
                string whereClause = "WHERE UserId like ''" + UserId + "'' ORDER BY Id DESC LIMIT 1";
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
        public void MarkInTime(string UserId)
        {
            try
            {
                var _event = new LogEventDE();
                _event.InTime = DateTime.Now;
                _event.Date = DateTime.Now;
                _event.UserId = UserId;
                _event.IsActive = true;
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
