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

namespace QST.MicroERP.Service
{
    public class PatientService
    {
        #region Class Variables
        private PtExtraFieldsDataDAL _dataDAL;
        private PatientDAL _patDAL;
        private CoreDAL _coreDAL;
        private Logger _logger;
        #endregion
        #region Constructor
        public PatientService ( )
        {
            _dataDAL = new PtExtraFieldsDataDAL ();
            _patDAL = new PatientDAL ();
            _coreDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }
        #endregion
        #region  Patient
        public bool ManagePatient ( PatientDE mod )
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                MicroERPDataContext.StartTransaction (cmd);
                closeConnectionFlag = true;

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (TableNames.PMS_Patient.ToString (),mod.ClientId,"ClientId");
                retVal = _patDAL.ManagePatient (mod, cmd);
                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                    foreach (var line in mod.ptFData)
                    {
                        string whereClause = "where 1=1";
                        var list = _dataDAL.SearchPtExtraFieldsData (whereClause += $" AND PatientId={mod.Id}   and FieldId ={line.FieldId} and ClientId={mod.ClientId}");
                        if (list != null)
                            if (list.Count > 0)
                            {
                                line.DBoperation = DBoperations.Update;
                                line.IsActive = true;
                            }
                            else line.DBoperation = DBoperations.Insert;
                        line.ClientId = mod.ClientId;
                        line.PatientId = mod.Id;
                        _dataDAL.ManagePtExtraFieldsData (line, cmd);
                    }
                MicroERPDataContext.EndTransaction (cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                MicroERPDataContext.CancelTransaction (cmd);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<PatientDE> SearchPatient ( PatientDE _pat )
        {
            List<PatientDE> retVal = new List<PatientDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_pat.Id != default)
                    WhereClause += $" AND Id={_pat.Id}";
                if (_pat.ClientId != default)
                    WhereClause += $" AND ClientId={_pat.ClientId}";
                if (_pat.PatientName != default)
                    WhereClause += $" and PatientName like ''" + _pat.PatientName + "''";
                if (_pat.DateOfBirth != default)
                    WhereClause += $" and DateOfBirth like ''" + _pat.DateOfBirth + "''";
                if (_pat.Gender != default)
                    WhereClause += $" and Gender like ''" + _pat.Gender + "''";
                if (_pat.ContactNo != default)
                    WhereClause += $" and ContactNo like ''" + _pat.ContactNo + "''";
                if (_pat.HouseNo != default)
                    WhereClause += $" and HouseNo like ''" + _pat.HouseNo + "''";
                if (_pat.Address != default)
                    WhereClause += $" and Address like ''" + _pat.Address + "''";
                if (_pat.Remarks != default)
                    WhereClause += $" and Remarks like ''" + _pat.Remarks + "''";
                if (_pat.IsActive != default && _pat.IsActive == true)
                    WhereClause += $" AND IsActive=1";
                retVal = _patDAL.SearchPatient (WhereClause, cmd);
                foreach (var line in retVal)
                {
                    string whereClause = "where 1=1";
                    line.ptFData = _dataDAL.SearchPtExtraFieldsData (whereClause += $" AND PatientId={line.Id} " +
                        $"and ClientId={line.ClientId} and IsActive=true");
                }
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
            return retVal;
        }
        #endregion
    }
}
