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
    public class AreaMap : BaseTypeConfiguration<AreaDE>
    {
        public AreaMap()
        {
            //Map the Table
            //this.ToTable("Area", SchemaName);
            this.ToTable("Area");

            //Primary key
            this.HasKey(m => new { m.SiteCode, m.AreaCode });

            //Validation and Mapping Column
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");
            this.Ignore(m => m.ID);
            this.Property(m => m.RegionCode).IsRequired();
            this.Property(m => m.CityCode).IsRequired();
            this.Property(m => m.AreaCode).IsRequired();
            this.Property(m => m.AreaName).IsRequired();
            this.Property(t => t.IsActive).IsRequired();
        }
    }
}
