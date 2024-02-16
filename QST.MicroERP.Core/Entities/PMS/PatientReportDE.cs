using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class PatientReportDE:BaseDomain
    {
        public int? RxId { get; set; }
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }
        public string? Category { get; set; }
        public string? ReportBase64Path { get; set; }
        public string? Name { get; set; }
    }
}
