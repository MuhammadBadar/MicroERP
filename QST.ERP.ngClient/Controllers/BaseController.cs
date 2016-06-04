using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http;
using QST.ERP.Services;
using QST.ERP.Domain.BDM;
using QST.ERP.Domain;

namespace QST.ERP.ngClient.Controllers
{
    public class BaseController : ApiController
    {
        private IBDMServiceContract _bdmSvc;
        private bool _mockingFlag;

        public BaseController()
        {
            _bdmSvc = new BDMService();
        }

        public List<RegionDE> CachedRegions
        { 
            get 
            {
                return _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
            } 
        }

        public List<CityDE> CachedCities
        {
            get 
            {
                return _bdmSvc.GetAllCities(AppConstants.SITE_CODE);
            }
        }

        public List<AreaDE> CachedAreas
        {
            get
            {
                return _bdmSvc.GetAllAreas(AppConstants.SITE_CODE);
            }
        }

        public AddressDE TranslateNames(AddressDE mod)
        {
            RegionDE region = CachedRegions.Where(m => m.RegionCode == mod.RegionCode).FirstOrDefault();
            if (region != null)
               mod.RegionName = region.RegionName;

            CityDE city = CachedCities.Where(m => m.CityCode == mod.CityCode).FirstOrDefault();
            if (city != null)
                mod.CityName = city.CityName;

            AreaDE area = CachedAreas.Where(m => m.AreaCode == mod.AreaCode).FirstOrDefault();
            if (area != null)
                mod.AreaName = area.AreaName;

            return mod;
        }
    }
}