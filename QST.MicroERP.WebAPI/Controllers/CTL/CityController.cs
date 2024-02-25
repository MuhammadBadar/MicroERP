using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service.CLT;
using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Service.CTL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QST.MicroERP.WebAPI.Controllers.CTL
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {

        // GET: api/<CityController>
        #region Class Variables

        //private CatalogueService _catSvc;

        #endregion
        #region Constructors
        public CityController()
        {
            //_catSvc = new CatalogueService();
        }

        #endregion

        #region Http Verbs

        [HttpGet]


        public IActionResult GetCity()
        {
            CityDE ctySC = new CityDE();
            List<CityDE> city = _catSvc.SearchCity(ctySC);
            //_catSvc.
            return Ok(city);
        }

        //// GET api/<VocabularyController>/5
        //[HttpGet("{id}")]
        //public IActionResult GetVocabularyById(int id)
        //{
        //    return "value";
        //}


        [HttpPost("{Search}")]
        public IActionResult SearchCity(CityDE city)
        {
            List<CityDE> list = _catSvc.SearchCity(city);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult PostCity(CityDE city)
        {
            city.DBoperation = DBoperations.Insert;
            bool cty = _catSvc.ManageCity(city);
            return Ok(cty);
        }


        // PUT api/<VocabularyController>/5
        [HttpPut]
        public IActionResult PutCity(CityDE city)
        {
            city.DBoperation = DBoperations.Update;
            _catSvc.ManageCity(city);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            CityDE city = new CityDE();
            city.DBoperation = DBoperations.Delete;
            city.Id = id;
            _catSvc.ManageCity(city);
            return Ok();
        }
    }
    #endregion
}

