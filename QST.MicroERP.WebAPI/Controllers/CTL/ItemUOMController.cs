using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Service;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ItemUOMController : ControllerBase
    {
        #region Class Variables

        private ItemUOMService _ItemUOMSVC;

        #endregion
        #region Constructors
        public ItemUOMController ( )
        {
            _ItemUOMSVC = new ItemUOMService ();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get ( )
        {
            ItemUOMDE ItemUOM = new ItemUOMDE ();
            List<ItemUOMDE> ItemUOMs = _ItemUOMSVC.SearchItemUOM (ItemUOM);
            return Ok (ItemUOMs);
        }
        [HttpGet ("{id}")]
        public ActionResult GetItemUOMById ( int id )
        {
            ItemUOMDE ItemUOM = new ItemUOMDE { Id = id };
            var ItemUOMs = _ItemUOMSVC.SearchItemUOM (ItemUOM);
            return Ok (ItemUOMs);
        }
        [HttpPost ("{Search}")]
        public ActionResult Search ( ItemUOMDE Search )
        {
            // Search.IsActive = true;
            List<ItemUOMDE> ItemUOM = _ItemUOMSVC.SearchItemUOM (Search);
            return Ok (ItemUOM);
        }
        [HttpPost]
        public ActionResult Post ( ItemUOMDE _attVal )
        {
            _attVal.DBoperation = DBoperations.Insert;
            bool retVla = _ItemUOMSVC.ManagementItemUOM (_attVal);
            return Ok (retVla);
        }
        [HttpPut]
        public ActionResult Put ( ItemUOMDE _attVal )
        {
            _attVal.DBoperation = DBoperations.Update;
            _ItemUOMSVC.ManagementItemUOM (_attVal);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public void Delete ( int id )
        {
            ItemUOMDE _attVal = new ItemUOMDE { Id = id, DBoperation = DBoperations.DeActivate };
            _ItemUOMSVC.ManagementItemUOM (_attVal);
        }

        #endregion
    }
}
