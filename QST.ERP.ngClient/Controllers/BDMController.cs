using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using QST.ERP.Domain;
using QST.ERP.Domain.BDM;
using QST.ERP.Services;
using QST.ERP.WebApi.Models;
using QST.ERP.ngClient.Translators;

namespace QST.ERP.ngClient.Controllers
{
    public class BDMController : BaseController // ApiController
    {
        private IBDMServiceContract _bdmSvc;
        private bool _mockingFlag;

        public BDMController()
        {
            _bdmSvc = new BDMService();
        }

        #region Region
        
        public RegionViewModel GetAllRegions()
        {
            var mod = new RegionViewModel();
            mod.IsValid = true;
            try
            {
                mod.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);//.Map(mod.Regions);
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }

            return mod;
        }

        public RegionViewModel AddRegion(RegionViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    if (_bdmSvc.RegionCodeExists(AppConstants.SITE_CODE, model.Region.RegionCode))
                    {
                        model.FieldId = "regionCode";
                        model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "Region Code");
                        model.IsValid = false;
                    }

                    if (model.IsValid)
                    {
                        model.Region.SiteCode = AppConstants.SITE_CODE;
                        _bdmSvc.AddRegion(model.Region);

                        model.FieldId = "regionCode";
                        model.Region = new RegionDE();
                        model.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
                        model.Message = string.Format(AppConstants.CRUD_CREATE, "Region");
                    }
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
            }

            return model;
        }

        public RegionViewModel ModifyRegion(RegionDE mod)
        {
            RegionViewModel model = new RegionViewModel();
                    
            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                _bdmSvc.ModifyRegion(mod);

                model.Region = new RegionDE();
                model.FieldId = "regionCode";
                model.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE); //.GetAllRegions().Map(model.Regions);
                model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "Region") : string.Format(AppConstants.CRUD_DELETE, "Region");
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

        #region City
        
        public CityViewModel GetAllCities()
        {
            var mod = new CityViewModel();
            mod.IsValid = true;
            try
            {
                mod.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
                mod.Cities = _bdmSvc.GetViewOfAllCities(AppConstants.SITE_CODE);
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }

            return mod;
        }

        public CityViewModel AddCity(CityViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    if (_bdmSvc.CityCodeExists(AppConstants.SITE_CODE, model.City.CityCode))
                    {
                        model.FieldId = "cityCode";
                        model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "City Code");
                        model.IsValid = false;
                    }

                    if (model.IsValid)
                    {
                        model.City.SiteCode = AppConstants.SITE_CODE;
                        _bdmSvc.AddCity(model.City);

                        model.FieldId = "cityCode";
                        model.City = new CityDE();
                        model.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
                        model.Cities = _bdmSvc.GetViewOfAllCities(AppConstants.SITE_CODE);
                        model.Message = string.Format(AppConstants.CRUD_CREATE, "City");
                    }
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
            }

            return model;
        }

        public CityViewModel ModifyCity(CityDE mod)
        {
            CityViewModel model = new CityViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.City = mod;
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    _bdmSvc.ModifyCity(mod);

                    model.FieldId = "cityCode";
                    model.City = new CityDE();
                    model.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
                    model.Cities = _bdmSvc.GetViewOfAllCities(AppConstants.SITE_CODE);
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "City") : string.Format(AppConstants.CRUD_DELETE, "City");
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

        public List<CityDE> GetCitiesByRegion(string regionCode)
        {
            return _bdmSvc.GetCitiesByRegion(AppConstants.SITE_CODE, regionCode);
        }

        #endregion

        #region Area

        public AreaViewModel GetAllAreas()
        {
            var mod = new AreaViewModel();
            mod.IsValid = true;
            try
            {
                mod.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
                mod.Areas = _bdmSvc.GetViewOfAllAreas(AppConstants.SITE_CODE);
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }

            return mod;
        }

        public AreaViewModel AddArea(AreaViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    if (_bdmSvc.AreaCodeExists(AppConstants.SITE_CODE, model.Area.AreaCode))
                    {
                        model.FieldId = "areaCode";
                        model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "Area Code");
                        model.IsValid = false;
                    }

                    if (model.IsValid)
                    {
                        model.Area.SiteCode = AppConstants.SITE_CODE;
                        _bdmSvc.AddArea(model.Area);

                        model.FieldId = "areaCode";
                        model.Area = new AreaDE();
                        model.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
                        model.Areas = _bdmSvc.GetViewOfAllAreas(AppConstants.SITE_CODE);
                        model.Message = string.Format(AppConstants.CRUD_CREATE, model.ViewName);
                    }
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
            }

            return model;
        }

        public AreaViewModel ModifyArea(AreaDE mod)
        {
            AreaViewModel model = new AreaViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.Area = mod;
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    _bdmSvc.ModifyArea(mod);

                    model.FieldId = "areaCode";
                    model.Area = new AreaDE();
                    model.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
                    model.Areas = _bdmSvc.GetViewOfAllAreas(AppConstants.SITE_CODE);
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, model.ViewName) : string.Format(AppConstants.CRUD_DELETE, "Area");
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

        public List<AreaDE> GetAreasByCity(string cityCode)
        {
            return _bdmSvc.GetAreasByCity(AppConstants.SITE_CODE, cityCode);
        }

        #endregion

        #region Manager

        public ManagerViewModel GetManagerById(decimal managerId)
        {
            ManagerViewModel mod = new ManagerViewModel();
            try
            {
                //mod.Manager = _bdmSvc.GetEntityById(AppConstants.SITE_CODE, managerId);
                //if (mod.Manager.Addresses.Count > 0)
                //    mod.Address = mod.Manager.Addresses[0];
                //if (mod.Manager.Contacts.Count > 0)
                //    mod.Contact = mod.Manager.Contacts[0];
            }
            catch (Exception ex)
            {
                mod.TranslateException(ex);
            }
            return mod;
        }

        public ManagerViewModel GetAllManagers()
        {
            ManagerViewModel mod = null;
            try
            {
                mod = RefreshManager();
            }
            catch (Exception ex)
            {
                mod.TranslateException(ex);
            }

            return mod;
        }

        private ManagerViewModel RefreshManager()
        {
            ManagerViewModel model = new ManagerViewModel();
            model.FieldId = "managerName";
            model.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
            model.Managers = _bdmSvc.GetViewOfAllEntities(AppConstants.SITE_CODE).Where(m => m.EntityTypeCode == EntityTypes.MGR.ToString()).ToList();
            return model;        
        }

        public ManagerViewModel AddManager(ManagerViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    model.Manager.SiteCode = AppConstants.SITE_CODE;

                    model.Address = base.TranslateNames(model.Address);
                    
                    model.Manager.Addresses.Add(model.Address);
                    model.Manager.Contacts.Add(model.Contact);
                    _bdmSvc.AddEntity(model.Manager);

                    model = RefreshManager();
                    model.Message = string.Format(AppConstants.CRUD_CREATE, "Manager");
                }
            }
            catch (Exception ex)
            {
                model.TranslateException(ex);
            }

            return model;
        }

        public ManagerViewModel ModifyManager(ManagerViewModel model)
        {
            try
            {
                DBOperations op = model.IsActive ? DBOperations.Update : DBOperations.Delete;
                model.Manager.SiteCode = AppConstants.SITE_CODE;
                if (model.IsActive)
                {
                    model.IsValid = model.Validate();
                    if (model.IsValid)
                    {
                        model.Address = base.TranslateNames(model.Address);
                        
                        if (model.Manager.Addresses.Count > 0)
                            model.Manager.Addresses[0] = model.Address.Translate(model.Manager.Addresses[0]);

                        if (model.Manager.Contacts.Count > 0)
                            model.Manager.Contacts[0] = model.Contact.Translate(model.Manager.Contacts[0]);
                        
                        _bdmSvc.ModifyEntity(model.Manager);
                    }
                }
                else
                {
                    model.Manager.IsActive = false;
                    _bdmSvc.ModifyEntity(model.Manager);
                }

                model = RefreshManager();
                model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "Manager") : string.Format(AppConstants.CRUD_DELETE, "Manager");
            }
            catch (Exception ex)
            {
                model.TranslateException(ex);
                if (ex.Message.Contains("Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded. See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions."))
                    model.Message = "Unable to modify Region Code";
            }

            return model;
        }

        #endregion

        #region Doctor

        public DoctorViewModel GetDoctorById(decimal doctorId)
        {
            DoctorViewModel mod = new DoctorViewModel();
            try
            {
                //mod.Doctor = _bdmSvc.GetEntityById(AppConstants.SITE_CODE, doctorId);
                //if (mod.Doctor.Addresses.Count > 0)
                //    mod.Address = mod.Doctor.Addresses[0];
                //if (mod.Doctor.Contacts.Count > 0)
                //    mod.Contact = mod.Doctor.Contacts[0];
            }
            catch (Exception ex)
            {
                mod.TranslateException(ex);
            }
            return mod;
        }

        public DoctorViewModel GetAllDoctors()
        {
            DoctorViewModel mod = null;
            try
            {
                mod = RefreshDoctor();
            }
            catch (Exception ex)
            {
                mod.TranslateException(ex);
            }

            return mod;
        }

        private DoctorViewModel RefreshDoctor()
        {
            DoctorViewModel model = new DoctorViewModel();
            model.FieldId = "doctorName";
            model.Regions = _bdmSvc.GetAllRegions(AppConstants.SITE_CODE);
            var entities = _bdmSvc.GetViewOfAllEntities(AppConstants.SITE_CODE);
            model.Doctors = entities.Where(m => m.EntityTypeCode == EntityTypes.DOC.ToString()).ToList();
            model.Managers = entities.Where(m => m.EntityTypeCode == EntityTypes.MGR.ToString()).ToList();
            //foreach (var mgr in model.Managers)
            //    mgr.ParentEntityID = mgr.ID;
            return model;
        }

        public void Translate(AddressDE src, ref AddressDE dest)
        {
            dest.RegionCode = src.RegionCode;
            dest.RegionName = src.RegionName;
            dest.CityCode = src.CityCode;
            dest.CityName = src.CityName;
            dest.AreaCode = src.AreaCode;
            dest.AreaName = src.AreaName;
            
        }
        public DoctorViewModel AddDoctor(DoctorViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    model.Doctor.SiteCode = AppConstants.SITE_CODE;
                    //var mgr = _bdmSvc.GetEntityById(AppConstants.SITE_CODE, decimal.Zero); // model.Doctor.ParentEntityID);
                    //if (mgr != null && mgr.Addresses.Count > 0)
                    //{
                    //    var src = mgr.Addresses[0];
                    //    AddressDE dest = model.Address;
                    //    Translate(src, ref dest);
                    //}
                    
                    //model.Doctor.Addresses.Add(model.Address);
                    //model.Doctor.Contacts.Add(model.Contact);
                    //_bdmSvc.AddEntity(model.Doctor);

                    //model = RefreshDoctor();
                    //model.Message = string.Format(AppConstants.CRUD_CREATE, "Doctor");
                }
            }
            catch (Exception ex)
            {
                model.TranslateException(ex);
            }

            return model;
        }

        public DoctorViewModel ModifyDoctor(DoctorViewModel model)
        {
            try
            {
                DBOperations op = model.IsActive ? DBOperations.Update : DBOperations.Delete;
                model.Doctor.SiteCode = AppConstants.SITE_CODE;
                if (model.IsActive)
                {
                    model.IsValid = model.Validate();
                    if (model.IsValid)
                    {
                        //model.Address = base.TranslateNames(model.Address);
                        //var mgr = _bdmSvc.GetEntityById(AppConstants.SITE_CODE, decimal.Zero); // model.Doctor.ParentEntityID);
                        //if (mgr != null && mgr.Addresses.Count > 0)
                        //{
                        //    var src = mgr.Addresses[0];
                        //    AddressDE dest = model.Address;
                        //    Translate(src, ref dest);
                        //}

                        //if (model.Doctor.Addresses.Count > 0)
                        //    model.Doctor.Addresses[0] = model.Address.Translate(model.Doctor.Addresses[0]);

                        //if (model.Doctor.Contacts.Count > 0)
                        //    model.Doctor.Contacts[0] = model.Contact.Translate(model.Doctor.Contacts[0]);

                        _bdmSvc.ModifyEntity(model.Doctor);
                    }
                }
                else
                {
                    model.Doctor.IsActive = false;
                    _bdmSvc.ModifyEntity(model.Doctor);
                }

                model = RefreshDoctor();
                model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "Doctor") : string.Format(AppConstants.CRUD_DELETE, "Doctor");
            }
            catch (Exception ex)
            {
                model.TranslateException(ex);
                if (ex.Message.Contains("Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded. See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions."))
                    model.Message = "Unable to modify Region Code";
            }

            return model;
        }

        #endregion

        #region ExpenseGroup

        public ExpenseGroupViewModel GetAllExpenseGroups()
        {
            var mod = new ExpenseGroupViewModel();
            mod.IsValid = true;
            try
            {
                mod.ExpenseGroups = _bdmSvc.GetAllExpenseGroups(AppConstants.SITE_CODE);
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }

            return mod;
        }

        public ExpenseGroupViewModel AddExpenseGroup(ExpenseGroupViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    if (_bdmSvc.ExpenseGroupCodeExists(AppConstants.SITE_CODE, model.ExpenseGroup.GroupCode))
                    {
                        model.FieldId = "groupCode";
                        model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "Group Code");
                        model.IsValid = false;
                    }

                    if (model.IsValid)
                    {
                        model.ExpenseGroup.SiteCode = AppConstants.SITE_CODE;
                        _bdmSvc.AddExpenseGroup(model.ExpenseGroup);

                        model.FieldId = "groupCode";
                        model.ExpenseGroup = new ExpenseGroupDE();
                        model.ExpenseGroups = _bdmSvc.GetAllExpenseGroups(AppConstants.SITE_CODE);
                        model.Message = string.Format(AppConstants.CRUD_CREATE, "Expense Group");
                    }
                }
            }
            catch (Exception ex)
            {
                model.IsValid = false;
                model.Message = ex.Message;
            }

            return model;
        }

        public ExpenseGroupViewModel ModifyExpenseGroup(ExpenseGroupDE mod)
        {
            ExpenseGroupViewModel model = new ExpenseGroupViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                _bdmSvc.ModifyExpenseGroup(mod);

                model.ExpenseGroup = new ExpenseGroupDE();
                model.FieldId = "groupCode";
                model.ExpenseGroups = _bdmSvc.GetAllExpenseGroups(AppConstants.SITE_CODE); //.GetAllExpenseGroups().Map(model.ExpenseGroups);
                model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "ExpenseGroup") : string.Format(AppConstants.CRUD_DELETE, "ExpenseGroup");
            }
            catch (Exception ex)
            {
                
                model.IsValid = false;
                model.Message = ex.Message;
                if (ex.Message.Contains("Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded. See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions."))
                {
                    model.FieldId = "groupCode";
                    model.Message = "Unable to modify ExpenseGroup Code";
                }
                    
            }
            return model;
        }

        #endregion

        #region ExpenseHead

        public ExpenseHeadViewModel GetAllExpenseHeads()
        {
            var mod = new ExpenseHeadViewModel();
            try
            {
                mod = RefreshExpenseHead();
            }
            catch (Exception ex)
            {
                mod.TranslateException(ex);
            }
            return mod;
        }

        public ExpenseHeadViewModel AddExpenseHead(ExpenseHeadViewModel model)
        {
            try
            {
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    if (_bdmSvc.ExpenseHeadCodeExists(AppConstants.SITE_CODE, model.ExpenseHead.ExpenseHeadCode))
                    {
                        model.FieldId = "groupCode";
                        model.Message = string.Format(AppConstants.VALIDATION_ALREADY_EXISTS, "ExpenseHead Code");
                        model.IsValid = false;
                    }

                    if (model.IsValid)
                    {
                        model.ExpenseHead.SiteCode = AppConstants.SITE_CODE;
                        _bdmSvc.AddExpenseHead(model.ExpenseHead);

                        model = RefreshExpenseHead(); 
                        model.Message = string.Format(AppConstants.CRUD_CREATE, "ExpenseHead");
                    }
                }
            }
            catch (Exception ex)
            {
                model.TranslateException(ex);
            }

            return model;
        }

        public ExpenseHeadViewModel ModifyExpenseHead(ExpenseHeadDE mod)
        {
            ExpenseHeadViewModel model = new ExpenseHeadViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.ExpenseHead = mod;
                model.IsValid = model.Validate();
                if (model.IsValid)
                {
                    _bdmSvc.ModifyExpenseHead(mod);

                    model = RefreshExpenseHead();
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "ExpenseHead") : string.Format(AppConstants.CRUD_DELETE, "ExpenseHead");
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

        public ExpenseHeadDE GetExpenseHeadsByExpenseGroup(string groupCode)
        {
            return _bdmSvc.GetExpenseHeadByCode(AppConstants.SITE_CODE, groupCode);
        }

        private ExpenseHeadViewModel RefreshExpenseHead()
        {
            ExpenseHeadViewModel model = new ExpenseHeadViewModel();
            model.FieldId = "groupCode";
            model.ExpenseHead = new ExpenseHeadDE();
            model.ExpenseGroups = _bdmSvc.GetAllExpenseGroups(AppConstants.SITE_CODE);
            model.ExpenseHeads = _bdmSvc.GetViewOfAllExpenseHeads(AppConstants.SITE_CODE);
            
            return model;
        }

        #endregion

        #region EmployeeCore

        public EmployeeCoreViewModel GetAllEmployeeCores()
        {
            var mod = new EmployeeCoreViewModel();
            mod.IsValid = true;
            try
            {
                mod.EmployeeCores = _bdmSvc.GetViewOfAllEmployees(AppConstants.SITE_CODE).Where(m => m.IsActive = true).ToList();
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }
            return mod;
        }

        public EmployeeCoreViewModel AddEmployeeCore(EmployeeCoreViewModel model)
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
                    model.EmployeeCore.SiteCode = AppConstants.SITE_CODE;
                    //model.EmployeeCore.EntityTypeCode = EntityTypeCodes.GFT.ToString();
                    _bdmSvc.AddEmployee(model.EmployeeCore);

                    model.FieldId = "vchNo";
                    model.EmployeeCore = new EmployeeCoreBE();
                    model.EmployeeCores = _bdmSvc.GetViewOfAllEmployees(AppConstants.SITE_CODE).Where(m => m.IsActive = true).ToList(); //.GetViewOfAllEmployeeCores(AppConstants.SITE_CODE);
                    model.Message = string.Format(AppConstants.CRUD_CREATE, "Employee Core");
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

        public EmployeeCoreViewModel ModifyEmployeeCore(EmployeeCoreBE mod)
        {
            EmployeeCoreViewModel model = new EmployeeCoreViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.EmployeeCore = mod;
                model.IsValid = model.Validate();
                if (op == DBOperations.Delete || model.IsValid)
                {
                    //_bdmSvc.ModifyEntity(mod);
                    _bdmSvc.ModifyEmployee(mod);

                    model.FieldId = "EmployeeCoreName";
                    model.EmployeeCore = new EmployeeCoreBE();
                    model.EmployeeCores = _bdmSvc.GetViewOfAllEmployees(AppConstants.SITE_CODE).Where(m => m.IsActive = true).ToList();
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "Employee Name") : string.Format(AppConstants.CRUD_DELETE, "Employee Name");
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

        #region Bank

        public BankViewModel GetAllBanks()
        {
            var mod = new BankViewModel();
            mod.IsValid = true;
            try
            {
                mod.Banks = _bdmSvc.GetAllBanks(AppConstants.SITE_CODE).Where(m => m.IsActive = true).ToList();
            }
            catch (Exception ex)
            {
                mod.IsValid = false;
                mod.Message = ex.Message;
            }
            return mod;
        }

        public BankViewModel AddBank(BankViewModel model)
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
                    model.Bank.SiteCode = AppConstants.SITE_CODE;
                    //model.Bank.EntityTypeCode = EntityTypeCodes.GFT.ToString();
                    _bdmSvc.AddBank(model.Bank);

                    model.FieldId = "vchNo";
                    model.Bank = new BankBE();
                    model.Banks = _bdmSvc.GetAllBanks(AppConstants.SITE_CODE).Where(m => m.IsActive = true).ToList(); //.GetViewOfAllBanks(AppConstants.SITE_CODE);
                    model.Message = string.Format(AppConstants.CRUD_CREATE, "Bank");
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

        public BankViewModel ModifyBank(BankBE mod)
        {
            BankViewModel model = new BankViewModel();

            try
            {
                DBOperations op = mod.IsActive ? DBOperations.Update : DBOperations.Delete;
                mod.SiteCode = AppConstants.SITE_CODE;
                model.Bank = mod;
                model.IsValid = model.Validate();
                if (op == DBOperations.Delete || model.IsValid)
                {
                    //_bdmSvc.ModifyEntity(mod);
                    _bdmSvc.ModifyBank(mod);

                    model.FieldId = "BankName";
                    model.Bank = new BankBE();
                    model.Banks = _bdmSvc.GetAllBanks(AppConstants.SITE_CODE).Where(m => m.IsActive = true).ToList();
                    model.Message = op == DBOperations.Update ? string.Format(AppConstants.CRUD_UPDATE, "Bank Name") : string.Format(AppConstants.CRUD_DELETE, "Bank Name");
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