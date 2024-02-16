using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class SupplierDE : BaseDomain
    {
        #region Properties
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public int AccId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public double DiscRate { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Account { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int CustomerId { get; set; }
        public bool IsCustomer { get; set; }
        #endregion
        #region Constructors
        public SupplierDE()
        {
            Phone = null;
            City = null;
            Address = null;
        }
        #endregion
    }
}