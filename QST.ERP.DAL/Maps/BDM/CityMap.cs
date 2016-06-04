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
    public class CityMap : BaseTypeConfiguration<CityDE>
    {
        public CityMap()
        {
            //Map the Table
            //this.ToTable("City", SchemaName);
            this.ToTable("City");

            //Primary key
            this.HasKey(m => new { m.SiteCode, m.CityCode });

            //Validation and Mapping Column
            this.Ignore(m => m.ID);
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");
            this.Property(m => m.SiteCode);
            this.Property(m => m.RegionCode);
            this.Property(m => m.CityCode);
            this.Property(m => m.CityName);
            this.Property(m => m.IsActive);
        }
    }
}
