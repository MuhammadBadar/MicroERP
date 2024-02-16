using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.SearchCriteria;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Service.TMS;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QST.MicroERP.WebAPI.Controllers.TMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private UserTaskService _tskSvc;
        public UserTaskController()
        {
            _tskSvc = new UserTaskService();
        }
        // HTTP Methods 
        [HttpGet]
        public ActionResult Get()
        {
            TaskSearchCriteria sc = new TaskSearchCriteria();
            List<UserTaskDE> values = _tskSvc.SearchUserTask(sc);
            List<UserTaskDE> filteredValues = values.Where(task => !string.IsNullOrEmpty(task.UserId)).ToList();

            return Ok(filteredValues);
        }

        [HttpGet("{id}")]
        public IActionResult GetusertaskById(int id)
        {
            List<UserTaskDE> list = new List<UserTaskDE>();
            TaskSearchCriteria sc = new TaskSearchCriteria();
            sc.Id = id;
            list = _tskSvc.SearchUserTask(sc);
            return Ok(list[0]);

        }

        [HttpPost("{Search}")]
        public ActionResult Search(TaskSearchCriteria usr)
        {
            List<UserTaskDE> values = _tskSvc.SearchUserTask(usr);
            return Ok(values);
        }
        [HttpPost("IsDayStarted")]
        public ActionResult IsDayStarted(UserVM user)
        {
            bool retVal = _tskSvc.IsDayStarted(user.Id);
            return Ok(retVal);
        }
        [HttpPost("HasTodaysTasks")]
        public ActionResult HasTasks(UserVM user)
        {
            bool retVal = _tskSvc.HasTodaysTasks(user.Id);
            return Ok(retVal);
        }
        [HttpPost]
        public ActionResult Post([FromBody] List<UserTaskDE> mod, [FromQuery] bool markAttendance)
        {
            foreach (var item in mod)
                item.DBoperation = DBoperations.Insert;
            bool usr = _tskSvc.ManageUserTask(mod, markAttendance, null);
            return Ok(usr);
        }
        [HttpPut]
        public ActionResult Put([FromBody] List<UserTaskDE> tasks, [FromQuery] bool markDayEnd)
        {
            foreach (var item in tasks)
                item.DBoperation = DBoperations.Update;
            bool retVal = _tskSvc.ManageUserTask(tasks, null, markDayEnd);
            if (retVal)
                return Ok(tasks);
            else
                return BadRequest("Failed to update tasks");
        }
        [HttpPut("MarkStatus")]
        public ActionResult MarkStatus(TaskDE task)
        {
            task.DBoperation = DBoperations.Update;
            bool retVal = _tskSvc.ChangeTaskStatus(task);
            return Ok(retVal);
        }
    }
}
