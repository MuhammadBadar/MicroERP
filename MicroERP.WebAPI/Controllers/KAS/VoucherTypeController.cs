using Microsoft.AspNetCore.Mvc;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherTypeController : ControllerBase
    {
        #region Class Variables

        private VoucherTypeService _VoucherTypeSVC;

        #endregion
        #region Constructors
        public VoucherTypeController()
        {
            _VoucherTypeSVC = new VoucherTypeService();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            VoucherTypeDE VoucherType = new VoucherTypeDE ();
            List<VoucherTypeDE> list = _VoucherTypeSVC.SearchVoucherTypes(VoucherType);
            return Ok(list);
        }
        [HttpGet("{id}")]
        public ActionResult GetVoucherTypeById(int id)
        {
            VoucherTypeDE VoucherType = new VoucherTypeDE { Id = id };
            var list = _VoucherTypeSVC.SearchVoucherTypes(VoucherType);
            return Ok(list);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(VoucherTypeDE Search)
        {
           // Search.IsActive = true;
            List<VoucherTypeDE> list = _VoucherTypeSVC.SearchVoucherTypes(Search);
            return Ok(list);
        }

        [HttpPost]
        public ActionResult Post(VoucherTypeDE VoucherType)
        {
            VoucherType.DBoperation = DBoperations.Insert;
            bool retVal = _VoucherTypeSVC.ManagementVoucherType(VoucherType);
            return Ok(retVal);
        }

        [HttpPut]
        public ActionResult Put(VoucherTypeDE VoucherType)
        {
            VoucherType.DBoperation = DBoperations.Update;
            _VoucherTypeSVC.ManagementVoucherType(VoucherType);
            return Ok();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            VoucherTypeDE VoucherType = new VoucherTypeDE { Id = id, DBoperation = DBoperations.DeActivate };
            _VoucherTypeSVC.ManagementVoucherType(VoucherType);
        }

        #endregion
    }
}
