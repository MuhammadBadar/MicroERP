using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QST.ERP.Domain;
using QST.ERP.Domain.BDM;
using QST.ERP.Domain.AlKhair;
using QST.ERP.Domain.GroceryKit;
using QST.ERP.Services;
using System.IO;
using System.Linq;

using QST.ERP.Domain.Translators;

namespace QST.ERP.Services.Tests
{
    [TestClass]
    public class AlKhair
    {
        private IAlKhairServiceContract _alKhairSvc;

        public AlKhair()
        {
            _alKhairSvc = new AlKhairService();
        }

        [TestMethod]
        public void CRUD_GiftType()
        {
            
            var list = _alKhairSvc.GetViewOfAllGiftTypes("QST", EntityTypeCodes.GFT.ToString());
            GiftTypeBE mod = new GiftTypeBE();
            mod.IsCondition = true;
            mod.Price = 400;
            mod.SiteCode = "QST";
            mod.EntityTypeCode = "GFT";
            mod.EntityName = "Khairaat";
            mod.Code = "020";

            _alKhairSvc = new AlKhairService();
            int id = _alKhairSvc.AddGiftType(mod);

            //ety.ID = id;
            //ety.EntityName = "Khariraat ...";
            //bool res = _bdmSvc.ModifyEntity(ety);

            
        }

        [TestMethod]
        public void CRUD_Donor()
        {
            DonorBE mod = new DonorBE();
            //mod.SiteCode = "QST";
            //mod.EntityName = "Raja Rizwan";
            ////p.EntityTypeCode = "DNR";

            //int donorId = _alKhairSvc.AddDonor(mod);

            var list = _alKhairSvc.GetViewOfAllDonors("QST").OrderByDescending(m => m.ID).ToList();
            if (list != null && list.Count > 0)
            {
                mod = list[0].ToDonor();
                mod.EntityName = "Raja Rizwan ...";
                //p = pv.ToPerson();
                //p.EntityName = "Imran ...";
                //p.Signature = "imran@123";
                if (_alKhairSvc.ModifyDonor(mod))
                { 

                }
                //p = _bdmSvc.GetPersonById(pv.SiteCode, pv.EntityTypeCode, pv.ID);
            }

        }

        [TestMethod]
        public void CRUD_Voucher()
        {
            #region Commented Code

            //    public string VchNo { get; set; }
        //public int GiftTypeId { get; set; }
        //public int ReceivedFrom { get; set; }
        //public string VchTypeCode { get; set; }
        //public int VchMonth { get; set; }
        //public int VchYear { get; set; }
        //public string VchKeyId { get; set; }
        //public DateTime VchDate { get; set; }
        //public decimal VchAmount { get; set; }
        //public string PaymentMode { get; set; }
        //public string ChequeNo { get; set; }
        //public DateTime ChequeDate { get; set; }
            //public int BankId { get; set; }
            #endregion


            VoucherBE mod = new VoucherBE();
            //mod.SiteCode = "QST";
            //mod.VchNo = "1234";
            //mod.GiftTypeId = 10;
            //mod.ReceivedFrom = 39;
            //mod.VchAmount = 3999;
            //mod.PaymentMode = "Cash";

            //int vchId = _alKhairSvc.AddVoucher(mod);

            var list = _alKhairSvc.GetAllVouchers("QST");//.GetViewOfAllVouchers("QST");
            if (list != null && list.Count > 0)
            {
                mod = list[0];
                //mod.VchNo = " ...";
                mod.VchAmount = 39949;

                if (_alKhairSvc.ModifyVoucher(mod))
                {

                }
                //p = _bdmSvc.GetPersonById(pv.SiteCode, pv.EntityTypeCode, pv.ID);
            }

        }
    }
}
