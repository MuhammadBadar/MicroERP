using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.AlKhair
{
    public class VoucherBE : BaseDomain
    {
        public VoucherBE()
        {
            VchTypeCode = "RV";  // Receipt Voucher
            VchMonth = DateTime.Now.Month;
            VchYear = DateTime.Now.Year;
            VchDate = DateTime.Now;
        }
        public string VchNo { get; set; }
        public int GiftTypeId { get; set; }
        //public string MyProperty { get; set; }
        public int ReceivedFrom { get; set; }
        public string VchTypeCode { get; set; }
        public int VchMonth { get; set; }
        public int VchYear { get; set; }
        public string VchKeyId { get; set; }
        public DateTime VchDate { get; set; }
        public decimal VchAmount { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public int BankId { get; set; }

        
        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string ReceivedFromPhone { get; set; }
    }
}
