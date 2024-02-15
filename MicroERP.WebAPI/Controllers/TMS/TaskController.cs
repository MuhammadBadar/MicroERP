using MicroERP.Core.Entities.TMS;
using MicroERP.Core.Enums;
using MicroERP.Core.SearchCriteria;
using MicroERP.Core.ViewModel;
using MicroERP.Service.TMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroERP.WebAPI.Controllers.TMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        #region Class Variables

        private TaskService _taskSVC;

        #endregion
        #region Constructors
        public TaskController()
        {
            _taskSVC = new TaskService();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {

            TaskSearchCriteria TaskSC = new TaskSearchCriteria { IsActive = true };
            List<UserTaskVM> task = _taskSVC.SearchUserTasks(TaskSC);
            return Ok(task);
        }
        [HttpGet("{id}")]
        public ActionResult GetTaskById(int id)
        {

            TaskSearchCriteria tasktSC = new TaskSearchCriteria { Id = id };
            var retVal = _taskSVC.SearchUserTasks(tasktSC);
            retVal = retVal.OrderBy(x => x.PriorityId).ToList();
            return Ok(retVal);
        }

        [HttpGet("GetTasksByUserId/{userId}")]
        public ActionResult GetTasksByUserId(string userId)
        {
            TaskSearchCriteria taskSearchCriteria = new TaskSearchCriteria { UserId = userId, IsActive = true };
            List<UserTaskVM> retVal = _taskSVC.SearchUserTasks(taskSearchCriteria);
            retVal = retVal.Where(x => x.ApprovedClaimId > 0 ? x.ApprovedClaimId != (int)ClaimPer.Claim_100Per : x.ClaimId != (int)ClaimPer.Claim_100Per).ToList();
            retVal = retVal.OrderBy(x => x.PriorityId).ToList();
            return Ok(retVal);
        }

        [HttpPost("{Search}")]
        public ActionResult Search(TaskSearchCriteria Search)
        {
            List<UserTaskVM> retVal = _taskSVC.SearchUserTasks(Search);
            retVal = retVal.OrderBy(x => x.PriorityId).ToList();
            return Ok(retVal);
        }
        [HttpPost("Tasks")]
        public ActionResult SearchUserTasks(TaskSearchCriteria Search)
        {
            List<TaskDE> retVal = _taskSVC.SearchTasks(Search);
            return Ok(retVal);
        }

        [HttpPost]
        public ActionResult Post(TaskDE task)
        {
            task.DBoperation = DBoperations.Insert;
            foreach (var item in task.Attachments)
            {
                item.DBoperation = DBoperations.Insert;
            }
            var retVal = _taskSVC.ManagementTask(task);
            return Ok(retVal);
        }

        [HttpPut]
        public void Put(TaskDE task)
        {

            task.DBoperation = DBoperations.Update;
            _taskSVC.ManagementTask(task);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TaskDE task = new TaskDE { Id = id, DBoperation = DBoperations.DeActivate };
            _taskSVC.ManagementTask(task);
        }

        #endregion
    }
}
