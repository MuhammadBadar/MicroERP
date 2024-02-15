using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        #region Class Variables
        private SaleService _SaleSVC;
        #endregion
        #region Constructor
        public SaleController()
        {
            _SaleSVC = new SaleService();
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            SaleDE Sale = new SaleDE ();
            List<SaleDE> categories = _SaleSVC.SearchSales(Sale);
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public ActionResult GetSaleById(int id)
        {
            SaleDE Sale = new SaleDE { Id = id };
            var categories = _SaleSVC.SearchSales(Sale);
            return Ok(categories);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(SaleDE Search)
        {
            //Search.IsActive = true;
            List<SaleDE> categories = _SaleSVC.SearchSales(Search);
            return Ok(categories);
        }
        [HttpPost]
        public ActionResult Post(SaleDE Prch)
        {
            Prch.DBoperation = DBoperations.Insert;
            foreach (SaleLineDE line in Prch.SaleLines)
            {
                line.DBoperation = Prch.DBoperation;
            }
            SaleDE Sale = _SaleSVC.ManagementSale(Prch);
            return Ok(Sale);
        }
        [HttpPut]
        public ActionResult Put(SaleDE Prch)
        {
            Prch.DBoperation = DBoperations.Update;
            SaleDE Sale = _SaleSVC.ManagementSale(Prch);
            return Ok(Sale);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SaleDE Sale = new SaleDE { Id = id, DBoperation = DBoperations.Delete };
            _SaleSVC.ManagementSale(Sale);
        }

        #endregion
    }
}
