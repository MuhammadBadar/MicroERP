using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QST.ERP.Domain
{
    
    public class UserDE : BaseDomain
    {

        public decimal DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        //public virtual DepartmentDE DepartmentDE { get; set; }
        //public virtual RoleDE RoleDE { get; set; }
    }
}
