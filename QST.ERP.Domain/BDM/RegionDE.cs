using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    public class RegionDE : BaseDomain
    {
        public RegionDE()
        {
            RegionCode = string.Empty;
            RegionName = string.Empty;
        }

        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }
}
