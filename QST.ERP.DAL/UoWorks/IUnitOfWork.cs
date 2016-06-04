using QST.ERP.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.DAL.UoWorks
{

    public interface IUnitOfWork //<U> where U : IDbContext
    {
        DbContext Context { get; set; }
        void Commit();
        bool LazyLoadingEnabled { get; set; }
    }
}
