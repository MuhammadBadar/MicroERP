using MySql.Data.MySqlClient;
using NLog;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.Service.CLT;

namespace QST.MicroERP.Service
{
    public class ItemService
    {
        #region Class Members/Class Variables

        private ItemDAL _itemDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        private ProductAttribDAL _proAttribDAL;
        public ItemVariantsDAL _itemVariantsDAL;
        public ItemUOMDAL _itemUOMDAL;
        public ItemUOMService _itemUOMSvc;
        public SettingsService _stngSvc;
        public ProductTaxesService _proTaxSvc;
        private List<AttributesDE> attributes;
        private List<AttributeValuesDE> attributeValues;

        #endregion
        #region Constructors
        public ItemService ( )
        {
            _itemDAL = new ItemDAL ();
            _proAttribDAL = new ProductAttribDAL ();
            _itemUOMDAL = new ItemUOMDAL ();
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
            _itemVariantsDAL = new ItemVariantsDAL ();
            _itemUOMSvc = new ItemUOMService ();
            _proTaxSvc = new ProductTaxesService ();
            _stngSvc = new SettingsService (null);
            attributes = new List<AttributesDE> ();
            attributeValues = new List<AttributeValuesDE> ();
        }

        #endregion
        #region Item
        public ItemDE ManagementItem ( ItemDE mod )
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                MicroERPDataContext.StartTransaction (cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetNextIdByClient (TableNames.CTL_Item.ToString (),mod.ClientId,"ClientId");
                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                {
                    #region ProductAttributes
                    if (mod.ProductAttribs.Count > 0)

                    {
                        var AttribId = _corDAL.GetMaxIdByClient (TableNames.SAL_ProductAttrib.ToString (),mod.ClientId,"ClientId");
                        foreach (var line in mod.ProductAttribs)
                        {
                            if (line.AttribValues != null)
                                foreach (var attrib in line.AttribValues)
                                {
                                    line.ClientId = mod.ClientId;
                                    line.ProductId = mod.Id;
                                    line.AttribValId = attrib;
                                    if (line.DBoperation == DBoperations.Insert)
                                    {
                                        AttribId += 1;
                                        line.Id = AttribId;
                                    }
                                    retVal = _proAttribDAL.ManageProductAttrib (line, cmd);
                                }
                        }
                    }
                    #endregion
                    #region ItemVaraints
                    if (mod.Variants.Count > 0)
                    {
                        this.GenerateVariant (mod);
                    }
                    #endregion
                    #region SaleUnits & PurchaseUnits
                    if (mod.ItemUOMList.Count > 0)
                    {
                        var uom = mod.ItemUOMList.Find (x => x.Id == 0);
                        if (uom != null)
                        {
                            if (mod.DBoperation == DBoperations.Update)
                            {
                                List<ItemUOMDE> itemUoms = new List<ItemUOMDE> ();
                                itemUoms = _itemUOMSvc.SearchItemUOM (new ItemUOMDE { ItemId = mod.Id ,ClientId=mod.ClientId});
                                if (itemUoms.Count > 0)
                                    foreach (var item in itemUoms)
                                    {
                                        item.DBoperation = DBoperations.Delete;
                                        _itemUOMDAL.ManageItemUOM (item);
                                    }
                            }
                            if (mod.SaleUnitIds.Count > 0)
                                mod.SaleUnits = string.Join (",", mod.SaleUnitIds.ToArray ());
                            if (mod.PurUnitIds.Count > 0)
                                mod.PurUnits = string.Join (",", mod.PurUnitIds.ToArray ());
                            var unitId = _corDAL.GetMaxIdByClient (TableNames.SAL_ItemUOM.ToString (),mod.ClientId,"ClientId");
                            foreach (var unit in mod.ItemUOMList)
                            {
                                unit.ClientId = mod.ClientId;
                                unit.ItemId = mod.Id;
                                unit.DBoperation = DBoperations.Insert;
                                unitId += 1;
                                unit.Id = unitId;
                                _itemUOMDAL.ManageItemUOM (unit);
                            }
                        }
                    }
                    #endregion
                }
                retVal = _itemDAL.ManageItem (mod);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;

                MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                MicroERPDataContext.CancelTransaction (cmd);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
                string whereClause = " Where 1=1";
                mod.ProductAttribs = _proAttribDAL.SearchProductAttrib (whereClause += $" AND ProductId={mod.Id} AND IsActive ={true} and ClientId={mod.ClientId}");
                whereClause = " Where 1=1";
                mod.Variants = _itemVariantsDAL.SearchItemVariants (whereClause += $" AND ItemId={mod.Id} AND IsActive ={true}   and ClientId={mod.ClientId}");
                var attrib = new AttributesDE ();
                foreach (var line in mod.ProductAttribs)
                {
                    attrib.Id = line.AttribId;
                    attrib.Name = line.Attribute;
                    attributes.Add (attrib);
                }
                attributes = attributes.Distinct ().ToList ();
                mod.Attributes = attributes;
            }
            return mod;
        }
        public void GenerateVariant ( ItemDE mod )
        {
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                // MicroERPDataContext.StartTransaction (cmd);
                string whereClause = " Where 1=1";
                var Variants = _itemVariantsDAL.SearchItemVariants (whereClause += $" AND ItemId={mod.Id} AND IsActive ={true} and ClientId={mod.ClientId}");
                if (Variants.Count > 0)
                    foreach (var variant in Variants)
                    {
                        var count = _itemVariantsDAL.GetUsedCount (variant.Id);
                        if (count > 0)
                            variant.DBoperation = DBoperations.DeActivate;
                        else
                            variant.DBoperation = DBoperations.Delete;
                        _itemVariantsDAL.ManageItemVariants (variant, cmd);
                    }
                foreach (var variant in mod.Variants)
                {
                    whereClause = " Where 1=1";
                    var _Variants = _itemVariantsDAL.SearchItemVariants (whereClause += $" AND" +
                        $" ItemId={mod.Id} and ClientId={mod.ClientId} and  AttributeValuesIds like ''" + variant.AttributeValuesIds + "''");
                    if (_Variants.Count > 0)
                    {
                        var _variant = _Variants.FirstOrDefault ();
                        if (_variant != null)
                        {
                            _variant.DBoperation = DBoperations.Update;
                            _variant.IsActive = true;
                            _itemVariantsDAL.ManageItemVariants (_variant, cmd);
                        }
                    }
                    else
                    {
                        variant.ItemId = mod.Id;
                        if (variant.DBoperation == DBoperations.Insert)
                        {
                            var Id = _corDAL.GetMaxVariantId (TableNames.SAL_ItemVariants.ToString (), "ItemId", mod.Id,mod.ClientId);
                            var regex = new Regex (Regex.Escape (mod.Id.ToString ()));
                            Id = int.Parse (regex.Replace (Id.ToString (), "", 1));
                            Id += 1;
                            variant.Id = int.Parse (mod.Id.ToString () + Id.ToString ());
                            variant.BarCode = variant.Id.ToString ();
                            variant.ClientId = mod.ClientId;
                        }
                        _itemVariantsDAL.ManageItemVariants (variant, cmd);
                    }
                }
                //MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception)
            {
                // MicroERPDataContext.CancelTransaction (cmd);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }

        }
        public List<ItemDE> SearchItems ( ItemDE mod )
        {
            List<ItemDE> Item = new List<ItemDE> ();
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
                if (mod.ModuleId != default)
                    whereClause += $" AND ModuleId={mod.ModuleId}";
                if (mod.ItemType != default)
                    whereClause += $" AND ItemType like ''%" + mod.ItemType + "%''";
                if (mod.TypeId != default && mod.TypeId != 0)
                    whereClause += $" AND TypeId={mod.TypeId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                if (mod.Name != default && mod.Name != "")
                    whereClause += $" AND Name like ''%" + mod.Name + "%''";
                if (mod.SaleRate != default)
                    whereClause += $" AND SaleRate like ''%" + mod.SaleRate + "%''";
                if (mod.PurRate != default)
                    whereClause += $" AND PurRate like ''%" + mod.PurRate + "%''";
                if (mod.SalePossibleUnits != default)
                    whereClause += $" AND SalePossibleUnits like ''%" + mod.SalePossibleUnits + "%''";
                if (mod.PurPossibleUnits != default)
                    whereClause += $" AND PurPossibleUnits like ''%" + mod.PurPossibleUnits + "%''";
                if (mod.RetailRate != default)
                    whereClause += $" AND RetailRate like ''%" + mod.RetailRate + "%''";

                Item = _itemDAL.SearchItems (whereClause);
                foreach (var item in Item)
                {
                    #region Attributes
                    whereClause = " Where 1=1";
                    item.ProductAttribs = _proAttribDAL.SearchProductAttrib (whereClause += $" AND ProductId={item.Id} AND IsActive ={true} and ClientId={item.ClientId}");
                    foreach (var line in item.ProductAttribs)
                    {
                        var attrib = new AttributesDE ();
                        attrib.Id = line.AttribId;
                        attrib.Name = line.Attribute;
                        attrib.IsActive = true;
                        if ((item.Attributes.Find (x => x.Id == attrib.Id)) == null)
                            item.Attributes.Add (attrib);
                        foreach (var _attrib in item.Attributes)
                        {
                            whereClause = " Where 1=1";
                            _attrib.ProductAttribs = _proAttribDAL.SearchProductAttrib (whereClause += $" AND ProductId={item.Id} AND AttribId={_attrib.Id} AND IsActive ={true} and ClientId={item.ClientId}");
                        }
                    }
                    #endregion
                    #region Variant
                    whereClause = " Where 1=1";
                    item.Variants = _itemVariantsDAL.SearchItemVariants (whereClause += $" AND ItemId={item.Id} AND IsActive ={true}  and ClientId={item.ClientId}");
                    #endregion
                    #region Sale & Purchase Units
                    if (item.SaleUnits != null)
                    {
                        List<string> result = item.SaleUnits.Split (',').ToList ();
                        item.SaleUnitIds = new List<int> ();
                        foreach (var unit in result)
                        {
                            item.SaleUnitIds.Add (int.Parse (unit));
                        }
                    }
                    if (item.PurUnits != null)
                    {
                        List<string> result = item.PurUnits.Split (',').ToList ();
                        item.PurUnitIds = new List<int> ();
                        foreach (var unit in result)
                        {
                            item.PurUnitIds.Add (int.Parse (unit));
                        }
                    }
                    #endregion
                    #region ItemUOM
                    whereClause = " Where 1=1";
                    item.ItemUOMList = _itemUOMDAL.SearchItemUOM (whereClause += $" AND ItemId={item.Id} AND IsActive ={true} and ClientId={item.ClientId}");

                    #endregion
                    #region Product Taxes
                    //List<SettingsDE> taxes=new List<SettingsDE> ();
                    //taxes = _stngSvc.SearchSettingss (new SettingsDE { EnumTypeId=(int)EnumType.Taxes});
                    //foreach (var tax in taxes)
                    //{
                    //    ProductTaxesDE proTax = new ProductTaxesDE ();
                    //    var _proTax = _proTaxSvc.SearchProductTaxes (new ProductTaxesDE { ProductId = item.Id, TaxId = tax.Id }).FirstOrDefault();
                    //    if (_proTax != null)
                    //        proTax = _proTax;
                    //    else
                    //    {
                    //        proTax.ProductId = item.Id;
                    //        proTax.TaxId=tax.Id;
                    //        proTax.Product = item.Name;
                    //        proTax.Tax = tax.Name;
                    //        proTax.Amount = 0;
                    //    }
                    //    item.ProductTaxes.Add (proTax);

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
