using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace QST.ERP.DAL.UoWorks
{      
    public class GLDataUoWork : IUnitOfWork, IDisposable
    {
        private PQSDataContext _context;
        public GLDataUoWork(string conStr)
        {
            this._context = new PQSDataContext(conStr);
        }

        public DbContext Context
        {
            get { return _context; }
            set { _context = (PQSDataContext)value; }
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public bool LazyLoadingEnabled
        {
            get { return this.Context.Configuration.LazyLoadingEnabled; }
            set { this.Context.Configuration.LazyLoadingEnabled = value; }
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
