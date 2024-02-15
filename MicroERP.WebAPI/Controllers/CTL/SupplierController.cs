using Microsoft.AspNetCore.Mvc;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        #region Class Variables

        private SupplierService _supplierSVC;

        #endregion
        #region Constructors
        public SupplierController()
        {
            _supplierSVC = new SupplierService();
        }

        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                SupplierDE Supplier = new SupplierDE ();
                List<SupplierDE> list = _supplierSVC.SearchSuppliers(Supplier);
                return Ok(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{id}")]
        public ActionResult GetSupplierById(int id)
        {
            try
            {
                SupplierDE Supplier = new SupplierDE { Id = id };
                var list = _supplierSVC.SearchSuppliers(Supplier);
                return Ok(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("{Search}")]
        public ActionResult Search(SupplierDE Search)
        {
            try
            {
                //Search.IsActive = true;
                List<SupplierDE> list = _supplierSVC.SearchSuppliers(Search);
                return Ok(list);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost]
        public ActionResult Post(SupplierDE supplier)
        {
            try
            {
                supplier.DBoperation = DBoperations.Insert;
                bool f = _supplierSVC.ManagementSupplier(supplier);
                return Ok(f);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPut]
        public ActionResult Put(SupplierDE supplier)
        {
            try
            {
                supplier.DBoperation = DBoperations.Update;
                _supplierSVC.ManagementSupplier(supplier);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("{Supplier}")]
        public ActionResult DealAsACustomer(SupplierDE supplier)
        {
            try
            {
                supplier.DBoperation = DBoperations.Update;
                _supplierSVC.DealSupplierAsACustomer(supplier);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                SupplierDE Supplier = new SupplierDE { Id = id, DBoperation = DBoperations.DeActivate };
                _supplierSVC.ManagementSupplier(Supplier);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
