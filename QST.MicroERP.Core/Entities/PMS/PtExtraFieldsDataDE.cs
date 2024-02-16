using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class PtExtraFieldsDataDE:BaseDomain
    {
        public int PatientId { get; set; }
        public int FieldId { get; set; }
        public string? FieldName { get; set; }
        public string? FieldValue { get; set; }
        public string? Type { get; set; }
    }
}
