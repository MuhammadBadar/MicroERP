using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using QST.ERP.Domain.BDM;
using QST.ERP.Domain.GroceryKit;
using QST.ERP.WebApi.Models;
using QST.ERP.Domain;

namespace QST.ERP.ngClient.ViewModels
{
    public class GroceryKitViewModel : BaseViewModel
    {
        public GroceryKitViewModel()
        {
            Form = new FormDE();
            Forms = new List<FormVw>();
            MedicalProblems = new List<MedicalProblemBE>();
            Occupations = new List<OccupationBE>();
            MemberStatuses = new List<MemberStatusBE>();
        }

        public FormDE Form { get; set; }
        public List<FormVw> Forms { get; set; }
        public List<MedicalProblemBE> MedicalProblems { get; set; }
        public List<OccupationBE> Occupations { get; set; }
        public List<MemberStatusBE> MemberStatuses { get; set; }

        public override bool IsValid { get; set; }

        public override bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Form.FormNo))
            {
                isValid = false;
                base.FieldId = "formNo";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Form No");
            }
            else if (string.IsNullOrEmpty(Form.HouseStatus))
            {
                isValid = false;
                base.FieldId = "houseStatus";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "House Status");
            }
            //else if(DistributionPoint)
            //{}
            else if (string.IsNullOrEmpty(Form.EntityName))
            {
                isValid = false;
                base.FieldId = "name";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Name");
            }
            else if (string.IsNullOrEmpty(Form.Gender))
            {
                isValid = false;
                base.FieldId = "gender";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Gender");
            }
            else if (!Form.DOB.HasValue)
            {
                isValid = false;
                base.FieldId = "dob";
                base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "DOB");
            }
            //else if (string.IsNullOrEmpty(Form.MemberStatusId))
            //{
            //    isValid = false;
            //    base.FieldId = "memberStatus";
            //    base.Message = string.Format(AppConstants.VALIDATION_REQUIRED_FIELD, "Member Status");
            //}
            else
            {
                base.FieldId = string.Empty;
                base.Message = string.Empty;
            }
            return isValid;
            //return base.Validate();
        }

        #region Incomes

        public int Salary { get; set; }
        public int Donation { get; set; }
        public int OtherIncome { get; set; }
        public int TotalIncome 
        {
            get { return Salary + Donation + OtherIncome; } 
        }

        #endregion

        #region Costs

        public int FoodCost { get; set; }
        public int HouseRent { get; set; }
        public int SchoolCost { get; set; }
        public int UtilitiesCost { get; set; }
        public int MedicalCost { get; set; }
        public int OtherCost { get; set; }
        public int TotalCost
        {
            get { return FoodCost + HouseRent + SchoolCost + UtilitiesCost + MedicalCost + OtherCost; }
        }

        public int ShortFallInCash 
        {
            get { return TotalIncome - TotalCost; } 
        }

        #endregion
    }
}