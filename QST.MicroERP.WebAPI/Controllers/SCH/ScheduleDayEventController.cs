using QST.MicroERP.Core.Entities.SCH;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.Models;
using QST.MicroERP.Service.SCH;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace QST.MicroERP.WebAPI.Controllers.SCH
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleDayEventController : Controller
    {
        #region Class Variables
        private ScheduleDayEventService _schSVC;
        #endregion
        #region Constructors
        public ScheduleDayEventController()
        {
            _schSVC = new ScheduleDayEventService();
        }
        #endregion
        #region Http Verbs
        [HttpPost("{Search}")]
        public IActionResult SearchScheduleDayEvent(ScheduleDayEventSearchCriteria schedule)
        {
            List<ScheduleDayEventDE> list = _schSVC.SearchScheduleDayEvent(schedule);

            foreach (var val in list)
            {
                DateTime startTime = DateTime.ParseExact(val.StartTime, "HH:mm", CultureInfo.InvariantCulture);
                DateTime endTime = DateTime.ParseExact(val.EndTime, "HH:mm", CultureInfo.InvariantCulture);
                TimeSpan timeDifference = endTime - startTime;
                val.Sp = Math.Round(timeDifference.TotalHours, 2);
            }
            return Ok(list);
        }
        [HttpGet("{id}")]
        public ActionResult GetScheduleDaysEventById(int id)
        {
            ScheduleDayEventSearchCriteria Schedule = new ScheduleDayEventSearchCriteria { Id = id };
            var values = _schSVC.SearchScheduleDayEvent(Schedule);
            return Ok(values);
        }
        [HttpGet("GetScheduleDayEvents/{id}")]
        public IActionResult GetScheduleDayEvents(int id)
        {
            ScheduleDayEventSearchCriteria schSC = new ScheduleDayEventSearchCriteria();
            schSC.SchDayId = id;
            List<ScheduleDayEventDE> schDayEvents = _schSVC.SearchScheduleDayEvent(schSC);
            return Ok(schDayEvents);
        }
        [HttpPost]
        public IActionResult PostScheduleDayEvents(ScheduleDayEventDE Schedule)
        {
            Schedule.DBoperation = DBoperations.Insert;
            ScheduleDayEventDE sch = _schSVC.ManageScheduleDayEvent(Schedule);
            return Ok(sch);
        }
        [HttpPut]
        public IActionResult PutScheduleDayEvents(ScheduleDayEventDE Schedule)
        {
            Schedule.DBoperation = DBoperations.Update;
            ScheduleDayEventDE sch = _schSVC.ManageScheduleDayEvent(Schedule);
            return Ok(sch);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteScheduleDayEvents(int id)
        {
            ScheduleDayEventDE Schedule = new ScheduleDayEventDE();
            Schedule.DBoperation = DBoperations.Delete;
            Schedule.Id = id;
            ScheduleDayEventDE sch = _schSVC.ManageScheduleDayEvent(Schedule);
            return Ok(sch);
        }
    }
    #endregion
}
