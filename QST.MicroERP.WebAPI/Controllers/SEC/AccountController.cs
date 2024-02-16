using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Core.Entities.Security;
using QST.MicroERP.Core.SearchCriteria;
using QST.MicroERP.Models;
using QST.MicroERP.Service.IServices;
using QST.MicroERP.Service.SEC;
using QST.MicroERP.Service.TMS;
using QST.MicroERP.WebAPI.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QST.MicroERP.WebAPI.Controllers.SEC
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTTokenGenerator _jwtToken;
        private readonly SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        public UserService _userService;
        private readonly IPermissionService _permsSvc;
        public AccountController(
            IPermissionService permsSvc,
            SignInManager<User> signInManager,
            UserManager<User> usrMgr,
            IJWTTokenGenerator jwtToken,
            RoleManager<IdentityRole> roleMgr,
            IConfiguration config)
        {
            _userService = new UserService();
            _jwtToken = jwtToken;
            _permsSvc = permsSvc;
            _roleManager = roleMgr;
            _signInManager = signInManager;
            _userManager = usrMgr;
            _config = config;
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    var reslt = await _userManager.CheckPasswordAsync(user, model.Password);
                    var results = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, true);
                    if (!result.Succeeded)
                        return Ok(result);
                    else
                    {
                        if (model.Name != null)
                            if (user.Name != model.Name)
                                return BadRequest("Invalid client request");
                    }
                    TaskSearchCriteria sc = new TaskSearchCriteria();
                    sc.UserId = user.Id;
                    sc.Date = DateTime.Now;
                    var existingTask = new UserTaskService().SearchUserTask(sc);
                    bool showDayStartDialog = false;
                    bool showDayEndDialog = false;
                    if (existingTask.Count == 0)
                        showDayStartDialog = true;
                    if (showDayStartDialog)
                    {
                        sc = new TaskSearchCriteria();
                        sc.IsDayEnded = false;
                        sc.UserId = user.Id;
                        var pendingTask = new UserTaskService().SearchUserTask(sc);
                        if (pendingTask.Count > 0)
                        {
                            showDayEndDialog = true;
                            showDayStartDialog = false;
                        }
                    }
                    var _user = new UserDE();
                    _user.Id = user.Id;
                    List<UserDE> users = _userService.SearchUsers(_user);
                    if (users != null && users.Count > 0)
                        _user = users[0];
                    var _permissions = new List<PermissionDE> ();
                    _permissions = _permsSvc.GetPermsByUserOrRole (user.Id, (int)_user.RoleId);
                    return Ok(new
                    {
                        result = results,
                        id = user.Id,
                        userName = user.UserName,
                        email = user.Email,
                        role = _user.Role,
                        roleId = _user.RoleId,
                        supervisorId = _user.SupervisorId,
                        showDayStartDialog,
                        showDayEndDialog,
                        clientId = user.ClientId,
                        client = _user.Client,
                        moduleIds = _user.ModuleIds,
                        cLTId = _user.CLTId,
                        cLTModuleIds = _user.CLTModuleIds,
                        doctorId = _user.DoctorId,
                        token = _jwtToken.GenerateToken (user, new List<string> { _user.Role }),
                        permissions = _permissions
                    }); ;
                }
                else
                    return BadRequest("Invalid client request");
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

    }
}
