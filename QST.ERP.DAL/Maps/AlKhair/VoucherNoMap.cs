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
    public class VoucherNoMap : BaseTypeConfiguration<VoucherNoBE>
    {
        public VoucherNoMap()
        {
            this.ToTable("VoucherNo");
            this.HasKey(m => new { m.UserName });
            
            this.Ignore(m => m.SiteCode);
            this.Ignore(m => m.Code);
            this.Ignore(m => m.ID);
            this.Ignore(m => m.IsActive);

            this.Property(m => m.UserName);
            this.Property(m => m.VoucherNo);
        }
    }
}
