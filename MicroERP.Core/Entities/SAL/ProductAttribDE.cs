using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class ProductAttribDE:BaseDomain
    {
        #region Properties
        public int ProductId { get; set; }
        public int AttribId { get; set; }
        public int AttribValId { get; set; }
        public double PurRate { get; set; }
        public double SaleRate { get; set; }
        public string? Product { get; set; }
        public string? Attribute { get; set; }
        public string? AttributeValue { get; set; }
        public IList<int>? AttribValues { get; set; }
        #endregion
    }
}
