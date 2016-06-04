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
    public class VoucherMap : BaseTypeConfiguration<VoucherBE>
    {
        public VoucherMap()
        {
            this.ToTable("Voucher");
            this.HasKey(m => new { m.SiteCode, m.VchNo });
            //this.Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.IdGiftType).IsRequired().HasColumnName("ID");

            this.Ignore(m => m.ID);
            this.Ignore(m => m.Code);

            this.Property(m => m.VchNo);
            this.Property(m => m.GiftTypeId);
            this.Property(m => m.ReceivedFrom);

            this.Property(m => m.VchTypeCode);
            this.Property(m => m.VchMonth);
            this.Property(m => m.VchYear);

            this.Property(m => m.VchKeyId);
            this.Property(m => m.VchDate);
            this.Property(m => m.VchAmount);
            this.Property(m => m.PaymentMode);
            this.Property(m => m.ChequeNo);
            this.Property(m => m.ChequeDate);
            this.Property(m => m.BankId);

            this.Property(m => m.IsActive);
        }
    }
}
