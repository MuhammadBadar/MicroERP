using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class VoucherDetailDE:BaseDomain
    {
        #region Properties
        public int VchId { get; set; }
        public int AcId { get; set; }
        public int BillId { get; set; }
        public string? Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int Rate { get; set; }
        public Boolean  IsDefaultDrCr { get; set; }
        #endregion
        #region Views Properties
        public string? AcName { get; set; }
        public string? Product { get; set; }
        #endregion
    }
}
