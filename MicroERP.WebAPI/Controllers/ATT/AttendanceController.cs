using MicroERP.Core.Entities.ATT;
using MicroERP.Core.Enums;
using MicroERP.Core.ViewModel;
using MicroERP.Service.ATT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers.ATT
{

    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        #region Class Variables
        private AttendanceService attSVC;
        #endregion
        #region Constructor
        public AttendanceController()
        {
            attSVC = new AttendanceService();
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            AttendanceDE Att = new AttendanceDE();
            List<AttendanceDE> values = attSVC.SearchAttendance(Att);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public ActionResult GetAttendanceById(int id)
        {
            AttendanceDE att = new AttendanceDE { Id = id };
            var values = attSVC.SearchAttendance(att);
            return Ok(values);
        }
        [HttpPost("{Search}")]
        public IActionResult SearchAttendance(AttendanceDE attendance)

        {
            var schTime = attSVC.GetScheduleTime(attendance.UserId, attendance.Date.Value);
            List<AttendanceDE> list = attSVC.SearchAttendance(attendance);
            return Ok(list);
        }
        [HttpPost("getAttendanceRpt")]
        public IActionResult AttendanceRpt(AttendanceDE Attendance)
        {
            List<AttendanceDE> list = attSVC.GetAttendanceReport(Attendance);
            return Ok(list);
        }
        [HttpPost]
        public IActionResult PostAttendance(AttendanceDE Attendance)
        {
            Attendance.DBoperation = DBoperations.Insert;
            bool std = attSVC.ManageAttendance(Attendance);
            return Ok(std);
        }
        [HttpGet("GetLastAttendance/{userId}")]
        public ActionResult GetLastAttendance(string userId)
        {
            try
            {
                AttendanceDE lastAttendance = attSVC.GetLastAttendanceByInTime(userId);

                if (lastAttendance != null)
                {
                    return Ok(lastAttendance);
                }
                else
                {
                    // Handle the case where no attendance record is found
                    return NotFound($"No attendance record found for user with ID: {userId}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                // Adjust this based on your error handling strategy
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPut]
        public ActionResult Put(AttendanceDE Attendance)
        {
            Attendance.DBoperation = DBoperations.Update;
            attSVC.ManageAttendance(Attendance);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            AttendanceDE Attendance = new AttendanceDE { Id = id, DBoperation = DBoperations.Delete };
            attSVC.ManageAttendance(Attendance);
        }
        #endregion
    }
}