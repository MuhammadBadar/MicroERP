using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    public class ContactDE : BaseDomain
    {
        public ContactDE()
        {
            Phone1 = string.Empty;
            Phone2 = string.Empty;
            Mobile1 = string.Empty;
            Mobile2 = string.Empty;
        }
        //public string SiteCode { get; set; }
        public decimal EntityID { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }

        public virtual EntityDE EntityDE { get; set; }
    }
}
