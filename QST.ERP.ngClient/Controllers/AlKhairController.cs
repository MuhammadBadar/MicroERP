using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.UI;
using QST.ERP.Domain;
using QST.ERP.Domain.AlKhair;
using QST.ERP.Services;
using QST.ERP.WebApi.Models;
using QST.ERP.ngClient.Translators;

namespace QST.ERP.ngClient.Controllers
{
    public class AlKhairController : BaseController
    {
        private IAlKhairServiceContract _alKhairSvc;

        public AlKhairController()
        {
            _alKhairSvc = new AlKhairService();
        }

        #region GiftType

        public GiftTypeViewModel GetAllGiftTypes()
        {
            var mod = new GiftTypeViewModel();
            mod.IsValid = true;
            try
            {
                mod.GiftTypes = _alKhairSvc.GetViewOfAllGiftTypes(AppConstants.SITE_CODE, EntityTypeCodes.GFT.ToString());
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }
            return mod;
        }

        public GiftTypeViewModel AddGiftType(GiftTypeViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    //if (_bdmSvc.CityCodeExists(AppConstants.SITE_CODE, model.City.CityCode))
                    //{
                    //    model.FieldId = "cityCode";
                    //    model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "City Code");
                    //    model.IsValid = false;
                    //}

                    //if (model.IsValid)
                    //{
                        model.GiftType.SiteCode = AppConstants.SITE_CODE;
                        model.GiftType.EntityTypeCode = EntityTypeCodes.GFT.ToString();
                        _alKhairSvc.AddGiftType(model.GiftType);

                        model.FieldId = "code";
                        model.GiftType = new GiftTypeBE();
                        model.GiftTypes = _alKhairSvc.GetViewOfAllGiftTypes(AppConstants.SITE_CODE, EntityTypeCodes.GFT.ToString());
                        model.Message = string.Format(AppConstants.CRUD_CREATE, "Gift Type");
                    //}
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
            }

            return model;
        }

