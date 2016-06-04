using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace QST.ERP.DAL.UoWorks
{
    public class GroceryKitDataUoWork : IUnitOfWork, IDisposable
    {
        private GroceryKitDataContext _context;

        public GroceryKitDataUoWork(string conStr)
        {
            this._context = new GroceryKitDataContext(conStr);
        }

        public DbContext Context
        {
            get { return _context; }
            set { _context = (GroceryKitDataContext)value; }
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
