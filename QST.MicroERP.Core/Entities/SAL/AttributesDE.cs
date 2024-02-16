using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class AttributesDE:BaseDomain
    {
        #region Properties
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ProductAttribDE> ProductAttribs { get; set; }
        #endregion
        public AttributesDE()
        {
            this.ProductAttribs = new List<ProductAttribDE>();
        }
    }
}
