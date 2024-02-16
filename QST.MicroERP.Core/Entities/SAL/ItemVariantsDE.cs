using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class ItemVariantsDE:BaseDomain
    {
        #region Properties
        public int ItemId { get; set; }
        public string? AttributeValuesIds { get; set; }
        public double? SaleExtraRate { get; set; }
        public double? PurchaseExtraRate { get; set; }
        public string? BarCode { get; set; }
        public double? StockValue { get; set; }
        public string? PossibleValues { get; set; }
        public string? Item { get; set; }

        #endregion
    }
}
