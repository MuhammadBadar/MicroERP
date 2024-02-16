using QST.MicroERP.Core.Entities.SCH;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service.ATT;
using QST.MicroERP.Service.SCH;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers.SCH
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        #region Class Variables

        private ScheduleService _schSVC;
        private AttendanceService _attSvc;

        #endregion
        #region Constructors
        public ScheduleController()
        {
            _schSVC = new ScheduleService();
            _attSvc = new AttendanceService();
        }
        #endregion
        #region Http Verbs

        [HttpPost("{Search}")]
        public IActionResult SearchSchedule(ScheduleDE schedule)
        {
            List<ScheduleDE> list = _schSVC.SearchSchedule(schedule);
            return Ok(list);
        }
        [HttpGet("GetScheduleByUserId")]
        public ActionResult GetScheduleByUserId(string userId)
        {
            var schedule = _schSVC.GetScheduleByUserId(userId);
            return Ok(schedule);
        }
        [HttpGet("GetDueSps")]
        public ActionResult GetDueSps(string userId)
        {
            var sps = _attSvc.GetDueSPs(userId, DateTime.Now);
            return Ok(sps);
        }
        [HttpGet]
        public IActionResult GetSchedule()
        {
            ScheduleDE schSC = new ScheduleDE();
            List<ScheduleDE> schedule = _schSVC.SearchSchedule(schSC);
            return Ok();
        }
        [HttpPost]
        public IActionResult PostSchedule(ScheduleDE Schedule)
        {
            Schedule.DBoperation = DBoperations.Insert;
            ScheduleDE sch = _schSVC.ManageSchedule(Schedule);
            return Ok(sch);
        }
        [HttpPut]
        public IActionResult PutSchedule(ScheduleDE Schedule)
        {
            Schedule.DBoperation = DBoperations.Update;
            ScheduleDE sch = _schSVC.ManageSchedule(Schedule);
            return Ok(sch);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSchedule(int id)
        {
            ScheduleDE Schedule = new ScheduleDE();
            Schedule.DBoperation = DBoperations.Delete;
            Schedule.Id = id;
            ScheduleDE sch = _schSVC.ManageSchedule(Schedule);
            return Ok(sch);
        }
        [HttpDelete("DeleteScheduleDay")]
        public IActionResult DeleteScheduleDay(int id)
        {
            ScheduleDayDE schDay = new ScheduleDayDE();
            schDay.DBoperation = DBoperations.Delete;
            schDay.Id = id;
            _schSVC.ManageScheduleDay(schDay);
            return Ok();
        }
    }
    #endregion
}

