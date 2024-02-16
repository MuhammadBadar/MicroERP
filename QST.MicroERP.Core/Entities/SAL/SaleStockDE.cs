using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class SaleStockDE:BaseDomain
    {
        #region Properties
        public int SaleTransId { get; set; }
        public int ProductId { get; set; }
        public int GodownId { get; set; }
        public int DebitId { get; set; }
        public int JobId { get; set; }
        public int PurchaseQty { get; set; }
        public int IssueQty { get; set; }
        public int ReturnQty { get; set; }
        public int SaleQty { get; set; }
        public int FreeQty { get; set; }
        public int SaleRate { get; set; }
        public int SaleGstRate { get; set; }
        public int ReturnGstRate { get; set; }
        public int DiscRate { get; set; }
        public int DiscAmt { get; set; }
        public int ExtraRate { get; set; }
        public string? Description { get; set; }
        public int Conversion { get; set; }
        public int RetailRate { get; set; }
        public string? SoNo { get; set; }
        #endregion
    }
}
