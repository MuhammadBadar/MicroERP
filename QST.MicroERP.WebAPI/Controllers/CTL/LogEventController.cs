﻿using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service.CLT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers.CTL
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogEventController : ControllerBase
    {
        #region Class Variables
        private LogEventService _LogEventSVC;
        #endregion
        #region Constructor
        public LogEventController()
        {
            _LogEventSVC = new LogEventService();
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            LogEventDE LogEvent = new LogEventDE();
            List<LogEventDE> values = _LogEventSVC.SearchLogEvents(LogEvent);
            return Ok(values);
        }
        [HttpGet("{id}")]
        public ActionResult GetLogEventById(int id)
        {
            LogEventDE LogEvent = new LogEventDE { Id = id };
            var values = _LogEventSVC.SearchLogEvents(LogEvent);
            return Ok(values);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(LogEventDE Search)
        {
            //Search.IsActive = true;
            List<LogEventDE> values = _LogEventSVC.SearchLogEvents(Search);
            return Ok(values);
        }
        [HttpPost("MarkInTime")]
        public ActionResult MarkInTime(LogEventDE logEvent)
        {
            _LogEventSVC.MarkInTime(logEvent.UserId, logEvent.ClientId);
            return Ok();
        }
        [HttpPost("MarkOutTime")]
        public ActionResult MarkOutTime( LogEventDE logEvent )
        {
            bool LogEvent = _LogEventSVC.MarkOutTime(logEvent.UserId, logEvent.ClientId);
            return Ok(LogEvent);
        }
        [HttpPost]
        public ActionResult Post(LogEventDE mod)
        {
            mod.DBoperation = DBoperations.Insert;
            bool LogEvent = _LogEventSVC.ManagementLogEvent(mod);
            return Ok(LogEvent);
        }
        [HttpPut]
        public ActionResult Put(LogEventDE mod)
        {
            mod.DBoperation = DBoperations.Update;
            _LogEventSVC.ManagementLogEvent(mod);
            return Ok();
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            LogEventDE LogEvent = new LogEventDE { Id = id, DBoperation = DBoperations.Delete };
            _LogEventSVC.ManagementLogEvent(LogEvent);
        }

        #endregion
    }
}
