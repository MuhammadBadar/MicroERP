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
    public class DepartmentMap : BaseTypeConfiguration<DepartmentDE> // EntityTypeConfiguration<DepartmentDE>
    {
        public DepartmentMap()
        {
            //Map the Table
            this.ToTable("PQS_Department", SchemaName);

            //Primary key
            this.HasKey(m => m.ID);

            //Relationship
            //this.HasMany<UserDE>(s => s.Users)
            //        .WithRequired(m => m.DepartmentDE)
            //        .HasForeignKey(s => s.DepartmentId)
            //        .WillCascadeOnDelete(true);

            //Validation and Mapping column
            this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired().HasColumnName("ID");
            this.Property(t => t.DepartmentName).HasMaxLength(200).IsRequired().HasColumnName("Department_Name");
            //this.Property(t => t.CreatedBy).IsRequired().HasColumnName("CreatedBy_ID");
            //this.Property(t => t.CreatedOn).IsRequired().HasColumnName("CreatedOn_DT");
            //this.Property(t => t.ModifiedBy).IsOptional().HasColumnName("ModifiedBy_ID");
            //this.Property(t => t.ModifiedOn).IsOptional().HasColumnName("ModifiedOn_DT");
            this.Property(t => t.IsActive).IsRequired().HasColumnName("IsActive_BIT");
        }
    }
}
