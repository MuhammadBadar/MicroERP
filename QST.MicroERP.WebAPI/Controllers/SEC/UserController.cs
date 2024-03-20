using QST.MicroERP.Core.Entities.SCH;
using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Models;
using QST.MicroERP.Service.SCH;
using QST.MicroERP.Service.SEC;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers.SEC
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected IConfiguration _configuration;
        public UserService _userSvc;
        private ScheduleService _schSvc;
        private UserManager<User> userManager;
        private IPasswordHasher<User> passwordHasher;
        public UserController(SignInManager<User> signInManager, UserManager<User> usrMgr, IPasswordHasher<User> passwordHash, IPasswordValidator<User> passwordVal, IUserValidator<User> userValid, IConfiguration configuration)
        {
            _schSvc = new ScheduleService();
            _userSvc = new UserService();
            userManager = usrMgr;
            passwordHasher = passwordHash;
            _configuration = configuration;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult> GetUserById ( String id )
        {
            User u = await userManager.FindByIdAsync (id);
            if (u != null)
                return Ok (u);
            else
                return BadRequest ("Invalid client request");
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            User data = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.UserPassword,
                UserPassword = user.UserPassword,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                FatherName = user.FatherName,
                FirstName = user.FirstName,
                Designation = user.Designation,
                CNIC = user.CNIC,
                RoleId = user.RoleId,
                ModuleId = user.ModuleId,
                ClientId = user.ClientId,
                Name=user.Name,
                SupervisorId = user.SupervisorId,

            };
            IdentityResult result = await userManager.CreateAsync(data, data.PasswordHash);
            if (result != null && result.Succeeded)
            {
                var clientAdmins = _userSvc.SearchUsers(new UserDE { RoleId = (int)Roles.ClientAdmin });
                if (clientAdmins != null && clientAdmins.Count > 0)
                {
                    var clientAdmin = clientAdmins.Last();
                    var Schedules = _schSvc.SearchSchedule(new ScheduleDE { UserId = clientAdmin.Id, IsActive = true });
                    if (Schedules != null && Schedules.Count > 0)
                    {
                        ScheduleDE userSchedule = Schedules.Last();
                        userSchedule.DBoperation = DBoperations.Insert;
                        userSchedule.UserId = data.Id;
                        var retVal = _schSvc.CopySchedule(userSchedule);
                    }
                }
            }
            return Ok (new
            {
                result,
                UserId = data.Id,
            });
        }


        [HttpPut]
        public async Task<ActionResult> Update ( User _user )
        {
            User user = await userManager.FindByIdAsync (_user.Id);
            user.UserName = _user.UserName;
            user.Email = _user.Email;
            user.RoleId = _user.RoleId;
            user.SupervisorId = _user.SupervisorId;
            user.UserPassword = _user.UserPassword;
            user.PhoneNumber = user.PhoneNumber;
            user.ClientId = user.ClientId;
            user.RoleId = user.RoleId;
            user.ModuleId = user.ModuleId;
            user.Name = user.Name;
            user.Address = user.Address;
            user.FatherName = user.FatherName;
            user.FirstName = user.FirstName;
            user.Designation = user.Designation;
            user.CNIC = user.CNIC;
            user.PasswordHash = passwordHasher.HashPassword (user, _user.UserPassword);
            IdentityResult result = await userManager.UpdateAsync (user);
            return Ok (new
            {
                result,
                UserId = user.Id,
            });
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
            }
            return Ok();
        }


        [HttpPost("{name}")]
        public async Task<ActionResult> GetUserbyName(User users)
        {
            User user = await userManager.FindByNameAsync(users.UserName);
            return Ok(user);
        }


        [HttpGet]
        public ActionResult GetUsers()
        {
            var users = userManager.Users;
            return Ok(users);
        }


        [HttpPost("Search")]
        public ActionResult Search(UserDE Search)
        {
            List<UserDE> users = _userSvc.SearchUsers(Search);
            return Ok(users);
        }


        [HttpPost("GetSubordinates")]
        public ActionResult GetSubordinates(UserDE Search)
        {
            List<UserDE> users = _userSvc.GetSubordinates(Search);
            return Ok(users);
        }

        [HttpPost("GetSupervisors")]
        public ActionResult GetSupervisor(UserDE Search)
        {
            List<UserDE> users = _userSvc.GetSupervisor(Search);
            return Ok(users);
        }
        [HttpPost ("GetUserWithSubordinates")]
        public ActionResult GetUserWithSubordinates ( UserDE Search )
        {
            List<UserDE> users = _userSvc.GetUserWithSubordinates (Search);
            return Ok (users);
        }
    }
}

