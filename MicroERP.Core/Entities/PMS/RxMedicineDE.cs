using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class RxMedicineDE:BaseDomain
    {
        public int? RxId { get; set; }
        public int? MedId { get; set; }
        public int? MRId { get; set; }
        public int? AMQty { get; set; }
        public int? NoonQty { get; set; }
        public int? EveQty { get; set; }
        public string? Remarks { get; set; }
        public int? RemarksId { get; set; }
        public int? Days { get; set; }
        public string? Medicine { get; set; }
        public string? MR { get; set; }

        public RxMedicineDE()
        {
       
        }
    }
}
