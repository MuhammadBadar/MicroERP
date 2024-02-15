using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class DocExtrasDE:BaseDomain
    {
        #region Properties
        public int DocExtraId { get; set; }
        public int DocExtraTypeId { get; set; }
        public int IncDecTypeId { get; set; }
        public int FormulaId { get; set; }
        public double Value { get; set; }
        public string? DocExtra { get; set; }
        public string? DocExtraType { get; set; }
        public string? Formula { get; set; }
        public string? IncDecType { get; set; }
        #endregion
    }
}
