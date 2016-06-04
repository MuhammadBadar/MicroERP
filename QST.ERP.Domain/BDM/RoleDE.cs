using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QST.ERP.Domain
{
    public class RoleDE : BaseDomain
    {
        public string RoleDescription { get; set; }
    }
}
