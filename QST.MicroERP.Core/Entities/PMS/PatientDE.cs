using QST.MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class PatientDE : BaseDomain
    {
        public string? PatientName { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public int? GenderId { get; set; }
        public string? ContactNo { get; set; }
        public string? HouseNo { get; set; }
        public string? Address { get; set; }
        public string? Remarks { get; set; }
        public int? CityId { get; set; }
        public int? AreaId { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public string? Age { get; set; }
        public int? CountryId { get; set; }
        public string? Country { get; set; }
        public List<PtExtraFieldsDataDE> ptFData { get; set; }
        public PatientDE()
        {
            ptFData = new List<PtExtraFieldsDataDE> ();
        }
    }
}
