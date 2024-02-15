using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class CustomerDE:BaseDomain
    {
        #region Properties


        public int AccId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string? Name { get; set; }
        public string? Region { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool? SendEmail { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Account { get; set; }
        public int SupplierId { get; set; }
        public bool IsSupplier { get; set; }
        #endregion
        #region Constructor
        public CustomerDE()
        {
            Name = null;
            Email = null;
            Phone = null;
            City = null;
            City = null;
            Address = null;
        }
        #endregion
    }
}
