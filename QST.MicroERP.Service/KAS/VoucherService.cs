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
    public class VoucherService
    {
        #region Data Members

        private VoucherDAL _vchDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public VoucherService()
        {
            _vchDAL = new VoucherDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region Voucher
        public VoucherDE ManagementVoucher(VoucherDE mod, int? Id = null)
        {
            MySqlCommand cmd = null;
            try
            {
                bool retVal = false;
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                if (mod.DBoperation == DBoperations.Insert)
                {
                    if(mod.VchTypeKeyCode!=null)
                    mod.VchNo = GetNextVchNo(mod.VchTypeKeyCode, mod.ClientId);
                    mod.Id = _corDAL.GetNextIdByClient (TableNames.ACC_Voucher.ToString (), mod.ClientId, "ClientId");
                }
                retVal = _vchDAL.ManageVoucher(mod,cmd);
                var _Id = _corDAL.GetMaxLineIdByClt (TableNames.ACC_VoucherDetail.ToString (), "VchId", mod.Id, mod.ClientId);
                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                    foreach (var vchDet in mod.VoucherDetails)
                    {                    
                        vchDet.VchId = mod.Id;
                        vchDet.ClientId = mod.ClientId;
                        if (vchDet.DBoperation == DBoperations.Insert)
                        {
                            _Id += 1;
                            vchDet.Id = _Id;
                        }
                        retVal = _vchDAL.ManageVoucherDetail(vchDet,cmd);
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
                mod.VoucherDetails = _vchDAL.SearchVoucherDetail(whereClause += $" AND VchId={mod.Id} AND ClientId={mod.ClientId} AND IsActive ={true}");
            }
            return mod;
        }
        public List<VoucherDE> SearchVouchers(VoucherDE mod)
        {
            List<VoucherDE> vch = new List<VoucherDE>();
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
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.VchTypeKeyCode != default)
                    whereClause += $" AND VchTypeKeyCode like ''" + mod.VchTypeKeyCode + "''";

                vch = _vchDAL.SearchVouchers(whereClause);
                foreach (var line in vch)
                {
                    whereClause = "where 1=1";
                    line.VoucherDetails = _vchDAL.SearchVoucherDetail(whereClause += $" AND VchId={line.Id} AND ClientId={mod.ClientId} AND IsActive ={true}");
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
        public string GetNextVchNo ( string keyCode, int ClientId )
        {
            string vchNo;
            var vouchers = SearchVouchers (new VoucherDE { VchTypeKeyCode = keyCode, ClientId= ClientId });
            if (vouchers != null && vouchers.Count > 0)
                vchNo = keyCode + (vouchers.Count + 1).ToString ();
            else
                vchNo = keyCode + "1";
            return vchNo;

        }
        #endregion

    }
}
