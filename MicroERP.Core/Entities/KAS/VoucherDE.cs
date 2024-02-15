using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class VoucherDE:BaseDomain
    {
        #region Properties
        public int VchTypeId { get; set; }
        public int VendorId { get; set; }
        public string? SalesmanId { get; set; }
        public int GodownId { get; set; }
        public int ApprovedById { get; set; }
        public string? VchNo { get; set; }
        public string? InvNo { get; set; }
        public string? DocNo { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? VchDate { get; set; }
        public string? Description { get; set; }
        public bool IsPosted { get; set; }
        public int StatusId { get; set; }
        public string? VchTypeKeyCode { get; set; }
        public List<VoucherDetailDE> VoucherDetails { get; set; }
        #endregion
        #region Views Properties
        public string? Vendor { get; set; }
        public string? Godown { get; set; }
        public string? Salesman { get; set; }
        public string? ApprovedBy { get; set; }
        public string? Status { get; set; }
        public string? VchType { get; set; }
        #endregion
        #region Contructor
        public VoucherDE()
        {
            this.VoucherDetails = new List<VoucherDetailDE>();
        }
        #endregion
    }
}
