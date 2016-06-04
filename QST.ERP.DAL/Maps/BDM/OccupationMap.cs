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
    public class OccupationMap : BaseTypeConfiguration<OccupationBE>
    {
        public OccupationMap()
        {
            //Map the Table
            this.ToTable("Occupation");

            //Primary key
            this.HasKey(m => new { m.ID });

            this.Ignore(m => m.SiteCode);
            this.Ignore(m => m.IsActive);

            this.Property(m => m.Occupation);
        }
    }
}
