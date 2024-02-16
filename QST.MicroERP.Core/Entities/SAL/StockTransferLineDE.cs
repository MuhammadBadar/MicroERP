using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class StockTransferLineDE:BaseDomain
    {
        #region Properties
        public int STId { get; set; }
        public int ProductId { get; set; }
        public int GodownId { get; set; }
        public double ProductUnits { get; set; }
        public int Qty { get; set; }
        public string? Description { get; set; }
        public string? Godown { get; set; }
        public string? Product { get; set; }
        #endregion
    }
}
