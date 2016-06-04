using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    public class AreaVw :BaseDomain
    {
         public AreaVw()
        {
            RegionCode = string.Empty;
            RegionName = string.Empty;

            CityCode = string.Empty;
            CityName = string.Empty;

            AreaCode = string.Empty;
            AreaName = string.Empty;
        }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }

        public string CityCode { get; set; }
        public string CityName { get; set; }

        public string AreaCode { get; set; }
        public string AreaName { get; set; }
    }
}
