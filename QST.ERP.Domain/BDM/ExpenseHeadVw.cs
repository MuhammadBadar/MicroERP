using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    public class ExpenseHeadVw : BaseDomain
    {
        public ExpenseHeadVw()
        {
            ExpenseHeadCode = string.Empty;
            ExpenseGroupCode = string.Empty;
            ExpenseDescription = string.Empty;
        }
        public string ExpenseHeadCode { get; set; }
        public string ExpenseGroupCode { get; set; }
        public string GroupName { get; set; }
        public string ExpenseDescription { get; set; }
    }
}
