using QST.MicroERP.Core.Entities.LMS;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service.LMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers.LMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private LectureService _lecSvc;
        public LectureController()
        {
            _lecSvc = new LectureService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetLecture()
        {
            List<LectureDE> list = new List<LectureDE>();
            list = _lecSvc.SearchLecture(new LectureDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchLecture(LectureDE lec)
        {
            List<LectureDE> list = _lecSvc.SearchLecture(lec);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetLectureById(int id)
        {
            List<LectureDE> list = new List<LectureDE>();
            list = _lecSvc.SearchLecture(new LectureDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostLecture(LectureDE lec)
        {
            lec.DBoperation = DBoperations.Insert;
            _lecSvc.ManageLecture(lec);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutLecture(LectureDE lec)
        {
            lec.DBoperation = DBoperations.Update;
            _lecSvc.ManageLecture(lec);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLecture(int id)
        {
            LectureDE lecDe = new LectureDE();
            lecDe.DBoperation = DBoperations.Delete;
            lecDe.Id = id;
            _lecSvc.ManageLecture(lecDe);
            return Ok();
        }
    }
}
