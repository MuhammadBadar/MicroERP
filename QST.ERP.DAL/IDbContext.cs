using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using QST.ERP.Domain;

namespace QST.ERP.DAL
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : BaseDomain;
        int SaveChanges();
    }
}
