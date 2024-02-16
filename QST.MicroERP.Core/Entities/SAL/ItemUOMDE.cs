using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class ItemUOMDE:BaseDomain
    {
        #region Properties
        public int ItemId { get; set; }
        public int UOMId { get; set; }
        public int UOMTypeId { get; set; }
        public string? UOM { get; set; }
        public string? Item { get; set; }
        public string? UOMType { get; set; }
        public double SalePrice { get; set; }
        public double PurPrice { get; set; }
        #endregion
        public ItemUOMDE()
        {
            IsActive = true;
        }
    }
}
