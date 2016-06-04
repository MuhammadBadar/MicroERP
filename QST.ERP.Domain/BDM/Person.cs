using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    public class Person : EntityDE
    {
        public Person()
        {
            //Gender = "F";
            //Occupation = string.Empty;
            Picture = string.Empty;
            Gender = string.Empty;
            Signature = string.Empty;
            ThumbImpression = string.Empty;
            NationalId = string.Empty;

            //ContactNos = string.Empty;
            //Address = string.Empty;
            //CNIC = string.Empty;
            //MedicalProblems = string.Empty;
            //Relation = string.Empty;
            DOB = new DateTime(1900, 1, 1);
            Age = 0;
        }

        //public string EntityTypeCode { get; set; }
        //public string EntityTypeCode { get; set; }
        public int ParentId { get; set; }
        public int ParentRelationId { get; set; }
        public int MeritalStatusId { get; set; }
        //public string Occupation { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        public string Picture { get; set; }
        public string Gender { get; set; }
        public string Signature { get; set; }
        public string ThumbImpression { get; set; }
        public string NationalId { get; set; }

        public string Phone { get; set; }
        //public string ContactNos { get; set; }
        //public string Address { get; set; }
        //public string MedicalProblems { get; set; }
        //public string Relation { get; set; }

    }

    public class PersonView : BaseDomain
    {
        public string EntityTypeCode { get; set; }
        public string EntityName { get; set; }
        public string Phone { get; set; }
    }
}
