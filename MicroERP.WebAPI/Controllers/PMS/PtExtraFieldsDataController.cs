using Microsoft.AspNetCore.Mvc;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;

namespace MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class PtExtraFieldsDataController : ControllerBase
    {
        #region Class Variables

        private PtExtraFieldsDataService _exfdSvc;

        #endregion
        #region Constructors
        public PtExtraFieldsDataController ( )
        {
            _exfdSvc = new PtExtraFieldsDataService ();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get ( )
        {
            PtExtraFieldsDataDE PtExtraFieldsData = new PtExtraFieldsDataDE ();
            List<PtExtraFieldsDataDE> list = _exfdSvc.SearchPtExtraFieldsData (PtExtraFieldsData);
            return Ok (list);
        }
        [HttpGet ("{id}")]
        public ActionResult GetPtExtraFieldsDataById ( int id )
        {
            PtExtraFieldsDataDE PtExtraFieldsData = new PtExtraFieldsDataDE { Id = id };
            var list = _exfdSvc.SearchPtExtraFieldsData (PtExtraFieldsData);
            return Ok (list);
        }
        [HttpPost ("{Search}")]
        public ActionResult Search ( PtExtraFieldsDataDE Search )
        {
            //Search.IsActive = true;
            List<PtExtraFieldsDataDE> list = _exfdSvc.SearchPtExtraFieldsData (Search);
            return Ok (list);
        }
        [HttpPost]
        public ActionResult Post ( PtExtraFieldsDataDE mod )
        {
            mod.DBoperation = DBoperations.Insert;
            bool retVal = _exfdSvc.ManagePtExtraFieldsData (mod);
            return Ok (retVal);
        }
        [HttpPut]
        public ActionResult Put ( PtExtraFieldsDataDE mod )
        {
            mod.DBoperation = DBoperations.Update;
            _exfdSvc.ManagePtExtraFieldsData (mod);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public void Delete ( int id )
        {
            PtExtraFieldsDataDE mod = new PtExtraFieldsDataDE { Id = id, DBoperation = DBoperations.DeActivate };
            _exfdSvc.ManagePtExtraFieldsData (mod);
        }

        #endregion
    }
}
