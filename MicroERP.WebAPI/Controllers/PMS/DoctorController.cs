using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        #region DataMembers
        private readonly IBaseService<DoctorDE> _docSvc;
        //private DoctorService _docSvc;
        #endregion
        #region Constructor
        public DoctorController ( IBaseService<DoctorDE> docSvc )
        {
            _docSvc = docSvc;
            //_docSvc = new DoctorService();
        }
        #endregion
        #region MyRegion
        [HttpGet]
        public IActionResult GetDoctor ( )
        {
            List<DoctorDE> list = new List<DoctorDE> ();
            list = _docSvc.SearchData (new DoctorDE ());
            return Ok (list);
        }
        [HttpPost ("{Search}")]
        public IActionResult SearchDoctor ( DoctorDE doc )
        {
            List<DoctorDE> list = _docSvc.SearchData (doc);
            return Ok (list);
        }
        [HttpGet ("{id}")]
        public IActionResult GetDoctorById ( int id )
        {
            List<DoctorDE> list = new List<DoctorDE> ();
            list = _docSvc.SearchData (new DoctorDE { Id = id });
            return Ok (list[0]);

        }
        [HttpPost]
        public IActionResult PostDoctor ( DoctorDE doc )
        {
            doc.DBoperation = DBoperations.Insert;
            _docSvc.ManageData (doc);
            return Ok ();
        }
        [HttpPut]
        public IActionResult PutDoctor ( DoctorDE doc )
        {
            doc.DBoperation = DBoperations.Update;
            _docSvc.ManageData (doc);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public IActionResult DeleteDoctor ( int id )
        {
            DoctorDE docDe = new DoctorDE ();
            docDe.DBoperation = DBoperations.Delete;
            docDe.Id = id;
            _docSvc.ManageData (docDe);
            return Ok ();
        }
        #endregion
        #region HttpVerbs
        //[HttpGet]
        //public IActionResult GetDoctor ( )
        //{
        //    List<DoctorDE> list = new List<DoctorDE> ();
        //    list = _docSvc.SearchDoctor (new DoctorDE ());
        //    return Ok (list);
        //}
        //[HttpPost ("{Search}")]
        //public IActionResult SearchDoctor ( DoctorDE doc )
        //{
        //    List<DoctorDE> list = _docSvc.SearchDoctor (doc);
        //    return Ok (list);
        //}
        //[HttpGet ("{id}")]
        //public IActionResult GetDoctorById ( int id )
        //{
        //    List<DoctorDE> list = new List<DoctorDE> ();
        //    list = _docSvc.SearchDoctor (new DoctorDE { Id = id });
        //    return Ok (list[0]);

        //}
        //[HttpPost]
        //public IActionResult PostDoctor ( DoctorDE doc )
        //{
        //    doc.DBoperation = DBoperations.Insert;
        //    _docSvc.ManageDoctor (doc);
        //    return Ok ();
        //}
        //[HttpPut]
        //public IActionResult PutDoctor ( DoctorDE doc )
        //{
        //    doc.DBoperation = DBoperations.Update;
        //    _docSvc.ManageDoctor (doc);
        //    return Ok ();
        //}
        //[HttpDelete ("{id}")]
        //public IActionResult DeleteDoctor ( int id )
        //{
        //    DoctorDE docDe = new DoctorDE ();
        //    docDe.DBoperation = DBoperations.Delete;
        //    docDe.Id = id;
        //    _docSvc.ManageDoctor (docDe);
        //    return Ok ();
        //}
        #endregion
    }
}
