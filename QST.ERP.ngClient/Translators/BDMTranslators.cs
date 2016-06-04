using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using QST.ERP.Domain.BDM;

namespace QST.ERP.ngClient.Translators
{
    public static class BDMTranslators
    {
        public static AddressDE Translate(this AddressDE src, AddressDE dest)
        {
            dest.AddressLine1 = src.AddressLine1;
            dest.AddressLine2 = src.AddressLine2;
            dest.AreaCode = src.AreaCode;
            dest.AreaName = src.AreaName;
            dest.CityCode = src.CityCode;
            dest.CityName = src.CityName;
            dest.CountryCode = src.CountryCode;
            dest.CountryName = src.CountryName;
            dest.RegionCode = src.RegionCode;
            dest.RegionName = src.RegionName;

            return dest;
        }

        public static ContactDE Translate(this ContactDE src, ContactDE dest)
        {
            dest.Mobile1 = src.Mobile1;
            dest.Mobile2 = src.Mobile2;
            dest.Phone1 = src.Phone1;
            dest.Phone2 = src.Phone2;

            return dest;
        }
    }
}