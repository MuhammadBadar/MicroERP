using QST.MicroERP.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers.CTL
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        [HttpPost("StringToList")]
        public IActionResult ConvertString([FromBody] string input)
        {
            List<string> list = input.ConvertStringToList();
            return Ok(list);
        }
    }
}
