using QST.MicroERP.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.SCH
{
    public class ScheduleDayDE : BaseDomain
    {
        public ScheduleDayDE()
        {
            ScheduleDayEvents = new List<ScheduleDayEventDE>();
        }

        #region Class Properties        
        public int? DayId { get; set; }
        public int? SchId { get; set; }
        public string? Location { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? EventType { get; set; }
        public string? Day { get; set; }
        public string? SchDayEvents { get; set; }
        public List<ScheduleDayEventDE> ScheduleDayEvents { get; set; }

        #endregion

    }
}
