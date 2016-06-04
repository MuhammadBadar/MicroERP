using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using QST.ERP.Domain.BDM;
using QST.ERP.Domain.AlKhair;

namespace QST.ERP.DAL.Maps.BDM
{
    public class GiftTypeViewMap : BaseTypeConfiguration<GiftTypeView>
    {
        public GiftTypeViewMap()
        {
            this.ToTable("vwGiftTypes");
            this.HasKey(m => new { m.SiteCode, m.ID, m.EntityTypeCode });
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.IdGiftType).IsRequired().HasColumnName("ID");

            this.Property(m => m.EntityTypeCode);
            this.Property(m => m.EntityTypeDesc);
            this.Property(m => m.EntityName);

            this.Property(m => m.IsActive);

            this.Property(m => m.IsCondition);
            this.Property(m => m.Price);
        }
    }
}
