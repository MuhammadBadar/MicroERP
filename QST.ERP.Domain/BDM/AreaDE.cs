using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    public class AreaDE : BaseDomain
    {
        public AreaDE()
        {
            RegionCode = string.Empty;
            CityCode = string.Empty;
            AreaCode = string.Empty;
            AreaName = string.Empty;
        }
        public string RegionCode { get; set; }
        public string CityCode { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
    }
}
