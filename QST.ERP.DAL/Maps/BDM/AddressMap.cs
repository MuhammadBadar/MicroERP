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
    public class AddressMap : BaseTypeConfiguration<AddressDE>
    {
        public AddressMap()
        {
            //this.ToTable("Address", SchemaName);
            this.ToTable("Address");
            this.HasKey(m => new { m.SiteCode, m.ID });
            this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");

            this.Property(m => m.EntityID).IsRequired();
            this.Property(m => m.AddressTypeCode).IsRequired();
            this.Property(m => m.AddressLine1).IsRequired();
            this.Property(m => m.AddressLine2);

            this.Property(m => m.CityCode);
            this.Property(m => m.CityName);

            this.Property(m => m.StateCode);
            this.Property(m => m.StateName);

            this.Property(m => m.CountryCode);
            this.Property(m => m.CountryName);

            this.Property(m => m.IsActive).IsRequired();
        }
    }
}
