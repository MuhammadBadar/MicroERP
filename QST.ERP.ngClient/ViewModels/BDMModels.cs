using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using QST.ERP.Domain.BDM;
using QST.ERP.Domain;
using QST.ERP.Domain.AlKhair;

namespace QST.ERP.WebApi.Models
{
    public class RegionViewModel : BaseViewModel
    {
        public RegionViewModel()
        {
            //base.FieldId = "regionCode";
            Region = new RegionDE();
            Regions = new List<RegionDE>();
        }
        public RegionDE Region { get; set; }
        public List<RegionDE> Regions { get; set; }

        public override bool IsValid { get; set;}
        
        public override bool Validate()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(Region.RegionCode))
            {
                isValid = false;
                base.FieldId = "regionCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Region Code");
            }
            else if (string.IsNullOrEmpty(Region.RegionName))
            {
                isValid = false;
                base.FieldId = "regionName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Region Name");
            }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }
    public class CityViewModel : BaseViewModel
    {
        public CityViewModel()
        {
            City = new CityDE();
            Cities = new List<CityVW>();
            Regions = new List<RegionDE>();
        }

        public string RegionCode { get; set; }
        public CityDE City { get; set; }
        public List<CityVW> Cities { get; set; }
        
        public List<RegionDE> Regions { get; set; }
        

        public override bool IsValid { get; set; }

        public override bool Validate()
        {
            bool isValid = true;
            
            if (string.IsNullOrEmpty(City.CityCode))
            {
                isValid = false;
                base.FieldId = "cityCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "City Code");
            }
            else if (string.IsNullOrEmpty(City.CityName))
            {
                isValid = false;
                base.FieldId = "cityName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "City Name");
            }
            else if (string.IsNullOrEmpty(City.RegionCode))
            {
                isValid = false;
                base.FieldId = "regionCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Region");
            }

            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }
    public class AreaViewModel : BaseViewModel
    {
        public AreaViewModel()
        {
            Area = new AreaDE();
            Areas = new List<AreaVw>();
            Cities = new List<CityDE>();
            Regions = new List<RegionDE>();
            ViewName = "Distribution Point";
        }

        public AreaDE Area { get; set; }
        public List<AreaVw> Areas { get; set; }

        public List<RegionDE> Regions { get; set; }
        public List<CityDE> Cities { get; set; }

        public override bool IsValid { get; set; }

        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Area.AreaCode))
            {
                isValid = false;
                base.FieldId = "areaCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Area Code");
            }
            else if (string.IsNullOrEmpty(Area.AreaName))
            {
                isValid = false;
                base.FieldId = "areaName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Area Name");
            }
            else if (string.IsNullOrEmpty(Area.RegionCode))
            {
                isValid = false;
                base.FieldId = "regionCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Region");
            }
            else if (string.IsNullOrEmpty(Area.CityCode))
            {
                isValid = false;
                base.FieldId = "cityCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "City");
            }

            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }
    public class ManagerViewModel : BaseViewModel
    {
        public ManagerViewModel()
        {
            Address = new AddressDE();
            Contact = new ContactDE();
            Manager = new EntityDE(EntityTypes.MGR);
            Managers = new List<EntityView>();
            Cities = new List<CityDE>();
            Areas = new List<AreaDE>();
            Regions = new List<RegionDE>();
        }

        public AddressDE Address { get; set; }
        public ContactDE Contact { get; set; }
        public EntityDE Manager { get; set; }
        public List<EntityView> Managers { get; set; }

        public List<RegionDE> Regions { get; set; }
        public List<CityDE> Cities { get; set; }
        public List<AreaDE> Areas { get; set; }

        public override bool IsValid { get; set; }

        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Manager.EntityName))
            {
                isValid = false;
                base.FieldId = "managerName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Manager Name");
            }
            else if (Contact.Phone1.IsNullOrEmpty() && Contact.Mobile1.IsNullOrEmpty() && Contact.Mobile2.IsNullOrEmpty())
            {
                isValid = false;
                base.FieldId = "phone1";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Phone (Off)");
            }
            else if (Address.AddressLine1.IsNullOrEmpty())
                {
                    isValid = false;
                    base.FieldId = "AddressessLine1";
                    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Address Line1");
                }
                else if (Address.RegionCode.IsNullOrEmpty())
                {
                    isValid = false;
                    base.FieldId = "regionCode";
                    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Region");
                }
                else if (Address.CityCode.IsNullOrEmpty())
                {
                    isValid = false;
                    base.FieldId = "cityCode";
                    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "City");
                }
                else if (Address.AreaCode.IsNullOrEmpty())
                {
                    isValid = false;
                    base.FieldId = "areaCode";
                    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Area");
                }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }
    public class DoctorViewModel : BaseViewModel
    {
        public DoctorViewModel()
        {
            Address = new AddressDE();
            Contact = new ContactDE();
            Doctor = new EntityDE(EntityTypes.DOC);
            Managers = new List<EntityView>();
            Doctors = new List<EntityView>();
            Cities = new List<CityDE>();
            Areas = new List<AreaDE>();
            Regions = new List<RegionDE>();
        }

        public AddressDE Address { get; set; }
        public ContactDE Contact { get; set; }
        public EntityDE Doctor { get; set; }
        public List<EntityView> Managers { get; set; }
        public List<EntityView> Doctors { get; set; }

        public List<RegionDE> Regions { get; set; }
        public List<CityDE> Cities { get; set; }
        public List<AreaDE> Areas { get; set; }

        public override bool IsValid { get; set; }

        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Doctor.EntityName))
            {
                isValid = false;
                base.FieldId = "doctorName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Doctor Name");
            }
            else if (Contact.Phone1.IsNullOrEmpty() && Contact.Mobile1.IsNullOrEmpty() && Contact.Mobile2.IsNullOrEmpty())
            {
                isValid = false;
                base.FieldId = "phone1";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Phone (Off)");
            }
            else if (Address.AddressLine1.IsNullOrEmpty())
            {
                isValid = false;
                base.FieldId = "addressLine1";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Address Line1");
            }
            //else if (Doctor.ParentEntityID == decimal.Zero)
            //{
            //    isValid = false;
            //    base.FieldId = "managerId";
            //    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Manager");
            //}
            //else if (Address.RegionCode.IsNullOrEmpty())
            //{
            //    isValid = false;
            //    base.FieldId = "regionCode";
            //    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Region");
            //}
            //else if (Address.CityCode.IsNullOrEmpty())
            //{
            //    isValid = false;
            //    base.FieldId = "cityCode";
            //    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "City");
            //}
            //else if (Address.AreaCode.IsNullOrEmpty())
            //{
            //    isValid = false;
            //    base.FieldId = "areaCode";
            //    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Area");
            //}
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
        }
    }
    public class ExpenseGroupViewModel : BaseViewModel
    {
        public ExpenseGroupViewModel()
        {
            ExpenseGroup = new ExpenseGroupDE();
            ExpenseGroups = new List<ExpenseGroupDE>();
        }
        public ExpenseGroupDE ExpenseGroup { get; set; }
        public List<ExpenseGroupDE> ExpenseGroups { get; set; }

        public override bool IsValid { get; set; }

        public override bool Validate()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(ExpenseGroup.GroupCode))
            {
                isValid = false;
                base.FieldId = "groupCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Group Code");
            }
            else if (string.IsNullOrEmpty(ExpenseGroup.GroupName))
            {
                isValid = false;
                base.FieldId = "groupName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Group Name");
            }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }
    public class ExpenseHeadViewModel : BaseViewModel
    {
        public ExpenseHeadViewModel()
        {
            ExpenseHead = new ExpenseHeadDE();
            ExpenseHeads = new List<ExpenseHeadVw>();
            ExpenseGroups = new List<ExpenseGroupDE>();
        }

        public ExpenseHeadDE ExpenseHead { get; set; }
        public List<ExpenseHeadVw> ExpenseHeads { get; set; }
        public List<ExpenseGroupDE> ExpenseGroups { get; set; }

        public override bool IsValid { get; set; }

        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(ExpenseHead.ExpenseGroupCode))
            {
                isValid = false;
                base.FieldId = "groupCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Expense Group");
            }
            else if (string.IsNullOrEmpty(ExpenseHead.ExpenseHeadCode))
            {
                isValid = false;
                base.FieldId = "headCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "ExpenseHead Code");
            }
            else if (string.IsNullOrEmpty(ExpenseHead.ExpenseDescription))
            {
                isValid = false;
                base.FieldId = "expenseDesc";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Expense Description");
            }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }
    public class EmployeeCoreViewModel : BaseViewModel
    {
        public EmployeeCoreViewModel()
        {
            EmployeeCore = new EmployeeCoreBE();
            //EmployeeCores = new List<EmployeeCoreView>();
            EmployeeCores = new List<EmployeeCoreBE>();
        }
        public EmployeeCoreBE EmployeeCore { get; set; }
        //public List<EmployeeCoreView> EmployeeCores { get; set; }
        public List<EmployeeCoreBE> EmployeeCores { get; set; }
        public override bool IsValid { get; set; }
        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(EmployeeCore.EntityName))
            {
                isValid = false;
                base.FieldId = "employeeName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Employee Name");
            }
            else if (string.IsNullOrEmpty(EmployeeCore.Phone))
            {
                isValid = false;
                base.FieldId = "phone";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Phone");
            }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }

    public class BankViewModel : BaseViewModel
    {
        public BankViewModel()
        {
            Bank = new BankBE();
            Banks = new List<BankBE>();
        }
        public BankBE Bank { get; set; }
        public List<BankBE> Banks { get; set; }
        public override bool IsValid { get; set; }
        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Bank.Code))
            {
                isValid = false;
                base.FieldId = "bankCode";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Bank Code");
            }
            else if (string.IsNullOrEmpty(Bank.EntityName))
            {
                isValid = false;
                base.FieldId = "bankName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Bank Name");
            }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }


    #region AlKhair

    public class GiftTypeViewModel : BaseViewModel
    {
        public GiftTypeViewModel()
        {
            GiftType = new GiftTypeBE();
            GiftTypes = new List<GiftTypeView>();
        }
        public GiftTypeBE GiftType { get; set; }
        public List<GiftTypeView> GiftTypes { get; set; }
        public override bool IsValid { get; set; }
        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(GiftType.Code))
            {
                isValid = false;
                base.FieldId = "code";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Code");
            }
            else if (string.IsNullOrEmpty(GiftType.EntityName))
            {
                isValid = false;
                base.FieldId = "giftTypeName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Gift Type Name");
            }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }

    public class DonorViewModel : BaseViewModel
    {
        public DonorViewModel()
        {
            Donor = new DonorBE();
            //Donors = new List<DonorView>();
            Donors = new List<DonorBE>();
        }
        public DonorBE Donor { get; set; }
        //public List<DonorView> Donors { get; set; }
        public List<DonorBE> Donors { get; set; }
        public override bool IsValid { get; set; }
        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Donor.EntityName))
            {
                isValid = false;
                base.FieldId = "donorName";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Donor Name");
            }
            else if (string.IsNullOrEmpty(Donor.Phone))
            {
                isValid = false;
                base.FieldId = "phone";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Phone");
            }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }

    public class VoucherViewModel : BaseViewModel
    {
        public VoucherViewModel()
        {
            Voucher = new VoucherBE();
            Donors = new List<DonorBE>();
            GiftTypes = new List<GiftTypeView>();
            Vouchers = new List<VoucherBE>();
            Names = new List<string>(){
            "Imran"
            ,"Irfan"
            ,"Jameel"
            ,"Ali"
            ,"Adil"
            };
        }

        public string GiftTypeNo { get; set; }
        public List<string> Names { get; set; }
        public VoucherBE Voucher { get; set; }

        public List<DonorBE> Donors { get; set; }
        public List<GiftTypeView> GiftTypes { get; set; }
        public List<VoucherBE> Vouchers { get; set; }
        public override bool IsValid { get; set; }
        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Voucher.VchNo))
            {
                isValid = false;
                base.FieldId = "vchNo";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Voucher No");
            }
            else if (Voucher.VchDate== new DateTime(1900,1,1))
            {
                isValid = false;
                base.FieldId = "vchDate";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Voucher Date");
            }
            else if (Voucher.ReceivedFrom == -1)
            {
                isValid = false;
                base.FieldId = "receivedFrom";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Received From");
            }
            else if (Voucher.GiftTypeId == -1)
            {
                isValid = false;
                base.FieldId = "giftType";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Gift Type");
            }
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }
    }


    #endregion
}