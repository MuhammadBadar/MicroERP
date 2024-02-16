
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace QST.MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        #region Class Variables
        private VoucherService _voucherSVC;
        #endregion
        #region Constructor
        public VoucherController()
        {
            _voucherSVC = new VoucherService();
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get()
        {
            VoucherDE Voucher = new VoucherDE ();
            List<VoucherDE> values = _voucherSVC.SearchVouchers(Voucher);
            return Ok(values);
        }
        [HttpGet("{id}")]
        public ActionResult GetVoucherById(int id)
        {
            VoucherDE Voucher = new VoucherDE { Id = id };
            var values = _voucherSVC.SearchVouchers(Voucher);
            return Ok(values);
        }
        [HttpPost("{Search}")]
        public ActionResult Search(VoucherDE Search)
        {
            //Search.IsActive = true;
            List<VoucherDE> values = _voucherSVC.SearchVouchers(Search);
            return Ok(values);
        }
        [HttpPost]
        public ActionResult Post(VoucherDE vch)
        {
            vch.DBoperation = DBoperations.Insert;
            foreach (VoucherDetailDE line in vch.VoucherDetails)
            {
                line.DBoperation = vch.DBoperation;
            }
            VoucherDE Voucher = _voucherSVC.ManagementVoucher(vch);
            return Ok(Voucher);
        }
        [HttpPut("{Update}/{Activate}")]
        public ActionResult Activate(VoucherDE vch)
        {
            vch.DBoperation = DBoperations.Activate;
            _voucherSVC.ManagementVoucher(vch);
            return Ok();
        }
        [HttpPut("{DeActivate}")]
        public ActionResult DeActivate(VoucherDE vch)
        {
            vch.DBoperation = DBoperations.DeActivate;
            _voucherSVC.ManagementVoucher(vch);
            return Ok();
        }
        [HttpPut]
        public ActionResult Put(VoucherDE vch)
        {
            vch.DBoperation = DBoperations.Update;
            VoucherDE Voucher = _voucherSVC.ManagementVoucher(vch);
            return Ok(Voucher);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            VoucherDE Voucher = new VoucherDE { Id = id, DBoperation = DBoperations.Delete };
            _voucherSVC.ManagementVoucher(Voucher);
        }
        #endregion
    }
}
