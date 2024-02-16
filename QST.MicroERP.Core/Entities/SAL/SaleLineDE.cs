using QST.MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class SaleLineDE:BaseDomain
    {
        #region properties
        public int ProductId { get; set; }
        public int SaleQty { get; set; }
        public int IssueQty { get; set; }
        public int RetQty { get; set; }
        public double SaleRate { get; set; }
        public double DiscRate { get; set; }
        public double GSTRate { get; set; }
        public double GSTRetailRate { get; set; }
        public double FTaxRate { get; set; }
        public double WhtRate { get; set; }
        public double Disc { get; set; }
        public double QtyDisc { get; set; }
        public double BulkDisc { get; set; }
        public double GST { get; set; }
        public double GSTRet { get; set; }
        public double FTax { get; set; }
        public double Wht { get; set; }
        public double ChrgsAdd { get; set; }
        public double ChrgsLess { get; set; }
        public double RetailRate { get; set; }
        public double Amount { get; set; }
        public int SaleId { get; set; }
        public string? Product { get; set; }
        public string? Description { get; set; }
        public int ItemVariantId { get; set; }
        public string? ProductAttribIds { get; set; }
        public int SaleUnitId { get; set; }
        public string? SaleUnit { get; set; }

        #endregion
    }
}
