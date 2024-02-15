using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroERP.Core.Entities.TMS;

namespace MicroERP.Core.Entities.ATT
{
    public class AttendanceDE : BaseDomain
    {
        //
        public AttendanceDE()
        {
            UserTasks = new List<UserTaskDE>();
        }
        public List<UserTaskDE> UserTasks { get; set; }
        public string? UserId { get; set; }
        public DateTime? DayStartTime { get; set; }
        public DateTime? DayEndTime { get; set; }
        public int? SchDayId { get; set; }
        public int? SchId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        #region View Properties
        public string? User { get; set; }
        public string? DayStartandEnd { get; set; }
        public string? InandOutTime { get; set; }
        public string? SchTime { get; set; }
        public string? Late { get; set; }
        public string? DueSPs { get; set; }
        public string? WorkedTime { get; set; }
        public string? FinalScore { get; set; }
        public string? MissingTime { get; set; }
        public string? ExtraTime { get; set; }
        public string? ClaimedSPs { get; set; }
        public string? ApprovedClaim { get; set; }
        public string? ClaimPer { get; set; }
        public string? SPsGap { get; set; }
        public string? ClaimErrorPer { get; set; }
        public string? Targets { get; set; }
        public string? DayEndStatus { get; set; }
        public string? Day { get; set; }

        #endregion
    }
}
