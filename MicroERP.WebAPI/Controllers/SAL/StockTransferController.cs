using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransferController : ControllerBase
    {
        #region Class Variables
        private StockTransferService _StockTransferSVC;
        #endregion
        #region Constructor
        public StockTransferController()
        {
            _StockTransferSVC = new StockTransferService();
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            StockTransferDE StockTransfer = new StockTransferDE ();
            List<StockTransferDE> stockTransfers = _StockTransferSVC.SearchStockTransfers(StockTransfer);
            return Ok(stockTransfers);
        }
        [HttpGet("{id}")]
        public ActionResult GetStockTransferById(int id)
        {
            StockTransferDE StockTransfer = new StockTransferDE { Id = id };
            var stockTransfers = _StockTransferSVC.SearchStockTransfers(StockTransfer);
            return Ok(stockTransfers);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(StockTransferDE Search)
        {
            //Search.IsActive = true;
            List<StockTransferDE> stockTransfers = _StockTransferSVC.SearchStockTransfers(Search);
            return Ok(stockTransfers);
        }
        [HttpPost]
        public ActionResult Post(StockTransferDE vch)
        {
            vch.DBoperation = DBoperations.Insert;
            foreach (StockTransferLineDE line in vch.StockTransferLines)
            {
                line.DBoperation = vch.DBoperation;
            }
            StockTransferDE StockTransfer = _StockTransferSVC.ManagementStockTransfer(vch);
            return Ok(StockTransfer);
        }
        [HttpPut]
        public ActionResult Put(StockTransferDE vch)
        {
            vch.DBoperation = DBoperations.Update;
            StockTransferDE StockTransfer = _StockTransferSVC.ManagementStockTransfer(vch);
            return Ok(StockTransfer);
        }
        [HttpPut("{Update}/{Activate}")]
        public ActionResult Activate(StockTransferDE mod)
        {
            mod.DBoperation = DBoperations.Activate;
            _StockTransferSVC.ManagementStockTransfer(mod);
            return Ok();
        }
        [HttpPut("{DeActivate}")]
        public ActionResult DeActivate(StockTransferDE mod)
        {
            mod.DBoperation = DBoperations.DeActivate;
            _StockTransferSVC.ManagementStockTransfer(mod);
            return Ok();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            StockTransferDE StockTransfer = new StockTransferDE { Id = id, DBoperation = DBoperations.Delete };
            _StockTransferSVC.ManagementStockTransfer(StockTransfer);
        }

        #endregion
    }
}
