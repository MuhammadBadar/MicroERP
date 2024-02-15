using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;
using MicroERP.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class SMTPCredentialsController : ControllerBase
    {
        #region Data Members
        private readonly IBaseService<SMTPCredentialsDE> _baseSvc;
        #endregion
        #region Constructor        
        public SMTPCredentialsController ( IBaseService<SMTPCredentialsDE> baseSvc)
        {
            _baseSvc = baseSvc;
        }
        #endregion
        #region HTTP Verbs
        [HttpGet]
        public IActionResult GetSMTPCredentials ( )
        {
            List<SMTPCredentialsDE> list = new List<SMTPCredentialsDE> ();
            list = _baseSvc.SearchData (new SMTPCredentialsDE ());
            return Ok (list);
        }

        [HttpPost ("{Search}")]
        public IActionResult SearchSMTPCredentials ( SMTPCredentialsDE SMTPCredentials )
        {
            List<SMTPCredentialsDE> list = _baseSvc.SearchData (SMTPCredentials);
            return Ok (list);
        }
        [HttpGet ("{id}")]
        public IActionResult GetSMTPCredentialsById ( int id )
        {
            List<SMTPCredentialsDE> list = new List<SMTPCredentialsDE> ();
            list = _baseSvc.SearchData (new SMTPCredentialsDE { Id = id });
            return Ok (list[0]);
        }
        [HttpPost]
        public IActionResult PostSMTPCredentials ( SMTPCredentialsDE SMTPCredentials )
        {
            SMTPCredentials.DBoperation = DBoperations.Insert;
            _baseSvc.ManageData (SMTPCredentials);
            return Ok ();
        }
        [HttpPut]
        public IActionResult PutSMTPCredentials ( SMTPCredentialsDE SMTPCredentials )
        {
            SMTPCredentials.DBoperation = DBoperations.Update;
            _baseSvc.ManageData (SMTPCredentials);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public IActionResult DeleteSMTPCredentials ( int id )
        {
            SMTPCredentialsDE SMTPCredentialsDe = new SMTPCredentialsDE ();
            SMTPCredentialsDe.DBoperation = DBoperations.Delete;
            SMTPCredentialsDe.Id = id;
            _baseSvc.ManageData (SMTPCredentialsDe);
            return Ok ();
        }
        #endregion
    }
}
