using QST.MicroERP.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.SCH
{
    public class SchLineDE : BaseDomain
    {


        #region Class Properties        
        public int? DayId { get; set; }
        public int SchId { get; set; }

        #endregion

        public string? Day { get; set; }


    }
}
