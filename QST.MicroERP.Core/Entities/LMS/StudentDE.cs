using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.LMS
{
    public class StudentDE : BaseDomain
    {
        public int? CityId { get; set; }
        public string? City { get; set; }
        public string? CellNo { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

    }
}
