using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class StaffDE : BaseDomain
    {
        public string? UserId { get; set; }
        public string? User { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public int? GenderId { get; set; }
        public string? ContactNo { get; set; }
        public string? HouseNo { get; set; }
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public int? AreaId { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public int? CountryId { get; set; }
        public string? Country { get; set; }
    }
}
