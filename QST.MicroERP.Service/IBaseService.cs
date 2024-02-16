using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Service
{
    public interface IBaseService<T>
    {
        List<T> SearchData (T entity );
        bool ManageData (T entity );
    }
}
