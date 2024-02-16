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
    public class SaleService
    {
        #region Data Members

        private SaleDAL _saleDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public SaleService()
        {
            _saleDAL = new SaleDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region Sale
        public SaleDE ManagementSale(SaleDE mod, int? Id = null)
        {
            MySqlCommand cmd = null;
            try
            {
                bool retVal = false;
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.sale.ToString());
                retVal = _saleDAL.ManageSale(mod, cmd);
                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                    foreach (var PrchDet in mod.SaleLines)
                    {
                        PrchDet.SaleId = mod.Id;
                        if (PrchDet.DBoperation == DBoperations.Insert)
                            PrchDet.Id = _corDAL.GetnextLineId(TableNames.saleline.ToString(), mod.Id, "SaleId");
                        retVal = _saleDAL.ManageSaleLine(PrchDet, cmd);
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
                mod.SaleLines = _saleDAL.SearchSaleLine(whereClause += $" AND SaleId={mod.Id} AND IsActive ={true}");
            }
            return mod;
        }
        public List<SaleDE> SearchSales(SaleDE mod)
        {
            List<SaleDE> Prch = new List<SaleDE>();
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
                if (mod.SupplierId != default && mod.SupplierId != 0)
                    whereClause += $" AND SupplierId={mod.SupplierId}";
                if (mod.AcId != default && mod.AcId != 0)
                    whereClause += $" AND AcId={mod.AcId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive={mod.IsActive}";

                Prch = _saleDAL.SearchSale(whereClause);
                foreach (var line in Prch)
                {
                    whereClause = "where 1=1";
                    line.SaleLines = _saleDAL.SearchSaleLine(whereClause += $" AND SaleId={line.Id} AND IsActive ={true}");
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
