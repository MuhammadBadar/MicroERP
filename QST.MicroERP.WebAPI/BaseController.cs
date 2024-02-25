using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Service.CTL;

namespace QST.MicroERP.WebAPI
{
    public class BaseController : ControllerBase
    {
        internal CatalogueService _catSvc;
        public BaseController()
        {
            _catSvc = new CatalogueService();
        }
    }
}
