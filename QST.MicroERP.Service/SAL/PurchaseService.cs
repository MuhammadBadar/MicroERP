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
    public class PurchaseService
    {
        #region Data Members

        private PurchaseDAL _PrchDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public PurchaseService()
        {
            _PrchDAL = new PurchaseDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region Purchase
        public PurchaseDE ManagementPurchase(PurchaseDE mod, int? Id = null)
        {
            MySqlCommand cmd = null;
            try
            {
                bool retVal = false;
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.PUR_Purchase.ToString());
                retVal = _PrchDAL.ManagePurchase(mod, cmd);
                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                    foreach (var PrchDet in mod.PurchaseLines)
                    {
                        PrchDet.PrchId = mod.Id;
                        if (PrchDet.DBoperation == DBoperations.Insert)
                            PrchDet.Id = _corDAL.GetnextLineId(TableNames.PUR_PurchaseLine.ToString(), mod.Id, "PrchId");
                        retVal = _PrchDAL.ManagePurchaseLine(PrchDet, cmd);
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
                mod.PurchaseLines = _PrchDAL.SearchPurchaseLine(whereClause += $" AND PrchId={mod.Id} AND IsActive ={true}");
            }
            return mod;
        }
        public List<PurchaseDE> SearchPurchases(PurchaseDE mod)
        {
            List<PurchaseDE> Prch = new List<PurchaseDE>();
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
                if (mod.SupplierId != default && mod.SupplierId !=0)
                    whereClause += $" AND SupplierId={mod.SupplierId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive={mod.IsActive}";

                Prch = _PrchDAL.SearchPurchase(whereClause);
                foreach (var line in Prch)
                {
                    whereClause = "where 1=1";
                    line.PurchaseLines = _PrchDAL.SearchPurchaseLine(whereClause += $" AND PrchId={line.Id} AND IsActive ={true}");
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
            return Prch;
        }
        #endregion
    }
}
