using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Service;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ProductTaxesController : ControllerBase
    {
        #region Class Variables

        private ProductTaxesService _proTaxesSVC;

        #endregion
        #region Constructors
        public ProductTaxesController ( )
        {
            _proTaxesSVC = new ProductTaxesService ();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get ( )
        {
            ProductTaxesDE ProductTaxes = new ProductTaxesDE ();
            List<ProductTaxesDE> ProductTaxess = _proTaxesSVC.SearchProductTaxes (ProductTaxes);
            return Ok (ProductTaxess);
        }
        [HttpGet ("{id}")]
        public ActionResult GetProductTaxesById ( int id )
        {
            ProductTaxesDE ProductTaxes = new ProductTaxesDE { Id = id };
            var ProductTaxess = _proTaxesSVC.SearchProductTaxes (ProductTaxes);
            return Ok (ProductTaxess);
        }
        [HttpPost ("{Search}")]
        public ActionResult Search ( ProductTaxesDE Search )
        {
            // Search.IsActive = true;
            List<ProductTaxesDE> ProductTaxes = _proTaxesSVC.SearchProductTaxes (Search);
            return Ok (ProductTaxes);
        }
        [HttpPost ("Product")]
        public ActionResult SearchProductwithVariant ( ProductWithVariantsVM Search )
        {
            var strComputerName = Environment.MachineName.ToString ();
            List<ProductWithVariantsVM> Products = _proTaxesSVC.SearchItemswithVariants (Search);
            return Ok (Products);
        }
        [HttpPost]
        public ActionResult Post ( ProductTaxesDE _attVal )
        {
            _attVal.DBoperation = DBoperations.Insert;
            bool retVla = _proTaxesSVC.ManagementProductTaxes (_attVal);
            return Ok (retVla);
        }
        [HttpPut]
        public ActionResult Put ( ProductTaxesDE _attVal )
        {
            _attVal.DBoperation = DBoperations.Update;
            _proTaxesSVC.ManagementProductTaxes (_attVal);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public void Delete ( int id )
        {
            ProductTaxesDE _attVal = new ProductTaxesDE { Id = id, DBoperation = DBoperations.DeActivate };
            _proTaxesSVC.ManagementProductTaxes (_attVal);
        }

        #endregion
    }
}
