using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.Service;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ItemVariantsController : ControllerBase
    {
        #region Class Variables

        private ItemVariantsService _ItemVariantsSVC;

        #endregion
        #region Constructors
        public ItemVariantsController ( )
        {
            _ItemVariantsSVC = new ItemVariantsService ();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get ( )
        {
            ItemVariantsDE ItemVariants = new ItemVariantsDE ();
            List<ItemVariantsDE> ItemVariantss = _ItemVariantsSVC.SearchItemVariants (ItemVariants);
            return Ok (ItemVariantss);
        }
        [HttpGet ("{id}")]
        public ActionResult GetItemVariantsById ( int id )
        {
            ItemVariantsDE ItemVariants = new ItemVariantsDE { Id = id };
            var ItemVariantss = _ItemVariantsSVC.SearchItemVariants (ItemVariants);
            return Ok (ItemVariantss);
        }
        [HttpPost ("{Search}")]
        public ActionResult Search ( ItemVariantsDE Search )
        {
            //Search.IsActive = true;
            List<ItemVariantsDE> ItemVariants = _ItemVariantsSVC.SearchItemVariants (Search);
            return Ok (ItemVariants);
        }
        [HttpPost]
        public ActionResult Post ( ItemVariantsDE iVar )
        {
            iVar.DBoperation = DBoperations.Insert;
            bool retVla = _ItemVariantsSVC.ManagementItemVariants (iVar);
            return Ok (retVla);
        }
        [HttpPut]
        public ActionResult Put ( ItemVariantsDE iVar )
        {
            iVar.DBoperation = DBoperations.Update;
            _ItemVariantsSVC.ManagementItemVariants (iVar);
            return Ok ();
        }
        [HttpDelete ("{id}")]
        public void Delete ( int id )
        {
            ItemVariantsDE iVar = new ItemVariantsDE { Id = id, DBoperation = DBoperations.DeActivate };
            _ItemVariantsSVC.ManagementItemVariants (iVar);
        }

        #endregion
    }
}
