using MySql.Data.MySqlClient;
using NLog;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Core.ViewModel;
using MicroERP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroERP.DAL.CTL;

namespace MicroERP.Service
{
    public class ProductAttribService
    {
        #region Class Members/Class Variables

        private ProductAttribDAL _attDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        #endregion
        #region Constructors
        public ProductAttribService()
        {
            _attDAL = new ProductAttribDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }

        #endregion
        #region ProductAttrib
        public bool ManagementProductAttrib(ProductAttribDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetNextIdByClient(TableNames.productattrib.ToString(), mod.ClientId,"ClientId");
                retVal = _attDAL.ManageProductAttrib(mod,cmd);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<ProductAttribDE> SearchProductAttrib(ProductAttribDE mod)
        {
            List<ProductAttribDE> ProductAttrib = new List<ProductAttribDE>();
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
                if (mod.ProductId != default)
                    whereClause += $" AND ProductId={mod.ProductId}";
                if (mod.AttribId != default)
                    whereClause += $" AND AttribId={mod.AttribId}";
                if (mod.AttribValId != default)
                    whereClause += $" AND AttribValId={mod.AttribValId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";

                ProductAttrib = _attDAL.SearchProductAttrib(whereClause);
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
            return ProductAttrib;
        }

        #endregion
    }
}
