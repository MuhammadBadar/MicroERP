using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QST.ERP.Domain;
using QST.ERP.Domain.BDM;
using QST.ERP.Domain.GroceryKit;
using QST.ERP.Services;
using System.IO;
using System.Linq;

using QST.ERP.Domain.Translators;

namespace QST.ERP.Services.Tests
{
    [TestClass]
    public class BDM
    {
        private IBDMServiceContract _bdmSvc;

        public BDM()
        {
            _bdmSvc = new BDMService();
        }

        [TestMethod]
        public void Entity_CRUD()
        {
            EntityDE ety = new EntityDE();
            ety.SiteCode = "QST";
            ety.EntityTypeCode = "DNR";
            ety.EntityName = "Khairaat";
            ety.Code = "090";

            _bdmSvc = new BDMService();
            int id = _bdmSvc.AddEntity(ety);

            ety.ID = id;
            ety.EntityName = "Khariraat ...";
            bool res = _bdmSvc.ModifyEntity(ety);

            res = _bdmSvc.DeleteEntity(ety.SiteCode, ety.EntityTypeCode, ety.ID);
        }

        [TestMethod]
        public void Person_CRUD()
        {
            _bdmSvc = new BDMService();
            
            Person p = new Person();
            //p.SiteCode = "QST";
            //p.EntityName = "Imran";
            //p.EntityTypeCode = "DNR";

            //var mod = _bdmSvc.AddPerson(p);

            var list = _bdmSvc.GetViewOfAllPersons("QST");
            if (list != null && list.Count > 0)
            {
                PersonView pv = list[0];
                //p = pv.ToPerson();
                //p.EntityName = "Imran ...";
                //p.Signature = "imran@123";
                //if (_bdmSvc.ModifyPerson(p))
                //{ 
                
                //}
                p = _bdmSvc.GetPersonById(pv.SiteCode, pv.EntityTypeCode, pv.ID);
            }
        }

        [TestMethod]
        public void Person_View()
        {
            _bdmSvc = new BDMService();
            var list = _bdmSvc.GetViewOfAllPersons("QST");
        }

        [TestMethod]
        public void EntityView()
        {
            _bdmSvc = new BDMService();
            var list = _bdmSvc.GetViewOfAllEntities("QST");
        }

        #region EmployeeCore

        [TestMethod]
        public void CRUD_Employee()
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


            EmployeeCoreBE mod = new EmployeeCoreBE();
            //mod.SiteCode = "QST";
            //mod.EntityName = "Jameel";
            //mod.Phone = "111222333";

            //int pId = _bdmSvc.AddEmployee(mod);
            //int vchId = _alKhairSvc.AddVoucher(mod);

            var list = _bdmSvc.GetViewOfAllEmployees("QST");// _alKhairSvc.GetAllVouchers("QST");//.GetViewOfAllVouchers("QST");
            if (list != null && list.Count > 0)
            {
                mod = list[0];
                //mod.VchNo = " ...";
                mod.EntityName = mod.EntityName + "...";

                if (_bdmSvc.ModifyEmployee(mod))
                {

                }
                //p = _bdmSvc.GetPersonById(pv.SiteCode, pv.EntityTypeCode, pv.ID);
            }

        }

        [TestMethod]
        public void CRUD_Bank()
        {
            #region Commented Code

            #endregion


            BankBE mod = new BankBE();
            mod.SiteCode = "QST";
            mod.EntityName = "Bank Alrijhi";
            //mod.Phone = "111222333";

            int id = _bdmSvc.AddBank(mod);
            //int vchId = _alKhairSvc.AddVoucher(mod);

            var list = _bdmSvc.GetAllBanks("QST");// _alKhairSvc.GetAllVouchers("QST");//.GetViewOfAllVouchers("QST");
            if (list != null && list.Count > 0)
            {
                mod = list[0];
                //mod.VchNo = " ...";
                mod.EntityName = mod.EntityName + "...";

                if (_bdmSvc.ModifyBank(mod))
                {

                }
                //p = _bdmSvc.GetPersonById(pv.SiteCode, pv.EntityTypeCode, pv.ID);
            }

        }

        #endregion
    }
}
