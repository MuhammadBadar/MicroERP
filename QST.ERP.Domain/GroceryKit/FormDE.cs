using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.BDM;

namespace QST.ERP.Domain.GroceryKit
{
    public class FormDE : Person
    {
        public FormDE()
        {
            FormNo = string.Empty;
            FormType = string.Empty;
            HouseStatus = string.Empty;
            //MemberStatus = string.Empty;
            Remarks = string.Empty;
            DataCollectedBySignatures = string.Empty;

            //RegDate = DateTime.Now;
            FamilyMembers = new List<Person>();
            FamilyMembers.Add(new Person
            {
                 EntityName = "Imran Khan"
                 //, Relation = "Son"
                 , IsActive = true, Age = 13, 
            });
            FamilyMembers.Add(new Person
            {
                EntityName = "Sohail Ahmad",
                //Relation = "Son",
                IsActive = true,
                Age = 43,
            });
        }

        public string FormNo { get; set; }
        public string FormType { get; set; }
        public string FatherOrHusbandName { get; set; }
        public int MedicalProblemId { get; set; }
        public int MemberStatusId { get; set; }
        public int OccupationId { get; set; }

        public string HouseStatus { get; set; }
        public bool ZakatAcceptable { get; set; }
        public int FamilySize { get; set; }
        public decimal Salary { get; set; }
        public decimal Donations { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal ShortFallInCash { get; set; }
        public string Remarks { get; set; }
        public bool IsDeserving { get; set; }
        public int ReferredBy { get; set; }
        public int DataCollectedBy { get; set; }
        public string DataCollectedBySignatures { get; set; }
        public bool ApprovalByDataCollectedPerson { get; set; }
        public int AuthorizedBy { get; set; }
        public bool ApprovalByDataAuthorizedPerson { get; set; }

        //public DateTime RegDate { get; set; }
        //public Person Person { get; set; }
        public List<Person> FamilyMembers { get; set; }

//        SiteCode	varchar(3)	Unchecked
//EntityTypeCode	varchar(3)	Unchecked
//FormId	int	Unchecked
//FormNo	varchar(50)	Unchecked
//FormType	varchar(3)	Checked
//FatherOrHusbandName	varchar(50)	Checked
//MedicalProblemId	int	Checked
//MemberStatusId	int	Checked
//OccupationId	int	Checked
//HouseStatus	varchar(1)	Checked
//ZakatAcceptable	bit	Checked
//FamilySize	int	Checked
//Salary	decimal(18, 0)	Checked
//Donations	decimal(18, 0)	Checked
//OtherIncome	decimal(18, 0)	Checked
//ShortfallInCash	decimal(18, 0)	Checked
//Remarks	varchar(50)	Checked
//IsDeserving	bit	Checked
//ReferredBy	int	Checked
//DataCollectedBy	int	Checked
//DataCollectedBySign	varchar(50)	Checked
//ApprovalByDataCollectedPerson	bit	Checked
//AuthorizedBy	int	Checked
//ApprovalByDataAuthorizedPerson	bit	Checked
    }
}
