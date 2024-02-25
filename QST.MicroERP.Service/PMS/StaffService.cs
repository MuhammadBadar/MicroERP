using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL.IDAL;
using MySql.Data.MySqlClient;
using NLog;

namespace QST.MicroERP.Service
{
    public class StaffService
    {
        #region Class Variables
        private StaffDAL _staffDAL;
        private CoreDAL _coreDAL;
        private Logger _logger;
        #endregion
        #region Constructor
        public StaffService ( )
        {
            _staffDAL = new StaffDAL();
            _coreDAL = new CoreDAL ();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region  Staff
        public bool ManageStaff ( StaffDE _staff )
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;

                if (_staff.DBoperation == DBoperations.Insert)
                    _staff.Id = _coreDAL.GetNextIdByClient (TableNames.PMS_Staff.ToString (), _staff.ClientId, "ClientId");
                retVal = _staffDAL.ManageStaff (_staff, cmd);
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
        public List<StaffDE> SearchStaff ( StaffDE _staff )
        {
            List<StaffDE> retVal = new List<StaffDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_staff.Id != default)
                    WhereClause += $" AND Id={_staff.Id}";
                if (_staff.ClientId != default)
                    WhereClause += $" AND ClientId={_staff.ClientId}";
                if (_staff.Name != default)
                    WhereClause += $" and Name like ''" + _staff.Name + "''";
                if (_staff.DateOfBirth != default)
                    WhereClause += $" and DateOfBirth like ''" + _staff.DateOfBirth + "''";
                if (_staff.Gender != default)
                    WhereClause += $" and Gender like ''" + _staff.Gender + "''";
                if (_staff.ContactNo != default)
                    WhereClause += $" and ContactNo like ''" + _staff.ContactNo + "''";
                if (_staff.HouseNo != default)
                    WhereClause += $" and HouseNo like ''" + _staff.HouseNo + "''";
                if (_staff.IsActive != default && _staff.IsActive == true)
                    WhereClause += $" AND IsActive=1";


                retVal = _staffDAL.SearchStaff (WhereClause, cmd);
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
