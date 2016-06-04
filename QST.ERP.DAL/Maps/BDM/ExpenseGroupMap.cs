using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using QST.ERP.Domain.BDM;

namespace QST.ERP.DAL.Maps.BDM
{
    public class ExpenseGroupMap : BaseTypeConfiguration<ExpenseGroupDE>
    {
        public ExpenseGroupMap()
        {
            //this.ToTable("ExpenseGroup", SchemaName);
            this.ToTable("ExpenseGroup");
            this.HasKey(m => new { m.SiteCode, m.GroupCode });
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");

            this.Ignore(m => m.ID);

            this.Property(m => m.GroupName).IsRequired();
            this.Property(m => m.IsActive).IsRequired();
        }
    }
}
