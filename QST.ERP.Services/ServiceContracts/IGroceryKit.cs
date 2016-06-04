using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.BDM;
using QST.ERP.Domain.GroceryKit;

namespace QST.ERP.Services
{
    public interface IGroceryKit
    {
        int AddMember(MemberDE mod);
        List<FormDE> GetAllForms();
        List<FormVw> GetViewOfAllForms();
        List<FormVw> GetViewOfForms(FormSearchCriteriaBE criteria);

        List<MemberStatusBE> GetAllMemberStatuses();
        List<MedicalProblemBE> GetAllMedicalProblems();
    }
}
