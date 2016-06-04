using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    public class AddressDE : BaseDomain
    {
        public AddressDE() { AddressTypeCode = AddressTypes.Personal.ToString(); }
        public AddressDE(string addressTypeCode)
        {
            this.AddressTypeCode = addressTypeCode;
        }

        public decimal EntityID { get; set; }
        public string AddressTypeCode { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string AreaCode { get; set; }
        public string AreaName { get; set; }

        public string CityCode { get; set; }
        public string CityName { get; set; }

        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        
        public string StateCode { get; set; }
        public string StateName { get; set; }

        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public virtual EntityDE EntityDE { get; set; }
    }
}
