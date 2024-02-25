using QST.MicroERP.Core.Entities;
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

namespace QST.MicroERP.Service
{
    public class DCService
    {
        #region Data Members

        private DCDAL _dcDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public DCService()
        {
            _dcDAL = new DCDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region DC
        public DCDE ManagementDC(DCDE mod, int? Id = null)
        {
            MySqlCommand cmd = null;
            try
            {
                bool retVal = false;
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.SAL_DC.ToString());
                retVal = _dcDAL.ManageDC(mod,cmd);
                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                    foreach (var dcDet in mod.DCDetails)
                    {
                        dcDet.DCId = mod.Id;
                        if (dcDet.DBoperation == DBoperations.Insert)
                            dcDet.Id = _corDAL.GetnextLineId(TableNames.SAL_DCDetail.ToString(),mod.Id, "DCId");
                        retVal = _dcDAL.ManageDCDetail(dcDet,cmd);
                    }
                if (retVal == true)

                    mod.DBoperation = DBoperations.NA;
                MicroERPDataContext.EndTransaction(cmd);              
            }
           catch (Exception exp)
            {
                _logger.Error(exp);
                MicroERPDataContext.CancelTransaction(cmd);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
                string whereClause = " Where 1=1";
                mod.DCDetails = _dcDAL.SearchDCDetail(whereClause += $" AND DCId={mod.Id} AND IsActive ={true}");
            }
            return mod;
        }
        public List<DCDE> SearchDC(DCDE mod)
        {
            List<DCDE> vch = new List<DCDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.CustId != default && mod.CustId != 0)
                    whereClause += $" AND CustId={mod.CustId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive={mod.IsActive}";

                vch = _dcDAL.SearchDC(whereClause);
                foreach (var line in vch)
                {
                    whereClause = "where 1=1";
                    line.DCDetails = _dcDAL.SearchDCDetail(whereClause += $" AND DCId={line.Id} AND IsActive ={true}");
                }
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
            return vch;
        }
        #endregion
    }
}
