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
using MicroERP.Service.CLT;
using MicroERP.Core.Entities.CTL;

namespace MicroERP.Service
{
    public class ProductTaxesService
    {
        #region Class Members/Class Variables

        private ProductTaxesDAL _proTaxDAL;
        public SettingsService _stngSvc;
        private CoreDAL _corDAL;
        private Logger _logger;
        #endregion
        #region Constructors
        public ProductTaxesService ( )
        {
            _proTaxDAL = new ProductTaxesDAL ();
            _stngSvc = new SettingsService (null);
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }

        #endregion
        #region ProductTaxes
        public bool ManagementProductTaxes ( ProductTaxesDE mod )
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();

                var proTax = SearchProductTaxes (new ProductTaxesDE { ProductId = mod.ProductId, TaxId = mod.TaxId , ClientId=mod.ClientId}).FirstOrDefault ();
                if (proTax != null)
                    mod.DBoperation = DBoperations.Update;
                else
                    mod.DBoperation = DBoperations.Insert;
                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetNextIdByClient (TableNames.producttaxes.ToString (), mod.ClientId,"ClientId");
                retVal = _proTaxDAL.ManageProductTaxes (mod);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<ProductTaxesDE> SearchProductTaxes ( ProductTaxesDE mod )
        {
            List<ProductTaxesDE> ProductTaxes = new List<ProductTaxesDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.ProductId != default && mod.ProductId != 0)
                    whereClause += $" AND ProductId={mod.ProductId}";
                if (mod.TaxId != default && mod.TaxId != 0)
                    whereClause += $" AND TaxId={mod.TaxId}";
                if (mod.IsVariant != default)
                    whereClause += $" AND IsVariant ={mod.IsVariant}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";

                ProductTaxes = _proTaxDAL.SearchProductTaxes (whereClause);
                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error (exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return ProductTaxes;
        }
        public List<ProductWithVariantsVM> SearchItemswithVariants ( ProductWithVariantsVM mod )
        {
            List<ProductWithVariantsVM> Item = new List<ProductWithVariantsVM> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ProdName != default)
                    whereClause += $" AND ProdName like ''" + mod.ProdName + "''";
                if (mod.IsVariant != default)
                    whereClause += $" AND IsVariant ={mod.IsVariant}";

                Item = _proTaxDAL.SearchItemswithVariants (whereClause);
                foreach (var item in Item)
                {
                    #region Product Taxes
                    List<SettingsDE> taxes = new List<SettingsDE> ();
                    taxes = _stngSvc.SearchSettingss (new SettingsDE { EnumTypeId = (int)EnumType.Taxes , ClientId=item.ClientId});
                    foreach (var tax in taxes)
                    {
                        ProductTaxesDE proTax = new ProductTaxesDE ();
                        var _proTax = SearchProductTaxes (
                            new ProductTaxesDE { ProductId = item.Id, TaxId = tax.Id, ClientId=tax.ClientId }).FirstOrDefault ();
                        if (_proTax != null)
                            proTax = _proTax;
                        else
                        {
                            proTax.ProductId = item.Id;
                            proTax.TaxId = tax.Id;
                            proTax.Product = item.ProdName;
                            proTax.Tax = tax.Name;
                            proTax.Amount = 0;
                            proTax.IsVariant = item.IsVariant;
                        }
                        item.ProductTaxes.Add (proTax);
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error (exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return Item;
        }
        #endregion
    }
}
