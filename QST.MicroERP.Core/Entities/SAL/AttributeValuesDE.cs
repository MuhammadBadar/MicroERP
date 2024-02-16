using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class AttributeValuesDE:BaseDomain
    {
        #region Properties
        public int AttributeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? KeyCode { get; set; }
        public string? Attribute { get; set; }
        #endregion
    }
}
