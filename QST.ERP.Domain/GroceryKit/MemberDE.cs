using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.GroceryKit
{
    public class MemberDE : FormDE
    {
        public DateTime RegDate { get; set; }
        public string RegNo { get; set; }
        public int DistributionPointId { get; set; }
        public int FamilyPackageId { get; set; }
    }
}
