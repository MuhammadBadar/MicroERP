using QST.MicroERP.Core.Entities.NTF;
using QST.MicroERP.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QST.MicroERP.WebAPI.Controllers.NTF
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTemplateController : ControllerBase
    {
        #region Class Variables

        private NTF_NotificationService _nTemSVC;

        #endregion
        #region Constructors
        public NotificationTemplateController()
        {
            _nTemSVC = new NTF_NotificationService();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            var nTemSC = new NotificationTemplateSearchCriteria();
            List<NotificationTemplateDE> nTem = _nTemSVC.SearchNotificationTemplates(nTemSC);
            return Ok(nTem);
        }
        [HttpGet("{id}")]
        public ActionResult GetNotificationTemplateById(int id)
        {
            var nTemSC = new NotificationTemplateSearchCriteria { Id = id };
            var nTem = _nTemSVC.SearchNotificationTemplates(nTemSC);
            return Ok(nTem);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(NotificationTemplateSearchCriteria Search)
        {
            List<NotificationTemplateDE> nTem = _nTemSVC.SearchNotificationTemplates(Search);
            return Ok(nTem);
        }

        [HttpPost]
        public ActionResult Post(NotificationTemplateDE nTem)
        {
            nTem.DBoperation = DBoperations.Insert;
            bool NotificationTemplate = _nTemSVC.ManagementNotificationTemplate(nTem);
            return Ok(NotificationTemplate);
        }

        [HttpPut]
        public ActionResult Put(NotificationTemplateDE nTem)
        {
            nTem.DBoperation = DBoperations.Update;
            _nTemSVC.ManagementNotificationTemplate(nTem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            NotificationTemplateDE tem = new NotificationTemplateDE { Id = id, DBoperation = DBoperations.DeActivate };
            _nTemSVC.ManagementNotificationTemplate(tem);
        }

        #endregion
    }
}
