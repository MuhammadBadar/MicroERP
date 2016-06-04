using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.DAL.Maps
{
    public abstract class BaseTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected BaseTypeConfiguration()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }

        public string SchemaName { get { return "ERP"; } }
    }
}
