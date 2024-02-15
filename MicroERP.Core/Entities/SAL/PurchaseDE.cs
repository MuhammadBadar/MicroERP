using MicroERP.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class PurchaseDE : BaseDomain
    {
        #region Class Properties
        public string? InvNo { get; set; } 
        public DateTime? Date { get; set; } 
        public int SupplierId { get; set; }
        public int AcId{ get; set;}
        public bool IsPosted { get; set; }
        public int StatusId { get; set; }
        public double? Gross { get; set; }
        public double? Discount { get; set; }
        public double? Gst { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public string? Description { get; set;}
        public string? Supplier { get; set; }
        public string? AccName { get; set; }
        public string? Status { get; set; }
        public List <PurchaseLineDE> PurchaseLines { get; set; }

        #endregion
        #region Contructor
        public PurchaseDE()
        {
            PurchaseLines = new List<PurchaseLineDE>();
        }
        #endregion
    }
}
