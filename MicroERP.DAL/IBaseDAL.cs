using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL.IDAL
{
    public interface IBaseDAL<T>
    {
        List<T> SearchData ( string whereClause, MySqlCommand? cmd );
        bool ManageData ( T entity , MySqlCommand? cmd );
    }
}
