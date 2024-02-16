using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL.IDAL
{
    public interface IApptDAL
    {
        List<AppointmentDE> SearchNextAppt (  string whereClause ,int ClientId,int DoctorId, MySqlCommand? cmd );
    }
}
