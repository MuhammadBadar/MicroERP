using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class DCDetailDE:BaseDomain
    {
        #region Properties
        public int DCId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public string? Description { get; set; }
        public string? Product { get; set; }
        #endregion
    }
}
