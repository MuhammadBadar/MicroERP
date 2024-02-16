using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        #region Class Variables
        private PurchaseService _PurchaseSVC;
        #endregion
        #region Constructor
        public PurchaseController()
        {
            _PurchaseSVC = new PurchaseService();
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            PurchaseDE Purchase = new PurchaseDE ();
            List<PurchaseDE> categories = _PurchaseSVC.SearchPurchases(Purchase);
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public ActionResult GetPurchaseById(int id)
        {
            PurchaseDE Purchase = new PurchaseDE { Id = id };
            var categories = _PurchaseSVC.SearchPurchases(Purchase);
            return Ok(categories);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(PurchaseDE Search)
        {
            //Search.IsActive = true;
            List<PurchaseDE> categories = _PurchaseSVC.SearchPurchases(Search);
            return Ok(categories);
        }
        [HttpPost]
        public ActionResult Post(PurchaseDE Prch)
        {
            Prch.DBoperation = DBoperations.Insert;
            foreach (PurchaseLineDE line in Prch.PurchaseLines)
            {
                line.DBoperation = Prch.DBoperation;
            }
            PurchaseDE Purchase = _PurchaseSVC.ManagementPurchase(Prch);
            return Ok(Purchase);
        }
        [HttpPut]
        public ActionResult Put(PurchaseDE Prch)
        {
            Prch.DBoperation = DBoperations.Update;
            PurchaseDE Purchase = _PurchaseSVC.ManagementPurchase(Prch);
            return Ok(Purchase);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PurchaseDE Purchase = new PurchaseDE { Id = id, DBoperation = DBoperations.DeActivate };
            _PurchaseSVC.ManagementPurchase(Purchase);
        }

        #endregion
    }
}
