using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace QST.ERP.DAL.UoWorks
{
    public class BDMDataUoWork : IUnitOfWork, IDisposable
    {
        private BDMDataContext _context;

        public BDMDataUoWork(string conStr)
        {
            this._context = new BDMDataContext(conStr);
        }

        public DbContext Context
        {
            get { return _context; }
            set { _context = (BDMDataContext)value; }
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
