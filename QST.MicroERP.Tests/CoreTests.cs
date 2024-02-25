using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Entities.LMS;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service.CLT;
using QST.MicroERP.Service.LMS;
using NUnit.Framework;
using System.Collections.Generic;
using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.Service.TMS;

namespace QST.MicroERP.Tests
{
    public class Tests
    {
        UserTaskService _usrTskSvc;
        [SetUp]
        public void Setup()
        {
            _usrTskSvc = new UserTaskService();
        }

        [Test]
        public void Service_UserTask_ManageUserTask()
        {
            List<UserTaskDE> modList = new List<UserTaskDE>();
            UserTaskDE mod = new UserTaskDE();
            mod.ClientId = 1;
            mod.UserId = "70d2b185-cc89-48a7-9194-b1ddd458fa23";
            mod.TaskId = 1101;
            mod.Title = "Task-1101: This is first Task";
            mod.Sp = 3;
            //mod.ClientId = 1;
            mod.Comments = "Task 1 Comments";
            modList.Add(mod);

            mod = new UserTaskDE();
            mod.ClientId = 1;
            mod.UserId = "70d2b185-cc89-48a7-9194-b1ddd458fa23";
            mod.TaskId = 1102;
            mod.Title = "Task-1102: This is second Task";
            mod.Sp = 5;
            //mod.ClientId = 1;
            mod.Comments = "Task 2 Comments ";
            modList.Add(mod);

            var retVal = _usrTskSvc.ManageUserTask(modList, true, false);

            //_userTaskService
        }

        [Test]
        public void Service_Topic_Crud()
        {
          TopicService _topicSvc =new TopicService();
          TopicDE topic = new TopicDE();
          List<TopicDE> topics = new List<TopicDE>();
            #region Delete
            topics = _topicSvc.SearchTopic(new TopicDE());
            foreach (var val in topics)
            {
                val.DBoperation = DBoperations.Delete;
                _topicSvc.ManageTopic(val);
            }
            topics = _topicSvc.SearchTopic(new TopicDE());
            Assert.AreEqual(0, topics.Count);
            #endregion
            #region Insert
            topic = new TopicDE
            {
                TopicTitle = "Oop",
                Description = "DES....",
                DBoperation = DBoperations.Insert,
                IsActive = true
            };
            Assert.IsTrue(_topicSvc.ManageTopic(topic));
            topics = _topicSvc.SearchTopic(new TopicDE());
            Assert.AreEqual(1, topics.Count);

            topic = new TopicDE
            {
                TopicTitle = "Java",
                Description = "DES....",
                DBoperation = DBoperations.Insert,
                IsActive = true
            };
            Assert.IsTrue(_topicSvc.ManageTopic(topic));
            topics = _topicSvc.SearchTopic(new TopicDE());
            Assert.AreEqual(2, topics.Count);
            #endregion
            #region Update

            topics = _topicSvc.SearchTopic(new TopicDE());
            topics[0].TopicTitle = "Android";
            topics[0].DBoperation = DBoperations.Update;
            topics[0].Id = 1;
            _topicSvc.ManageTopic(topics[0]);
            topics = _topicSvc.SearchTopic(new TopicDE());
            Assert.AreEqual("Android", topics[0].TopicTitle);

            #endregion
            #region DeActivate

            topics = _topicSvc.SearchTopic(new TopicDE());
            topics[0].IsActive = false;
            topics[0].DBoperation = DBoperations.Update;
            topics[0].Id = 1;
            _topicSvc.ManageTopic(topics[0]);
            topics = _topicSvc.SearchTopic(new TopicDE());
            Assert.AreEqual(false, topics[0].IsActive);

            #endregion
            #region Activate

            topics = _topicSvc.SearchTopic(new TopicDE());
            topics[0].IsActive = true;
            topics[0].DBoperation = DBoperations.Update;
            topics[0].Id = 1;
            _topicSvc.ManageTopic(topics[0]);
            topics = _topicSvc.SearchTopic(new TopicDE());
            Assert.AreEqual(true, topics[0].IsActive);

            #endregion
            #region Search

            topic = new TopicDE { Id = 1 };
            topics = _topicSvc.SearchTopic(topic);
            Assert.AreEqual("Android", topics[0].TopicTitle);
            Assert.AreEqual(true, topics[0].IsActive);

            topic = new TopicDE {TopicTitle= "Android" };
            topics = _topicSvc.SearchTopic(topic);
            Assert.AreEqual(1, topics[0].Id);
            Assert.AreEqual(true, topics[0].IsActive);

            #endregion
        }

        [Test]
        public void Service_City_CRUD()
        {
            CityService ctySvc = new CityService();

            //var list = new List<CourseDE>();
            //list = crsSvc.SearchCourse();

            var cty = new Core.Entities.CTL.CityDE();
            cty.Id = 1;
            cty.Name = "Lahoreeeee";


            //crs.DBoperation = DBoperations.Update;
            cty.DBoperation = DBoperations.Insert;

            ctySvc.ManageCity(cty);
        }
        [Test]
        public void Service_Student_CRUD()
        {
            StudentService stdSvc = new StudentService();

            //var list = new List<CourseDE>();
            //list = crsSvc.SearchCourse();

            var std = new StudentDE();
            //std.Id = 1;
            //std.Name = "Miss Sana";
            //std.CityId = 1;
            //std.City = "pakistann";
            //std.Email = "SanaJawed@gmail.com";
            ////crs.DBoperation = DBoperations.Update;
            //std.DBoperation = DBoperations.Insert;

            stdSvc.ManageStudent(std);
        }
        [Test]
        public void Service_CityStudent_CRUD()
        {
            CityStudentService ctystdSvc = new CityStudentService();

            //var list = new List<CourseDE>();
            //list = crsSvc.SearchCourse();

            var ctystd = new CityStudentDE();
            ctystd.Id = 1;

            ctystd.CityId = 1;
            ctystd.StudentId = 1;
            ctystd.City = "Lahore";
            ctystd.Student = "sana";


            //crs.DBoperation = DBoperations.Update;
            ctystd.DBoperation = DBoperations.Insert;

            ctystdSvc.ManageCityStudent(ctystd);
        }
        //[Test]
        //public void service_ScheduleFH_crud()
        //{
        //    ScheduleService schsvc = new ScheduleService();

        //    //var list = new list<coursede>();
        //    //list = crssvc.searchcourse();

        //    var sch = new ScheduleDE();
        //    sch.Id = 1;
        //    sch.UserId = 2;
        //    sch.User = "related to";
        //    sch.RoleId = 1;
        //    sch.ScheduleType = "fh";
        //    sch.WorkingFor = "week";
        //    sch.WorkingHours = "10 hours";

        //    //crs.dboperation = dboperations.update;
        //    sch.DBoperation = DBoperations.Insert;

        //    schsvc.ManageScheduleFH(sch);
        //}
    }
}

