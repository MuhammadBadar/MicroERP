using QST.MicroERP.Core.Enums;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class PurchaseLineDE : BaseDomain
    {
        #region properties
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public double PurchaseRate { get; set; }
        public double DiscPer { get; set; }
        public double GSTRate { get; set; }
        public double GSTRetailRate { get; set; }
        public double RetailRate { get; set; }
        public double Amount { get; set; }
        public int PrchId {get; set;}
        public string? Description { get; set; }
        public int ItemVariantId { get; set; }
        public string? ProductAttribIds { get; set; }
        public string? Product { get; set; }
        public int PurUnitId { get; set; }
        public string? PurUnit { get; set; }

        #endregion
    }
        
}
