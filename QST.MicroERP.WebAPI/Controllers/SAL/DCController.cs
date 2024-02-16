using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DCController : ControllerBase
    {
        #region Class Variables
        private DCService _DCSVC;
        #endregion
        #region Constructor
        public DCController()
        {
            _DCSVC = new DCService();
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            DCDE DC = new DCDE ();
            List<DCDE> categories = _DCSVC.SearchDC(DC);
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public ActionResult GetDCById(int id)
        {
            DCDE DC = new DCDE { Id = id };
            var categories = _DCSVC.SearchDC(DC);
            return Ok(categories);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(DCDE Search)
        {
            //Search.IsActive = true;
            List<DCDE> categories = _DCSVC.SearchDC(Search);
            return Ok(categories);
        }
        [HttpPost]
        public ActionResult Post(DCDE _dc)
        {
            _dc.DBoperation = DBoperations.Insert;
            foreach (DCDetailDE line in _dc.DCDetails)
            {
                line.DBoperation = _dc.DBoperation;
            }
            DCDE DC = _DCSVC.ManagementDC(_dc);
            return Ok(DC);
        }
        [HttpPut]
        public ActionResult Put(DCDE _dc)
        {
            _dc.DBoperation = DBoperations.Update;
            DCDE DC = _DCSVC.ManagementDC(_dc);
            return Ok(DC);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DCDE DC = new DCDE { Id = id, DBoperation = DBoperations.Delete };
            _DCSVC.ManagementDC(DC);
        }

        #endregion
    }
}
