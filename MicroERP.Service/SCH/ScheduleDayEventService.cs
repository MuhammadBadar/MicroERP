using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MySql.Data.MySqlClient;
using NLog;
using MicroERP.Core.Models;
using MicroERP.DAL.SCH;
using MicroERP.Core.Entities.SCH;
using MicroERP.DAL.CTL;

namespace MicroERP.Service.SCH
{
    public class ScheduleDayEventService
    {
        #region Class Variables
        private ScheduleDayEventDAL _schEvtDAL;
        private CoreDAL _coreDAL;
        private Logger _logger;
        #endregion
        #region Constructor
        public ScheduleDayEventService()
        {
            _schEvtDAL = new ScheduleDayEventDAL();
            _coreDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region  Schedule Day Events
        public ScheduleDayEventDE ManageScheduleDayEvent(ScheduleDayEventDE mod)
        {
            bool retVal = false;
            string message = "";
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetnextId(TableNames.ScheduleDayEvent.ToString());
                retVal = _schEvtDAL.ManageScheduleDayEvent(mod, cmd);
                if (retVal == true)
                {
                    if (mod.DBoperation == DBoperations.Insert)
                        message = "ScheduleEvent Successfully Added";
                    else if (mod.DBoperation == DBoperations.Update)
                        message = "ScheduleEvent Successfully Updated";
                    mod.HasErrors = false;
                    _logger.Info(message);
                }
                else
                {
                    if (mod.DBoperation == DBoperations.Insert)
                        message = "Error Occurred while Saving the ScheduleEvent";
                    else if (mod.DBoperation == DBoperations.Update)
                        message = "Error Occurred while Updating the ScheduleEvent";
                    mod.HasErrors = true;
                    _logger.Error(message);
                }
                mod.ResponseMessage = message;

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
        public List<ScheduleDayEventDE> GetScheduleDayEvents(int schDayId)
        {
            List<ScheduleDayEventDE> mod = null;
            ScheduleDayEventSearchCriteria sc = new ScheduleDayEventSearchCriteria();
            sc.SchDayId = schDayId;
            //sc.IsActive = true;
            mod = SearchScheduleDayEvent(sc);

            return mod;
        }
        public List<ScheduleDayEventDE> SearchScheduleDayEvent(ScheduleDayEventSearchCriteria _schEvt)
        {
            List<ScheduleDayEventDE> retVal = new List<ScheduleDayEventDE>();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_schEvt.Id != default)
                    WhereClause += $" AND Id={_schEvt.Id}";
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
