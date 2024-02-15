using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Service
{
    public class StockTransferService
    {
        #region Data Members

        private StockTransferDAL _stDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public StockTransferService()
        {
            _stDAL = new StockTransferDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region StockTransfer
        public StockTransferDE ManagementStockTransfer(StockTransferDE mod, int? Id = null)
        {
            MySqlCommand cmd = null;
            try
            {
                bool retVal = false;
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.stocktransfer.ToString());
                retVal = _stDAL.ManageStockTransfer(mod, cmd);

                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                    foreach (var stLine in mod.StockTransferLines)
                    {
                        stLine.STId = mod.Id;
                        if (stLine.DBoperation == DBoperations.Insert)
                            stLine.Id = _corDAL.GetnextLineId(TableNames.stocktransferline.ToString(), mod.Id, "STId");
                        retVal = _stDAL.ManageStockTransferLine(stLine, cmd);
                    }

                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;


                MicroERPDataContext.EndTransaction(cmd);

                string whereClause = " Where 1=1";
                mod.StockTransferLines = _stDAL.SearchStockTransferLine(whereClause += $" AND STId={mod.Id} AND IsActive ={true}");
                return mod;
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
            }
        }
        public List<StockTransferDE> SearchStockTransfers(StockTransferDE mod)
        {
            List<StockTransferDE> stkTransfer = new List<StockTransferDE>();
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
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";

                stkTransfer = _stDAL.SearchStockTransfers(whereClause);
                foreach (var line in stkTransfer)
                {
                    whereClause = "where 1=1";
                    line.StockTransferLines = _stDAL.SearchStockTransferLine(whereClause += $" AND STId={line.Id} AND IsActive ={true}");
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
            return stkTransfer;
        }
        #endregion
    }
}
