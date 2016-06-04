using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Reflection;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration;
using QST.ERP.DAL.Maps;

namespace QST.ERP.DAL
{
    public class GroceryKitDataContext : DbContext, IDbContext
    {
        public GroceryKitDataContext(string nameOrConString) : base(nameOrConString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetAssembly(typeof(DAL)).GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace) && type.Namespace == "QST.ERP.DAL.Maps.GroceryKit")
           .Where(type => type.BaseType != null
                          && type.BaseType.IsGenericType
               //&& type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
                          && type.BaseType.GetGenericTypeDefinition() == typeof(BaseTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<T> Set<T>() where T : Domain.BaseDomain
        {
            return base.Set<T>();
        }
    }
}

