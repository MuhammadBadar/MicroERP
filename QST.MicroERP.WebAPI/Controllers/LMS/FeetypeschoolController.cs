﻿using QST.MicroERP.Core.Entities.LMS;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service.LMS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers.LMS
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeetypeschoolController : ControllerBase
    {
        private FeetypeschoolService _feetypeschoolSvc;
        public FeetypeschoolController()
        {
            _feetypeschoolSvc = new FeetypeschoolService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetFeetypeschool()
        {
            List<FeetypeschoolDE> list = new List<FeetypeschoolDE>();
            list = _feetypeschoolSvc.SearchFeetypeschool(new FeetypeschoolDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchFeetypeschool(FeetypeschoolDE feetypeschool)
        {
            List<FeetypeschoolDE> list = _feetypeschoolSvc.SearchFeetypeschool(feetypeschool);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetFeetypeschoolById(int id)
        {
            List<FeetypeschoolDE> list = new List<FeetypeschoolDE>();
            list = _feetypeschoolSvc.SearchFeetypeschool(new FeetypeschoolDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostFeetypeschool(FeetypeschoolDE feetypeschool)
        {
            feetypeschool.DBoperation = DBoperations.Insert;
            _feetypeschoolSvc.ManageFeetypeschool(feetypeschool);
            return Ok();
        }

        [HttpPut]
        public IActionResult PutFeetypeschool(FeetypeschoolDE feetypeschool)
        {
            feetypeschool.DBoperation = DBoperations.Update;
            _feetypeschoolSvc.ManageFeetypeschool(feetypeschool);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeetypeschool(int id)
        {
            FeetypeschoolDE feetypeschoolDe = new FeetypeschoolDE();
            feetypeschoolDe.DBoperation = DBoperations.Delete;
            feetypeschoolDe.Id = id;
            _feetypeschoolSvc.ManageFeetypeschool(feetypeschoolDe);
            return Ok();
        }
        [HttpGet("Titles")]
        public IActionResult GetFeetypeschoolTitles()
        {
            List<FeetypeschoolDE> list = _feetypeschoolSvc.SearchFeetypeschool(new FeetypeschoolDE());
            List<string> titles = list.Select(f => f.Title).ToList();
            return Ok(titles);
        }
    }
}
