using QST.MicroERP.Core.Entities.ATT;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Service.ATT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Service.SCH;

namespace QST.MicroERP.WebAPI.Controllers.ATT
{

    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        #region Class Variables
        private AttendanceService attSvc;
        private ScheduleService schSvc;
        #endregion
        #region Constructor
        public AttendanceController()
        {
            attSvc = new AttendanceService();
            schSvc = new ScheduleService(); 
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            AttendanceDE Att = new AttendanceDE();
            List<AttendanceDE> values = attSvc.SearchAttendance(Att);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public ActionResult GetAttendanceById(int id)
        {
            AttendanceDE att = new AttendanceDE { Id = id };
            var values = attSvc.SearchAttendance(att);
            return Ok(values);
        }
        [HttpPost("{Search}")]
        public IActionResult SearchAttendance(AttendanceDE attendance)
        {
            var schTime = schSvc.GetScheduleTime(attendance.UserId,attendance.ClientId, attendance.Date.Value);
            List<AttendanceDE> list = attSvc.SearchAttendance(attendance);
            return Ok(list);
        }
        [HttpPost("getAttendanceRpt")]
        public IActionResult AttendanceRpt(AttendanceDE Attendance)
        {
            List<AttendanceDE> list = attSvc.GetAttendanceReport(Attendance);
            return Ok(list);
        }
        [HttpPost]
        public IActionResult PostAttendance(AttendanceDE Attendance)
        {
            Attendance.DBoperation = DBoperations.Insert;
            var att = attSvc.ManageAttendance(Attendance);
            return Ok(att);
        }
        [HttpPut]
        public ActionResult Put(AttendanceDE Attendance)
        {
            Attendance.DBoperation = DBoperations.Update;
            attSvc.ManageAttendance(Attendance);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            AttendanceDE Attendance = new AttendanceDE { Id = id, DBoperation = DBoperations.Delete };
            attSvc.ManageAttendance(Attendance);
        }
        #endregion
    }
}