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
    public class ContactMap : BaseTypeConfiguration<ContactDE>
    {
        public ContactMap()
        {
            //this.ToTable("Contact", SchemaName);
            this.ToTable("Contact");
            this.HasKey(m => new { m.SiteCode, m.ID });
            this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");

            //this.Property(m => m.SiteCode);
            this.Property(m => m.EntityID);
            this.Property(m => m.Phone1);
            this.Property(m => m.Phone2);
            this.Property(m => m.Mobile1);
            this.Property(m => m.Mobile2);

            this.Property(m => m.IsActive).IsRequired();
        }
    }
}
