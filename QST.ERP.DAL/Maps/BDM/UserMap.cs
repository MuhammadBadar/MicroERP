using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using QST.ERP.Domain;

namespace QST.ERP.DAL.Maps.BDM
{
    public class UserMap : BaseTypeConfiguration<UserDE>
    {
        public UserMap()
        {
            //Map the Table
            this.ToTable("PQS_User", SchemaName);

            //Primary key
            this.HasKey(t => t.ID);

            //Validation and Mapping Column
            this.Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");
            this.Property(t => t.DepartmentId).IsOptional().HasColumnName("Department_ID");
            //this.Property(t => t.CreatedBy).IsRequired().HasColumnName("CreatedBy_ID");
            //this.Property(t => t.ModifiedBy).IsOptional().HasColumnName("ModifiedBy_ID");
            this.Property(t => t.FirstName).HasMaxLength(50).IsRequired().HasColumnName("First_Name");
            this.Property(t => t.LastName).HasMaxLength(50).IsOptional().HasColumnName("Last_Name");
            //this.Property(t => t.CreatedOn).IsRequired().HasColumnName("CreatedOn_DT");
            //this.Property(t => t.ModifiedOn).IsOptional().HasColumnName("ModifiedOn_DT");
            this.Property(t => t.IsActive).IsRequired().HasColumnName("IsActive_BIT");

        }
    }
}
