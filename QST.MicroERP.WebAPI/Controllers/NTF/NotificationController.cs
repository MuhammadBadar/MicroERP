using QST.MicroERP.Core.Entities.NTF;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using QST.MicroERP.Service;
//using WhatsAppApi;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QST.MicroERP.WebAPI.Controllers.NTF
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        #region Class Variables

        private NTF_NotificationService _notificSVC;

        #endregion
        #region Constructors
        public NotificationController()
        {
            _notificSVC = new NTF_NotificationService();
        }

        #endregion
        #region Http Verbs

        [HttpPost]
        public ActionResult Post(NotificationDE ntfcn)
        {
            _notificSVC.SendEmail(ntfcn);
            return Ok();
        }
        [HttpPost("Mail")]
        public ActionResult SendMail([FromForm] NotificationDE mail)
        {
            _notificSVC.SendEmailWithAttachment(mail.Attachment, mail);
            return Ok();
        }
        #endregion
    }
}
