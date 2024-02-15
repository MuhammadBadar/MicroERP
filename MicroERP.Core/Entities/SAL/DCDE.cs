using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class DCDE:BaseDomain
    {
        #region Properties
        public int AcId { get; set; }
        public int CustId { get; set; }
        public DateTime Date { get; set; }
        public string? InvNo { get; set; }
        public string? Customer { get; set; }
        public string? AcName { get; set; }
        public List<DCDetailDE> DCDetails { get; set; }
        #endregion
        #region Constructor
        public DCDE()
        {
            DCDetails = new List<DCDetailDE>();
        }
        #endregion
    }
}
