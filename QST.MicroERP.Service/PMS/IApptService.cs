using QST.MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Service.IServices
{
    public interface IApptService
    {
        List<AppointmentDE> SearchAdjacentAppts ( AppointmentDE entity );
        string ApptMinTime ( AppointmentDE entity );
        int GetNextTokenNo ( AppointmentDE entity );
    }
}
