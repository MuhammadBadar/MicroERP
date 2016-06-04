using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.GroceryKit
{
    public class FormVw : BaseDomain
    {
        public int ParentEntityID { get; set; }
        public string EntityName { get; set; }
        public string EntityTypeCode { get; set; }
        public int MeritalStatusID { get; set; }
        public string MeritalStatus { get; set; }
        public string Occupation { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        public string Gender { get; set; }
        public string Signature { get; set; }
        public string ThumbImpression { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDeserving { get; set; }
        public string FormNo { get; set; }
        public string FormType { get; set; }
        public string HouseStatus { get; set; }
        public bool ZakatAcceptable { get; set; }
        public int FamilySize { get; set; }
        public decimal Salary { get; set; }
        public decimal Donations { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal ShortFallInCash { get; set; }
        public string Remarks { get; set; }
        public int DataCollectedById { get; set; }
        public string DataCollectedBy { get; set; }
        public string DataCollectedBySignature { get; set; }
        public bool ApprovalByDataCollectedPerson { get; set; }
        public int AuthorizedById { get; set; }
        public string AuthorizedBy { get; set; }
        public bool ApprovalByAuthorizedPerson { get; set; }

    }
}
