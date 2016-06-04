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
    public class RegionMap : BaseTypeConfiguration<RegionDE>
    {
        public RegionMap()
        {
            //Map the Table
            //this.ToTable("Region", SchemaName);
            this.ToTable("Region");

            //Primary key
            this.HasKey(m => new { m.SiteCode, m.RegionCode });

            //Validation and Mapping Column
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");
            this.Ignore(m => m.ID);
            this.Property(m => m.SiteCode).IsRequired();
            this.Property(m => m.RegionCode).IsRequired();
            this.Property(m => m.RegionName).IsRequired();
            this.Property(m => m.IsActive).IsRequired();
        }
    }
}
