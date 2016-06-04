using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.BDM;

namespace QST.ERP.Domain.AlKhair
{
    public class GiftTypeView : EntityView
    {
        public bool IsCondition { get; set; }
        public decimal Price { get; set; }
    }
}
