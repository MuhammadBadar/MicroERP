using MicroERP.Core.Entities.LMS;
using MicroERP.Core.Enums;
using MicroERP.Service.LMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers.LMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeepaymentschoolController : ControllerBase
    {
        private FeepaymentschoolService _feepaymentschoolSvc;
        public FeepaymentschoolController()
        {
            _feepaymentschoolSvc = new FeepaymentschoolService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetFeepaymentschool()
        {
            List<FeepaymentschoolDE> list = new List<FeepaymentschoolDE>();
            list = _feepaymentschoolSvc.SearchFeepaymentschool(new FeepaymentschoolDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchFeepaymentschool(FeepaymentschoolDE feepaymentschool)
        {
            List<FeepaymentschoolDE> list = _feepaymentschoolSvc.SearchFeepaymentschool(feepaymentschool);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetFeepaymentschoolById(int id)
        {
            List<FeepaymentschoolDE> list = new List<FeepaymentschoolDE>();
            list = _feepaymentschoolSvc.SearchFeepaymentschool(new FeepaymentschoolDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostFeepaymentschool(FeepaymentschoolDE feepaymentschool)
        {
            feepaymentschool.DBoperation = DBoperations.Insert;
            _feepaymentschoolSvc.ManageFeepaymentschool(feepaymentschool);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutFeepaymentschool(FeepaymentschoolDE feepaymentschool)
        {
            feepaymentschool.DBoperation = DBoperations.Update;
            _feepaymentschoolSvc.ManageFeepaymentschool(feepaymentschool);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeepaymentschool(int id)
        {
            FeepaymentschoolDE feepaymentschoolDe = new FeepaymentschoolDE();
            feepaymentschoolDe.DBoperation = DBoperations.Delete;
            feepaymentschoolDe.Id = id;
            _feepaymentschoolSvc.ManageFeepaymentschool(feepaymentschoolDe);
            return Ok();
        }
    }
}
