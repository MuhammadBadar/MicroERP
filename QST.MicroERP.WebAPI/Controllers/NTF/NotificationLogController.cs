using QST.MicroERP.Core.Entities.NTF;
using QST.MicroERP.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QST.MicroERP.WebAPI.Controllers.NTF
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationLogController : ControllerBase
    {
        #region Class Variables

        private NTF_NotificationService _nLogSVC;

        #endregion
        #region Constructors
        public NotificationLogController()
        {
            _nLogSVC = new NTF_NotificationService();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult GetNextNotificationLog()
        {
            var ntfSc = new NotificationLogSearchCriteria { IsSent = false };
            List<NotificationLogDE> nLog = _nLogSVC.SearchNotificationLogs(ntfSc);
            var minId = nLog.Min(x => x.Id);
            var notification = nLog.First(x => x.Id == minId);
            //notification = nLog.First();
            return Ok(notification);
        }
        [HttpGet("{id}")]
        public ActionResult GetNotificationLogById(int id)
        {
            var nLogSC = new NotificationLogSearchCriteria { Id = id };
            var nLog = _nLogSVC.SearchNotificationLogs(nLogSC);
            return Ok(nLog);
        }
        [HttpPost("{Search}")]
        public ActionResult SearchNotificationLog(NotificationLogSearchCriteria Search)
        {
            List<NotificationLogDE> nLog = _nLogSVC.SearchNotificationLogs(Search);
            return Ok(nLog);
        }

        [HttpPost]
        public ActionResult PostNotificationLog(NotificationLogDE nLog)
        {
            nLog.DBoperation = DBoperations.Insert;
            bool NotificationLog = _nLogSVC.ManagementNotificationLog(nLog);
            return Ok(NotificationLog);
        }

        [HttpPut]
        public ActionResult UpdateNotificationStatus(NotificationLogDE nLog)

        {
            nLog.IsSent = true;
            nLog.DBoperation = DBoperations.Update;
            _nLogSVC.ManagementNotificationLog(nLog);
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(NotificationLogDE nLog)
        {
            nLog.DBoperation = DBoperations.Delete;
            _nLogSVC.ManagementNotificationLog(nLog);
            return Ok();
        }

        #endregion
    }
}
