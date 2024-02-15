using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities
{
    public class UOMConversionDE:BaseDomain
    {
        #region Properties
        public int UOMId { get; set; }
        public int ConvertedUOMId { get; set; }
        public bool IsBaseUnit { get; set; }
        public int Qty { get; set; }
        public double Multiplier { get; set; }
        public string? DisplayUOM { get; set; }
        public string? UOM { get; set; }
        public string? ConvertedUOM { get; set; }
        #endregion
    }
}
