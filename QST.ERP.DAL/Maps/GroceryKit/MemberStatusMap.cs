using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using QST.ERP.Domain.GroceryKit;

namespace QST.ERP.DAL.Maps.GroceryKit
{
    public class MemberStatusMap : BaseTypeConfiguration<MemberStatusBE>
    {
        public MemberStatusMap()
        {
            this.ToTable("MemberStatus");

            this.HasKey(m => new { m.ID });
            this.Ignore(m => m.SiteCode);
            this.Ignore(m => m.IsActive);

            this.Property(m => m.MemberStatus);
        }
    }
}
