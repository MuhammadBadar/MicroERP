using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        #region DataMembers
        private StaffService _staffSvc;
        #endregion
        #region Constructor
        public StaffController ( )
        {
            _staffSvc = new StaffService();
        }
        #endregion
        #region HttpVerbs
        [HttpGet]
        public IActionResult GetStaff ( )
        {
            List<StaffDE> list = new List<StaffDE> ();
            list = _staffSvc.SearchStaff (new StaffDE ());
            return Ok (list);
        }
        [HttpPost ("{Search}")]
        public IActionResult SearchStaff ( StaffDE doc )
        {
            List<StaffDE> list = _staffSvc.SearchStaff (doc);
            return Ok (list);
        }
        [HttpGet ("{id}")]
        public IActionResult GetStaffById ( int id )
        {
            List<StaffDE> list = new List<StaffDE> ();
            list = _staffSvc.SearchStaff (new StaffDE { Id = id });
            return Ok (list[0]);

        }
        [HttpPost]
        public IActionResult PostStaff ( StaffDE doc )
        {
            doc.DBoperation = DBoperations.Insert;
            _staffSvc.ManageStaff (doc);
            return Ok ();
        }
        [HttpPut]
        public IActionResult PutStaff ( StaffDE doc )
        {
            doc.DBoperation = DBoperations.Update;
            _staffSvc.ManageStaff (doc);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public IActionResult DeleteStaff ( int id )
        {
            StaffDE docDe = new StaffDE ();
            docDe.DBoperation = DBoperations.Delete;
            docDe.Id = id;
            _staffSvc.ManageStaff (docDe);
            return Ok ();
        }
        #endregion
    }
}
