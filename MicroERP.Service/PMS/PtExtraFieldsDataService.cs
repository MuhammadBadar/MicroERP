using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MySql.Data.MySqlClient;
using NLog;

namespace MicroERP.Service
{
    public class PtExtraFieldsDataService
    {
        #region Class Variables
        private PtExtraFieldsDataDAL _exfdDAL;
        private CoreDAL _coreDAL;
        private Logger _logger;
        #endregion
        #region Constructor
        public PtExtraFieldsDataService ( )
        {
            _exfdDAL = new PtExtraFieldsDataDAL ();
            _coreDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }
        #endregion
        #region  PtExtraFieldsData
        public bool ManagePtExtraFieldsData ( PtExtraFieldsDataDE _exfd )
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;

                if (_exfd.DBoperation == DBoperations.Insert)
                    _exfd.Id = _coreDAL.GetNextIdByClient (TableNames.ptextrafieldsdata.ToString (),_exfd.ClientId, "ClientId");
                retVal = _exfdDAL.ManagePtExtraFieldsData (_exfd, cmd);
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
        public List<PtExtraFieldsDataDE> SearchPtExtraFieldsData ( PtExtraFieldsDataDE _exfd )
        {
            List<PtExtraFieldsDataDE> retVal = new List<PtExtraFieldsDataDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_exfd.Id != default)
                    WhereClause += $" AND Id={_exfd.Id}";
                if (_exfd.ClientId != default && _exfd.ClientId!=0)
                    WhereClause += $" AND ClientId={_exfd.ClientId}";
                if (_exfd.FieldId != default)
                    WhereClause += $" AND FieldId={_exfd.FieldId}";
                if (_exfd.PatientId >0)
                    WhereClause += $" AND PatientId={_exfd.PatientId}";
                if (_exfd.IsActive != default && _exfd.IsActive == true)
                    WhereClause += $" AND IsActive=1";

                retVal = _exfdDAL.SearchPtExtraFieldsData (WhereClause, cmd);
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
        #endregion
    }
}
