using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.DAL.UoWorks;

namespace QST.ERP.Services
{
    public abstract class Service
    {
        public virtual void InitializeService(IUnitOfWork uow) { }
    }
}
