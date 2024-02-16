using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Service;


namespace QST.MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class DocExtrasController : ControllerBase
    {
        #region Class Variables

        private DocExtrasService _DocExtrasSVC;

        #endregion
        #region Constructors
        public DocExtrasController ( )
        {
            _DocExtrasSVC = new DocExtrasService ();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get ( )
        {
            DocExtrasDE DocExtras = new DocExtrasDE ();
            List<DocExtrasDE> list = _DocExtrasSVC.SearchDocExtras (DocExtras);
            return Ok (list);
        }
        [HttpGet ("{id}")]
        public ActionResult GetDocExtrasById ( int id )
        {
            DocExtrasDE DocExtras = new DocExtrasDE { Id = id };
            var list = _DocExtrasSVC.SearchDocExtras (DocExtras);
            return Ok (list);
        }
        [HttpPost ("{Search}")]
        public ActionResult Search ( DocExtrasDE Search )
        {
            // Search.IsActive = true;
            List<DocExtrasDE> list = _DocExtrasSVC.SearchDocExtras (Search);
            return Ok (list);
        }
        [HttpPost]
        public ActionResult Post ( DocExtrasDE _attVal )
        {
            _attVal.DBoperation = DBoperations.Insert;
            bool retVla = _DocExtrasSVC.ManagementDocExtras (_attVal);
            return Ok (retVla);
        }
        [HttpPut]
        public ActionResult Put ( DocExtrasDE _attVal )
        {
            _attVal.DBoperation = DBoperations.Update;
            _DocExtrasSVC.ManagementDocExtras (_attVal);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public void Delete ( int id )
        {
            DocExtrasDE _attVal = new DocExtrasDE { Id = id, DBoperation = DBoperations.DeActivate };
            _DocExtrasSVC.ManagementDocExtras (_attVal);
        }

        #endregion
    }
}
