using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QST.ERP.Domain
{
    public class DepartmentDE : BaseDomain
    {
        #region Construtor
        
        public DepartmentDE()
        {
            //Users = new List<UserDE>();
        }

        #endregion

        public string DepartmentName { get; set; }
       
        //public virtual IList<UserDE> Users { get; set; }
    }
}
