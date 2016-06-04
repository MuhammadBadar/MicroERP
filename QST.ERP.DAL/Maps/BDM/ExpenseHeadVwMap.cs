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
    public class ExpenseHeadVwMap : BaseTypeConfiguration<ExpenseHeadVw>
    {
        public ExpenseHeadVwMap()
        {
            //this.ToTable("ExpenseHead", SchemaName);
            this.ToTable("ExpenseHeadVw");
            this.HasKey(m => new { m.SiteCode, m.ExpenseHeadCode });
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");

            this.Ignore(m => m.ID);

            this.Property(m => m.ExpenseGroupCode);
            this.Property(m => m.GroupName);
            this.Property(m => m.ExpenseDescription);
            this.Property(m => m.IsActive);
        }
    }
}
