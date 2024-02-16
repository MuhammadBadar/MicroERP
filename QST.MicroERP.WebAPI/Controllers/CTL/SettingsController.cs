using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service.CLT;
using QST.MicroERP.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers.CTL
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        #region Class Variables
        private SettingsService _settingsSVC;
        #endregion
        #region Constructor
        public SettingsController ( IPermissionService permSvc )
        {
            _settingsSVC = new SettingsService (permSvc);
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get ( )
        {
            SettingsDE Settings = new SettingsDE ();
            List<SettingsDE> list = _settingsSVC.SearchSettingss (Settings);
            return Ok (list);
        }
        [HttpGet ("{id}")]
        public ActionResult GetSettingsById ( int id )
        {
            SettingsDE Settings = new SettingsDE { Id = id };
            var list = _settingsSVC.SearchSettingss (Settings);
            return Ok (list);
        }
        [HttpPost ("{Search}")]
        public ActionResult Search ( SettingsDE Search )
        {
            // Search.IsActive = true;
            List<SettingsDE> list = _settingsSVC.SearchSettingss (Search);
            return Ok (list);
        }
        [HttpPost ("EnumLine")]
        public ActionResult SearchEnumLine ( SettingsDE Search )
        {
            // Search.IsActive = true;
            List<SettingsDE> list = _settingsSVC.SearchEnumLine (Search);
            return Ok (list);
        }
        [HttpPost ("Accounts")]
        public ActionResult SearchAccounts ( SettingsDE Search )
        {
            // Search.IsActive = true;
            List<SettingsDE> list = _settingsSVC.SearchAccounts (Search);
            return Ok (list);
        }
        [HttpPost ("Menu")]
        public ActionResult GetMenu ( SettingsDE Search )
        {
            List<SettingsDE> list = _settingsSVC.SearchMenu ();
            return Ok (list);
        }
        [HttpPost ("ActiveMenu")]
        public ActionResult GetActiveMenu ( UserVM user )
        {
            List<SettingsDE> list = _settingsSVC.SearchMenuByUser (user);
            return Ok (list);
        }
        [HttpGet ("{Id}/{KeyCode}")]
        public ActionResult SearchByKeyCode ( int Id, string KeyCode )
        {
            SettingsDE Search = new SettingsDE { EnumTypeId = Id, IsActive = true, TypeKeyCode = KeyCode };
            List<SettingsDE> list = _settingsSVC.SearchSettingss (Search);
            return Ok (list);
        }
        [HttpPost]
        public ActionResult Post ( SettingsDE stng )
        {
            stng.DBoperation = DBoperations.Insert;
            bool Settings = _settingsSVC.ManagementSettings (stng);
            return Ok (Settings);
        }
        [HttpPut]
        public ActionResult Put ( SettingsDE stng )
        {
            stng.DBoperation = DBoperations.Update;
            _settingsSVC.ManagementSettings (stng);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public void Delete ( int id )
        {
            SettingsDE Settings = new SettingsDE { Id = id, DBoperation = DBoperations.Delete };
            _settingsSVC.ManagementSettings (Settings);
        }

        #endregion
    }
}
