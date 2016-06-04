using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    [NotMapped]
    public class EmployeeCoreBE : Person
    {
        public EmployeeCoreBE()
        {
            base.EntityTypeCode = EntityTypeCodes.EMP.ToString();
        }
    }

    [NotMapped]
    public class EmployeeCoreView : PersonView
    {
        public EmployeeCoreView()
        {
            base.EntityTypeCode = EntityTypeCodes.EMP.ToString();
        }
    }
}
