using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class VoucherTypeDE : BaseDomain
    {
        public string? Name { get; set; }
        public Boolean DefaultDrCrFirst { get; set; }
        public string? KeyCode { get; set; }
        public int DefaultDrCrSecondId { get; set; }
        public string? DefaultDrCrSecond { get; set; }
    }
}
