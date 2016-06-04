using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.GroceryKit
{
    public class CardSheetLine
    {
        //S.#	Reg. S.#	Form No	Name in English	Name in Urdu	Relation	Relation	Father / Husband Name	Father / Husband Name in urdu	
        //CNIC No.	Family Members	Flour	Package	Dist. Id	City	Dist. Point	 
        
        public string SrNo { get; set; }
        public string RegSrNo { get; set; }
        public string FormNo { get; set; }
        public string Name { get; set; }
        public string NameInUrdu { get; set; }
        public string Relation { get; set; }
        public string RelationInUrdu { get; set; }
        public string FatherOrHusbandName { get; set; }
        public string FatherOrHusbandNameInUrdu { get; set; }
        public string CNICNo { get; set; }
        public string FamilyMembers { get; set; }
        public string Flour { get; set; }
        public string Package { get; set; }
        public string DistId { get; set; }
        public string City { get; set; }
        public string DistPoint { get; set; }
        public string DistPointInUrdu { get; set; }

        //Alternate 	Active	DOB	Medical Problem	Contact No.	Contact Type	Occupation	
        //Status	Reffered by	Gender	House Status	Name	Relation	Age	Status	Name	Relation	
        //Age	Status	Name	Relation	Age	Status
        public string Picture { get; set; }
        public string Alternate { get; set; }
        public string Active { get; set; }
        public string DOB { get; set; }
        public string MedicalProblem { get; set; }
        public string ContactNo { get; set; }
        public string ContactType { get; set; }
        public string Occupation { get; set; }
        public string Status { get; set; }

        public string ReferredBy { get; set; }
        public string Gender { get; set; }
        public string HouseStatus { get; set; }
        public string FamilyMember1Name { get; set; }
        public string FamilyMember1Relation { get; set; }
        public string FamilyMember1Age { get; set; }
        public string FamilyMember1Status { get; set; }
        public string FamilyMember2Name { get; set; }
        public string FamilyMember2Relation { get; set; }
        public string FamilyMember2Age { get; set; }
        public string FamilyMember2Status { get; set; }
        public string FamilyMember3Name { get; set; }
        public string FamilyMember3Relation { get; set; }
        public string FamilyMember3Age { get; set; }
        public string FamilyMember3Status { get; set; }

        public ExcelSheetImportErrors ExcelSheetError { get; set; }
    }
}
