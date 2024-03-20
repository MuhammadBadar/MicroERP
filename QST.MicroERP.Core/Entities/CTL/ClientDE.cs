using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class ClientDE : BaseDomain
    {
        public ClientDE()
        {
            ModuleIdList = new List<int> ();
        }
        #region Properties
        public string? ClientName { get; set; }
        public string? ModuleIds { get; set; }
        public string? Modules { get; set; }
        public List<int> ModuleIdList { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public string? City { get; set; }
        public string? UserId { get; set; }
        public string? User { get; set; }
        public string? Email { get; set; }
        public int? CountryId { get; set; }
        public string? Country { get; set; }
        public string? Contact { get; set; }
        public string? Owner { get; set; }
        public string? RelevantPerson { get; set; }
        #endregion
    }
}
