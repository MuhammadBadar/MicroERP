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
    public class CityVWMap : BaseTypeConfiguration<CityVW>
    {
        public CityVWMap()
        {
            //Map the Table
            //this.ToTable("CityVW", SchemaName);
            this.ToTable("vwCities");

            //Primary key
            this.HasKey(m => new { m.SiteCode, m.CityCode });

            this.Ignore(m => m.ID);

            //Validation and Mapping Column
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");
            this.Property(m => m.SiteCode);
            //this.Property(m => m.RegionCode);
            this.Property(m => m.RegionName);
            this.Property(m => m.CityCode);
            this.Property(m => m.CityName);
            this.Property(m => m.IsActive);
        }
    }
}
