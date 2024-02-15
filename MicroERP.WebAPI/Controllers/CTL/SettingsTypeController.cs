using MicroERP.Core.Entities.CTL;
using MicroERP.Core.Enums;
using MicroERP.Service.CLT;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroERP.WebAPI.Controllers.CTL
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsTypeController : ControllerBase
    {
        #region Class Variables
        private SettingsTypeService _settingsTypeSVC;
        #endregion
        #region Constructor
        public SettingsTypeController ( )
        {
            _settingsTypeSVC = new SettingsTypeService ();
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get ( )
        {
            SettingsTypeDE SettingsType = new SettingsTypeDE ();
            List<SettingsTypeDE> retVal = _settingsTypeSVC.SearchSettingsTypes (SettingsType);
            return Ok (retVal);
        }
        [HttpGet ("{id}")]
        public ActionResult GetSettingsTypeById ( int id )
        {
            SettingsTypeDE SettingsType = new SettingsTypeDE { Id = id };
            var retVal = _settingsTypeSVC.SearchSettingsTypes (SettingsType);
            return Ok (retVal);
        }
        [HttpPost ("{Search}")]
        public ActionResult Search ( SettingsTypeDE Search )
        {
            //Search.IsActive = true;
            List<SettingsTypeDE> retVal = _settingsTypeSVC.SearchSettingsTypes (Search);
            return Ok (retVal);
        }
        [HttpPost ("Enum")]
        public ActionResult SearchEnum ( SettingsTypeDE Search )
        {
            //Search.IsActive = true;
            List<SettingsTypeDE> retVal = _settingsTypeSVC.SearchEnums (Search);
            return Ok (retVal);
        }
        [HttpPost]
        public ActionResult Post ( SettingsTypeDE stngType )
        {
            stngType.DBoperation = DBoperations.Insert;
            SettingsTypeDE SettingsType = _settingsTypeSVC.ManagementSettingsType (stngType);
            return Ok (SettingsType);
        }
        [HttpPut]
        public ActionResult Put ( SettingsTypeDE stngType )
        {
            stngType.DBoperation = DBoperations.Update;
            SettingsTypeDE SettingsType = _settingsTypeSVC.ManagementSettingsType (stngType);
            return Ok (SettingsType);
        }
        [HttpDelete ("{id}")]
        public void Delete ( int id )
        {
            SettingsTypeDE SettingsType = new SettingsTypeDE { Id = id, DBoperation = DBoperations.DeActivate };
            _settingsTypeSVC.ManagementSettingsType (SettingsType);
        }

        #endregion
    }
}
