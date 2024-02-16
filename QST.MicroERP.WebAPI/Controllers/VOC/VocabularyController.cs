using QST.MicroERP.Core.Entities.VOC;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service.VOC;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QST.MicroERP.WebAPI.Controllers.VOC
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private VocabularyService _vcbSvc;
        public VocabularyController()
        {
            _vcbSvc = new VocabularyService();
        }
        [HttpGet]
        public IActionResult GetVocabulary()
        {
            List<VocabularyDE> list = new List<VocabularyDE>();
            list = _vcbSvc.SearchVocabulary(new VocabularyDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SaveVocabulary(VocabularyDE vocabulary)
        {
            List<VocabularyDE> list = _vcbSvc.SearchVocabulary(vocabulary);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult PostVocabulary(VocabularyDE vocabulary)
        {
            vocabulary.DBoperation = DBoperations.Insert;
            vocabulary.UserVocab.DBoperation = DBoperations.Insert;
            var retVal = _vcbSvc.ManageVocabularyAsync(vocabulary);
            return Ok(retVal);
        }

        [HttpPut]
        public IActionResult PutVocabulary(VocabularyDE vocabulary)
        {
            vocabulary.DBoperation = DBoperations.Update;
            vocabulary.UserVocab.DBoperation = DBoperations.Update;
            var retVal = _vcbSvc.ManageVocabularyAsync(vocabulary);
            return Ok(retVal);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVocabulary(int id)
        {
            var vocab = new VocabularyDE();
            vocab.Id = id;
            vocab.DBoperation = DBoperations.DeActivate;
            var retVal = _vcbSvc.ManageVocabularyAsync(vocab);
            return Ok(retVal);
        }
    }
}
