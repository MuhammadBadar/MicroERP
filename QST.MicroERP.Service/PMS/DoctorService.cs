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
    public class DoctorService:BaseService, IBaseService<DoctorDE>
    {
        #region Class Variables
        //private DoctorDAL _docDAL;
        private readonly IBaseDAL<DoctorDE> _baseDAL;
        private CoreDAL _coreDAL;
        //private Logger _logger;
        #endregion
        #region Constructor
        public DoctorService( IBaseDAL<DoctorDE> baseDAL )
        {
            _baseDAL= baseDAL;
            //_docDAL = new DoctorDAL();
            _coreDAL = new CoreDAL();
            //_logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region  Doctor
        public bool ManageData(DoctorDE _doc)
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                if (_doc.DBoperation == DBoperations.Insert)
                    _doc.Id = _coreDAL.GetNextIdByClient(TableNames.doctor.ToString(),_doc.ClientId,"ClientId");
                retVal = _baseDAL.ManageData(_doc, cmd);
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
        public List<DoctorDE> SearchData(DoctorDE _doc)
        {
            List<DoctorDE> retVal = new List<DoctorDE>();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_doc.Id != default)
                    WhereClause += $" AND Id={_doc.Id}";
                if (_doc.ClientId != default)
                    WhereClause += $" AND ClientId={_doc.ClientId}";
                if (_doc.UserId != default)
                    WhereClause += $" and UserId like ''" + _doc.UserId + "''";
                if (_doc.StartTime != default)
                    WhereClause += $" and StartTime like ''" + _doc.StartTime + "''";
                if (_doc.User != default)
                    WhereClause += $" and User like ''" + _doc.User + "''";
                if (_doc.DoctorName != default)
                    WhereClause += $" and DoctorName like ''" + _doc.DoctorName + "''";
                if (_doc.DateOfBirth != default)
                    WhereClause += $" and DateOfBirth like ''" + _doc.DateOfBirth + "''";
                if (_doc.Gender != default)
                    WhereClause += $" and Gender like ''" + _doc.Gender + "''";
                if (_doc.ContactNo != default)
                    WhereClause += $" and ContactNo like ''" + _doc.ContactNo + "''";
                if (_doc.HouseNo != default)
                    WhereClause += $" and HouseNo like ''" + _doc.HouseNo + "''";
                if (_doc.Specialization != default)
                    WhereClause += $" and Specialization like ''" + _doc.Specialization + "''";
                if (_doc.IsActive != default && _doc.IsActive == true)
                    WhereClause += $" AND IsActive=1";


                retVal = _baseDAL.SearchData(WhereClause, cmd);
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
        #region  Doctor
        //public bool ManageDoctor ( DoctorDE _doc )
        //{
        //    bool retVal = false;
        //    bool closeConnectionFlag = false;
        //    MySqlCommand? cmd = null;
        //    try
        //    {
        //        cmd = MicroERPDataContext.OpenMySqlConnection ();
        //        closeConnectionFlag = true;

        //        if (_doc.DBoperation == DBoperations.Insert)
        //            _doc.Id = _coreDAL.GetnextId (TableNames.doctor.ToString ());
        //        retVal = _docDAL.ManageDoctor (_doc, cmd);
        //        return retVal;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error (ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (closeConnectionFlag)
        //            MicroERPDataContext.CloseMySqlConnection (cmd);
        //    }
        //}
        //public List<DoctorDE> SearchDoctor ( DoctorDE _doc )
        //{
        //    List<DoctorDE> retVal = new List<DoctorDE> ();
        //    bool closeConnectionFlag = false;
        //    MySqlCommand? cmd = null;
        //    try
        //    {
        //        cmd = MicroERPDataContext.OpenMySqlConnection ();
        //        closeConnectionFlag = true;
        //        string WhereClause = " Where 1=1";
        //        if (_doc.Id != default)
        //            WhereClause += $" AND Id={_doc.Id}";
        //        if (_doc.DoctorName != default)
        //            WhereClause += $" and DoctorName like ''" + _doc.DoctorName + "''";
        //        if (_doc.DateOfBirth != default)
        //            WhereClause += $" and DateOfBirth like ''" + _doc.DateOfBirth + "''";
        //        if (_doc.Gender != default)
        //            WhereClause += $" and Gender like ''" + _doc.Gender + "''";
        //        if (_doc.ContactNo != default)
        //            WhereClause += $" and ContactNo like ''" + _doc.ContactNo + "''";
        //        if (_doc.HouseNo != default)
        //            WhereClause += $" and HouseNo like ''" + _doc.HouseNo + "''";
        //        if (_doc.Specialization != default)
        //            WhereClause += $" and Specialization like ''" + _doc.Specialization + "''";
        //        if (_doc.IsActive != default && _doc.IsActive == true)
        //            WhereClause += $" AND IsActive=1";


        //        retVal = _docDAL.SearchDoctor (WhereClause, cmd);
        //        return retVal;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error (ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (closeConnectionFlag)
        //            MicroERPDataContext.CloseMySqlConnection (cmd);
        //    }
        //}
        #endregion
    }
}
