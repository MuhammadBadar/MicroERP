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
    public class PersonViewMap : BaseTypeConfiguration<PersonView>
    {
        public PersonViewMap()
        {
            //Map the Table
            //this.ToTable("City", SchemaName);
            this.ToTable("vwPersons");

            //Primary key
            this.HasKey(m => new { m.SiteCode, m.ID, m.EntityTypeCode });

            //Validation and Mapping Column
            //this.Ignore(m => m.ID);
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");
            this.Property(m => m.SiteCode);
            this.Property(m => m.EntityTypeCode);

            this.Property(m => m.EntityName).HasColumnName("EntityName");
            this.Property(m => m.Phone).HasColumnName("Phone");
            this.Property(m => m.IsActive);
        }
    }
}
