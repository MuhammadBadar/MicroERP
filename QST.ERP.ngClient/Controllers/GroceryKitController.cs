using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using QST.ERP.Domain.GroceryKit;
using QST.ERP.Domain.BDM;
using QST.ERP.ngClient.ViewModels;
using QST.ERP.Services;
using QST.ERP.Domain;

namespace QST.ERP.ngClient.Controllers
{
    public class GroceryKitController : ApiController
    {
        private IBDMServiceContract _bdmSvc;
        private IGroceryKit _grocerySvc;
        private bool _mockingFlag;

        public GroceryKitController()
        {
            _bdmSvc = new BDMService();
            _grocerySvc = new GroceryKitService();
        }

        #region GroceryKit

        //public RegionViewModel GetAllRegions()
        [HttpGet]
        public GroceryKitViewModel ManageGroceryKit()
        {
            var mod = new GroceryKitViewModel();
            try
            {
                mod = RefreshGroceryKit();
            }
            catch (Exception ex)
            {
                mod.TranslateException(ex);
            }
            return mod;
        }

        public GroceryKitViewModel GetAllGroceryKits()
        {
            var mod = new GroceryKitViewModel();
            try
            {
                mod = RefreshGroceryKit();
            }
            catch (Exception ex)
            {
                mod.TranslateException(ex);
            }
            return mod;
        }

        public GroceryKitViewModel AddGroceryKit(GroceryKitViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    //if (_bdmSvc.GroceryFormCodeExists(AppConstants.SITE_CODE, model.GroceryKit.GroceryKitCode))
                    //{
                    //    model.FieldId = "groupCode";
                    //    model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "GroceryKit Code");
                    //    model.IsValid = false;
                    //}

                    //if (model.IsValid)
                    {
                        model.Form.SiteCode = AppConstants.SITE_CODE;
                        //_grocerySvc.AddForm(model.Form);

                        model = RefreshGroceryKit();
                        model.Message = string.Format(AppConstants.CRUD_CREATE, "GroceryKit");
                    }
                }
            }
            catch (Exception ex)
            {
                model.TranslateException(ex);
            }

            return model;
        }

        public GroceryKitViewModel ModifyGroceryKit(FormDE mod)
        {
            GroceryKitViewModel model = new GroceryKitViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.Form = mod;
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    //_bdmSvc.ModifyGroceryKit(mod);

                    model = RefreshGroceryKit();
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "GroceryKit") : string.Format(AppConstants.CRUD_DELETE, "GroceryKit");
                }
            }
            catch (Exception ex)
            {
                model.TranslateException(ex);
                if (ex.Message.Contains("Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded. See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions."))
                {
                    model.IsActive = false;
                    model.Message = "Unable to modify ExpenseGroup Code";
                }
            }
            return model;
        }

        //public GroceryKitDE GetGroceryKitsByExpenseGroup(string groupCode)
        //{
        //    return _bdmSvc.GetGroceryKitByCode(AppConstants.SITE_CODE, groupCode);
        //}

        private GroceryKitViewModel RefreshGroceryKit()
        {
            GroceryKitViewModel model = new GroceryKitViewModel();
            model.FieldId = "formNo";
            model.Form = new FormDE();
            //model.ExpenseGroups = _bdmSvc.GetAllExpenseGroups(AppConstants.SITE_CODE);
            //model.GroceryKits = _grocerySvc.GetViewOfAllForms();
            model.Occupations = _bdmSvc.GetAllOccupations();
            model.MemberStatuses = _grocerySvc.GetAllMemberStatuses();
            model.MedicalProblems = _grocerySvc.GetAllMedicalProblems();

            return model;
        }

        #endregion

    }
}