        public GiftTypeViewModel ModifyGiftType(GiftTypeBE mod)
        {
            GiftTypeViewModel model = new GiftTypeViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.GiftType = mod;
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    //_bdmSvc.ModifyEntity(mod);
                    _alKhairSvc.ModifyGiftType(mod);

                    model.FieldId = "code";
                    model.GiftType = new GiftTypeBE();
                    model.GiftTypes = _alKhairSvc.GetViewOfAllGiftTypes(AppConstants.SITE_CODE, EntityTypeCodes.GFT.ToString());
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "Gift Type") : string.Format(AppConstants.CRUD_DELETE, "Gift Type");
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
                if (ex.Message.Contains("Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded. See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions."))
                    model.Message = "Unable to modify Region Code";
            }
            return model;
        }


        #endregion
        
        #region Donor

        public DonorViewModel GetAllDonors()
        {
            var mod = new DonorViewModel();
            mod.IsValid = true;
            try
            {
                mod.Donors = _alKhairSvc.GetViewOfAllDonors(AppConstants.SITE_CODE); //.GetViewOfAllDonors(AppConstants.SITE_CODE, EntityTypeCodes.GFT.ToString());
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }
            return mod;
        }

        public DonorViewModel AddDonor(DonorViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    //if (_bdmSvc.CityCodeExists(AppConstants.SITE_CODE, model.City.CityCode))
                    //{
                    //    model.FieldId = "cityCode";
                    //    model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "City Code");
                    //    model.IsValid = false;
                    //}

                    //if (model.IsValid)
                    //{
                    model.Donor.SiteCode = AppConstants.SITE_CODE;
                    model.Donor.EntityTypeCode = EntityTypeCodes.GFT.ToString();
                    _alKhairSvc.AddDonor(model.Donor);

                    model.FieldId = "donorName";
                    model.Donor = new DonorBE();
                    model.Donors = _alKhairSvc.GetViewOfAllDonors(AppConstants.SITE_CODE);
                    model.Message = string.Format(AppConstants.CRUD_CREATE, "Donor Name");
                    //}
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
            }

            return model;
        }

        public DonorViewModel ModifyDonor(DonorBE mod)
        {
            DonorViewModel model = new DonorViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.Donor = mod;
                model.IsValid = model.Validate();
                if (op == DBOperations.Delete || model.IsValid)
                {
                    //_bdmSvc.ModifyEntity(mod);
                    _alKhairSvc.ModifyDonor(mod);

                    model.FieldId = "donorName";
                    model.Donor = new DonorBE();
                    model.Donors = _alKhairSvc.GetViewOfAllDonors(AppConstants.SITE_CODE);
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "Donor Name") : string.Format(AppConstants.CRUD_DELETE, "Donor Name");
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
                if (ex.Message.Contains("Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded. See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions."))
                    model.Message = "Unable to modify Region Code";
            }
            return model;
        }


        #endregion

        #region Voucher

        public VoucherViewModel GetAllVouchers()
        {
            var mod = new VoucherViewModel();
            mod.IsValid = true;
            try
            {
                string userName = (string)HttpContext.Current.Session["UserName"]; // (string)Session["UserName"];
                mod.Voucher.VchNo = _alKhairSvc.GetNextVoucherNo(userName);
                mod.Vouchers = _alKhairSvc.GetAllVouchers(AppConstants.SITE_CODE); //.GetViewOfAllVouchers(AppConstants.SITE_CODE, EntityTypeCodes.GFT.ToString());
                mod.GiftTypes = _alKhairSvc.GetViewOfAllGiftTypes(AppConstants.SITE_CODE, EntityTypeCodes.GFT.ToString());
                mod.Donors = _alKhairSvc.GetViewOfAllDonors(AppConstants.SITE_CODE);
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }
            return mod;
        }

        public VoucherViewModel AddVoucher(VoucherViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    //if (_bdmSvc.CityCodeExists(AppConstants.SITE_CODE, model.City.CityCode))
                    //{
                    //    model.FieldId = "cityCode";
                    //    model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "City Code");
                    //    model.IsValid = false;
                    //}

                    //if (model.IsValid)
                    //{
                    model.Voucher.SiteCode = AppConstants.SITE_CODE;
                    model.Voucher.UserName = (string)HttpContext.Current.Session["UserName"];
                    //model.Voucher.EntityTypeCode = EntityTypeCodes.GFT.ToString();
                    _alKhairSvc.AddVoucher(model.Voucher);

                    model.FieldId = "vchNo";
                    model.Voucher = new VoucherBE();
                    string userName = (string)HttpContext.Current.Session["UserName"]; // (string)Session["UserName"];
                    model.Voucher.VchNo = _alKhairSvc.GetNextVoucherNo(userName);
                
                    model.Vouchers = _alKhairSvc.GetAllVouchers(AppConstants.SITE_CODE); //.GetViewOfAllVouchers(AppConstants.SITE_CODE);
                    model.Message = string.Format(AppConstants.CRUD_CREATE, "Voucher");
                    //}
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
            }

            return model;
        }

        public VoucherViewModel ModifyVoucher(VoucherBE mod)
        {
            VoucherViewModel model = new VoucherViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.Voucher = mod;
                model.IsValid = model.Validate();
                if (op == DBOperations.Delete || model.IsValid)
                {
                    //_bdmSvc.ModifyEntity(mod);
                    _alKhairSvc.ModifyVoucher(mod);

                    model.FieldId = "VoucherName";
                    model.Voucher = new VoucherBE();
                    model.Vouchers = _alKhairSvc.GetViewOfAllVouchers(AppConstants.SITE_CODE);
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "Voucher Name") : string.Format(AppConstants.CRUD_DELETE, "Voucher Name");
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
                if (ex.Message.Contains("Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded. See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions."))
                    model.Message = "Unable to modify Region Code";
            }
            return model;
        }


        #endregion
    }
}