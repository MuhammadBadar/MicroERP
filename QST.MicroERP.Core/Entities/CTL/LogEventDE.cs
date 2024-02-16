using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.CTL
{
    public class LogEventDE : BaseDomain
    {
        public string? UserId { get; set; }
        public string? User { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public DateTime? Date { get; set; }
        public string? Message { get; set; }
    }
}
