﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.SCH
{
    public class ScheduleDayEventDE : BaseDomain
    {

        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int SchId { get; set; }
        public int EventTypeId { get; set; }
        public int LocationId { get; set; }
        public int? SchDayId { get; set; }

        #region View Properties

        public string? EventType { get; set; }
        public double? Sp { get; set; }
        public string? Location { get; set; }
        public string? Day { get; set; }
        // Add SchedulePoints property
        public float SchedulePoints { get; set; }

        #endregion


    }
}
