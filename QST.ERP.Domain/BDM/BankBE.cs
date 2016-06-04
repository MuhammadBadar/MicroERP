using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{

    [NotMapped]
    public class BankBE : EntityDE
    {
        public BankBE()
        {
            base.EntityTypeCode = EntityTypeCodes.BNK.ToString();
        }
    }
}
