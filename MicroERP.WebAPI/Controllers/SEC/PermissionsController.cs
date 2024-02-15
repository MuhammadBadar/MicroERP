using MicroERP.Core.Entities.Security;
using MicroERP.Core.SearchCriteria;
using MicroERP.Service;
using MicroERP.Service.IServices;
using MicroERP.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "ManagerOnly")]
    public class PermissionsController : ControllerBase
    {

        #region Class Variables

        private IPermissionService _permSvc;
        private IBaseService<PermissionDE> _baseSvc;

        #endregion
        #region Constructors
        public PermissionsController( IBaseService<PermissionDE> baseSvc , IPermissionService permSvc )
        {
            _baseSvc = baseSvc;
            _permSvc = permSvc;
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            PermissionDE permsSC = new PermissionDE();
            List<PermissionDE> perms = _baseSvc.SearchData(permsSC);
            return Ok(perms);
        }

        [HttpPost("{Search}")]
        public ActionResult Search(PermissionDE Search)
        {
            List<PermissionDE> perms = _baseSvc.SearchData(Search);
            return Ok(perms);
        }
        [HttpPost ("Permission")]
        public ActionResult SearchPermissions ( PermissionDE Search )
        {
            List<PermissionDE> perms = _permSvc.SearchPermissions (Search);
            return Ok (perms);
        }
        [HttpPost ("GetPerms")]
        public ActionResult GetPermsByUserOrRole ( string UserId, int RoleId )
        {
            List<PermissionDE> perms = _permSvc.GetPermsByUserOrRole (UserId, RoleId);
            return Ok (perms);
        }

        [HttpPost]
        public ActionResult Post(List<PermissionDE> perms)
        {
            bool per = _permSvc.SavePermissions(perms);
            return Ok(per);
        }

        [HttpPut]
        public ActionResult Put(PermissionDE perms)
        {
            bool per = _baseSvc.ManageData(perms);
            return Ok(per);
        }
        #endregion

    }
}
