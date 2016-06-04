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
    public class EntityViewMap : BaseTypeConfiguration<EntityView>
    {
        public EntityViewMap()
        {
            //this.ToTable("EntityVw", SchemaName);
            this.ToTable("vwEntities");
            this.HasKey(m => new { m.SiteCode, m.ID, m.EntityTypeCode });
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");

            //this.Property(m => m.ParentEntityID);
            //this.Property(m => m.EntityNameParent);
            
        //    public string EntityTypeCode { get; set; }
        //public string EntityTypeDesc { get; set; }
        //public string EntityName { get; set; }

            this.Property(m => m.EntityTypeCode);
            this.Property(m => m.EntityTypeDesc);
            this.Property(m => m.EntityName);

            this.Property(m => m.IsActive);

            //this.Property(m => m.AddressLine1).IsRequired();
            //this.Property(m => m.AddressLine2);

            //this.Property(m => m.AreaCode);
            //this.Property(m => m.AreaName);

            //this.Property(m => m.CityCode);
            //this.Property(m => m.CityName);

            //this.Property(m => m.RegionCode);
            //this.Property(m => m.RegionName);

            //this.Property(m => m.StateCode);
            //this.Property(m => m.StateName);

            //this.Property(m => m.CountryCode);
            //this.Property(m => m.CountryName);

            //// Contact Fields
            //this.Property(m => m.Phone1);
            //this.Property(m => m.Phone2);
            //this.Property(m => m.Mobile1);
            //this.Property(m => m.Mobile2);
        }
    }
}
