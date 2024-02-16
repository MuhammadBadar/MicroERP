using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleStockController : ControllerBase
    {
        #region Class Variables

        private SaleStockService _SaleStockSVC;

        #endregion
        #region Constructors
        public SaleStockController()
        {
            _SaleStockSVC = new SaleStockService();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            SaleStockDE SaleStock = new SaleStockDE ();
            List<SaleStockDE> list = _SaleStockSVC.SearchSaleStocks(SaleStock);
            return Ok(list);
        }
        [HttpGet("{id}")]
        public ActionResult GetSaleStockById(int id)
        {
            SaleStockDE SaleStock = new SaleStockDE { Id = id };
            var list = _SaleStockSVC.SearchSaleStocks(SaleStock);
            return Ok(list);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(SaleStockDE Search)
        {
            //Search.IsActive = true;
            List<SaleStockDE> list = _SaleStockSVC.SearchSaleStocks(Search);
            return Ok(list);
        }

        [HttpPost]
        public ActionResult Post(SaleStockDE SaleStock)
        {
            SaleStock.DBoperation = DBoperations.Insert;
            bool retVal = _SaleStockSVC.ManagementSaleStock(SaleStock);
            return Ok(retVal);
        }

        [HttpPut]
        public ActionResult Put(SaleStockDE SaleStock)
        {
            SaleStock.DBoperation = DBoperations.Update;
            _SaleStockSVC.ManagementSaleStock(SaleStock);
            return Ok();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SaleStockDE SaleStock = new SaleStockDE { Id = id, DBoperation = DBoperations.DeActivate };
            _SaleStockSVC.ManagementSaleStock(SaleStock);
        }

        #endregion
    }
}
