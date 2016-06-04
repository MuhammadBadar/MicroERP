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
    public class AreaVwMap : BaseTypeConfiguration<AreaVw>
    {
        public AreaVwMap()
        {
            //Map the Table
            //this.ToTable("Area", SchemaName);
            this.ToTable("vwAreas");

            //Primary key
            this.HasKey(m => new { m.SiteCode, m.AreaCode });

            //Validation and Mapping Column
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");
            this.Ignore(m => m.ID);
            
            this.Property(m => m.RegionCode);
            this.Property(m => m.RegionName);
            
            this.Property(m => m.CityCode);
            this.Property(m => m.CityName);
            
            this.Property(m => m.AreaCode);
            this.Property(m => m.AreaName);
            this.Property(t => t.IsActive);
        }
    }
}
