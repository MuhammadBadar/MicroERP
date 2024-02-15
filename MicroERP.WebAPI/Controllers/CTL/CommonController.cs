using KeyAccounting.Core.Extenstions;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        [HttpPost("dob")]
        public IActionResult GetAge ( [FromBody]  DateTime dob )
        {
            string age = dob.GetAgeFromDOB ();
            return Ok ("\"" + age + "\"");
        }
    }
}
