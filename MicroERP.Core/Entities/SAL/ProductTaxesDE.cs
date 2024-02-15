using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class ProductTaxesDE:BaseDomain
    {
        #region Properties
        public int ProductId { get; set; }
        public int TaxId { get; set; }
        public double Amount { get; set; }
        public string? Tax { get; set; }
        public string? Product { get; set; }
        public bool? IsVariant { get; set; }
        #endregion
    }
}
