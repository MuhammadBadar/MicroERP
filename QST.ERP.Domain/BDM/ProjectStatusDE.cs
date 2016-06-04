using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSA.Avella.PQS.Domain
{
    public class ProjectStatusDE:BaseDomain
    {
        public string StatusDescription { get; set; }
    }
}
