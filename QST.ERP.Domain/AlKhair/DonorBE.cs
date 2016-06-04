using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.BDM;
using System.ComponentModel.DataAnnotations.Schema;

namespace QST.ERP.Domain.AlKhair
{
    [NotMapped]

    public class DonorBE : Person
    {
    }

    [NotMapped]

    public class DonorView : PersonView { }
}
