using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Models
{
    public abstract class BaseSearchCritia
    {
        public BaseSearchCritia() 
        {
            IsActive = true;
        }
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsActive { get; set; }
    }
}
