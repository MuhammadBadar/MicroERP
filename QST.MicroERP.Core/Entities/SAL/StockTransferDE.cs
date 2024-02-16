using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class StockTransferDE:BaseDomain
    {
        #region Properties
        public DateTime? Date { get; set; }
        public string? InvNo { get; set; }
        public int TransferTo { get; set; }
        public int TransferFrom { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public List<StockTransferLineDE> StockTransferLines { get; set; }
        #endregion
        #region Constructor
        public StockTransferDE()
        {
            this.StockTransferLines = new List<StockTransferLineDE>();
        }
        #endregion
    }
}
