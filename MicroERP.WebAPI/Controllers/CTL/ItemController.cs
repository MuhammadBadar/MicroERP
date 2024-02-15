using Microsoft.AspNetCore.Mvc;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Core.ViewModel;
using MicroERP.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        #region Class Variables

        private ItemService _itemSVC;

        #endregion
        #region Constructors
        public ItemController()
        {
            _itemSVC = new ItemService();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            ItemDE item = new ItemDE ();
            List<ItemDE> items = _itemSVC.SearchItems(item);
            return Ok(items);
        }
        [HttpGet("{id}")]
        public ActionResult GetItemById(int id)
        {
            ItemDE item = new ItemDE { Id = id };
            var items = _itemSVC.SearchItems(item);
            return Ok(items);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(ItemDE Search)
        {
            //Search.IsActive = true;
            List<ItemDE> item = _itemSVC.SearchItems(Search);
            return Ok(item);
        }
        [HttpPost]
        public ActionResult Post(ItemDE item)
        {
            item.DBoperation = DBoperations.Insert;
            ItemDE Item = _itemSVC.ManagementItem(item);
            return Ok(Item);
        }
        [HttpPut]
        public ActionResult Put(ItemDE item)
        {
            item.DBoperation = DBoperations.Update;
            ItemDE Item = _itemSVC.ManagementItem(item);
            return Ok(Item);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            ItemDE item = new ItemDE { Id = id, DBoperation = DBoperations.DeActivate };
            ItemDE Item = _itemSVC.ManagementItem(item);
            return Ok(Item);
        }

        #endregion
    }
}
