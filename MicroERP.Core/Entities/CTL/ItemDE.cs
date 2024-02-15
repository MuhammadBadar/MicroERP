using MicroERP.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class ItemDE : BaseDomain
    {
        #region properties
        public string? Name { get; set; }
        public int VendorId { get; set; }
        public int TypeId { get; set; }
        public int ModuleId { get; set; }
        public double? PurRate { get; set; }
        public double? SaleRate { get; set; }
        public double? Conversion { get; set; }
        public double? GstSaleRate { get; set; }
        public double? GstPurRate { get; set; }
        public double? RetailRate { get; set; }
        public double? SaleStRate { get; set; }
        public double? PurStRate { get; set; }
        public string? PurUnits { get; set; }
        public string? SaleUnits  { get; set; }
        public double? ExtraRate { get; set; }
        public double? PrMazdoori { get; set; }
        public double? UnitPrice { get; set; }
        public double? UnitsInStock { get; set; }
        public int ManufacturersId { get; set; }
        public string? Manufacturer { get; set; }
        public string? Formula { get; set; }
        public string? Category { get; set; }
        public int CategoryId { get; set; }
        public string? Remarks { get; set; }
        public List<ProductAttribDE> ProductAttribs { get; set; }
        public List<AttributesDE> Attributes { get; set; }
        public List<ItemVariantsDE> Variants { get; set; }
        public List<ItemUOMDE> ItemUOMList { get; set; }
        public List<int> PurUnitIds { get; set; }
        public List<int> SaleUnitIds { get; set; }
        public string? PurPossibleUnits { get; set; }
        public string? SalePossibleUnits { get; set; }
        public List<ProductTaxesDE> ProductTaxes { get; set; }
        public string? ItemType { get; set; }

        #endregion
        #region Constructor
        public ItemDE()
        {
            this.ItemUOMList = new List<ItemUOMDE> ();
            this.Attributes = new List<AttributesDE>();
            this.ProductAttribs = new List<ProductAttribDE>();
            this.Variants= new List<ItemVariantsDE> ();
            ProductTaxes = new List<ProductTaxesDE> ();
             PurUnitIds = new List<int>();
            SaleUnitIds = new List<int>();
        }
        #endregion
    }
}
