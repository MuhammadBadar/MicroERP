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
    public class EntityMap : BaseTypeConfiguration<EntityDE>
    {
        public EntityMap()
        {
            //this.ToTable("Entity", SchemaName);
            this.ToTable("Entity");
            this.HasKey(m => new { m.SiteCode, m.ID });
            this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");

            //one-to-many
            //this.HasMany<AddressDE>(s => s.Addresses)
            //            .WithRequired(m => m.EntityDE)
            //            .HasForeignKey(s => new { s.SiteCode, s.EntityID })
            //            .WillCascadeOnDelete(true);

            //this.HasMany<ContactDE>(s => s.Contacts)
            //            .WithRequired(m => m.EntityDE )
            //            .HasForeignKey(s => new { s.SiteCode, s.EntityID })
            //            .WillCascadeOnDelete(true);

            //this.Property(m => m.ParentEntityID).IsOptional();
            this.Property(m => m.Code);
            this.Property(m => m.EntityName);
            this.Property(m => m.EntityTypeCode);

            this.Property(m => m.IsActive).IsRequired();
        }
    }
}
