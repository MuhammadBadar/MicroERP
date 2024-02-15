using Microsoft.AspNetCore.Mvc;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Core.ViewModel;
using MicroERP.Service;

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttribController : ControllerBase
    {
        #region Class Variables

        private ProductAttribService _ProductAttribSVC;

        #endregion
        #region Constructors
        public ProductAttribController()
        {
            _ProductAttribSVC = new ProductAttribService();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            ProductAttribDE ProductAttrib = new ProductAttribDE ();
            List<ProductAttribDE> ProductAttribs = _ProductAttribSVC.SearchProductAttrib(ProductAttrib);
            return Ok(ProductAttribs);
        }
        [HttpGet("{id}")]
        public ActionResult GetProductAttribById(int id)
        {
            ProductAttribDE ProductAttrib = new ProductAttribDE { Id = id };
            var ProductAttribs = _ProductAttribSVC.SearchProductAttrib(ProductAttrib);
            return Ok(ProductAttribs);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(ProductAttribDE Search)
        {
            //Search.IsActive = true;
            List<ProductAttribDE> ProductAttrib = _ProductAttribSVC.SearchProductAttrib(Search);
            return Ok(ProductAttrib);
        }
        [HttpPost]
        public ActionResult Post(ProductAttribDE _att)
        {
            _att.DBoperation = DBoperations.Insert;
            bool retVla = _ProductAttribSVC.ManagementProductAttrib(_att);
            return Ok(retVla);
        }
        [HttpPut]
        public ActionResult Put(ProductAttribDE _att)
        {
            _att.DBoperation = DBoperations.Update;
            _ProductAttribSVC.ManagementProductAttrib(_att);
            return Ok();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ProductAttribDE _att = new ProductAttribDE { Id = id, DBoperation = DBoperations.DeActivate };
            _ProductAttribSVC.ManagementProductAttrib(_att);
        }

        #endregion
    }
}
