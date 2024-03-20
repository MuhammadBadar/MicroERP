using QST.MicroERP.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QST.MicroERP.Core.Entities.TMS
{
    public class TaskDE : BaseDomain
    {
        public TaskDE ( )
        {
            this.Attachments = new List<AttachmentsDE> ();
            TaskActivities = new List<UserTaskDE> ();
        }
        public string? UserId { get; set; }
        public int ModuleId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public string? Title { get; set; }
        public double SP { get; set; }
        public string SPStr
        {
            get { return SP.ToHHMM (); }
        }
        public string? DirectSupervisorId { get; set; }
        public string? Description { get; set; }
        public List<AttachmentsDE> Attachments { get; set; }
        public int UserTaskId { get; set; }
        public string? User { get; set; }
        public string? Module { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? UserMail { get; set; }
        public int ClaimPercent { get; set; }
        public int? ClaimId { get; set; }
        public double RemainingSPs { get; set; }
        public int ApprovedClaimId { get; set; }
        public int ApprovedClaim { get; set; }
        public string? Reason { get; set; }
        public DateTime? Date { get; set; }
        public float? WorkTime { get; set; }
        public float? ExtraTime { get; set; }
        public bool IsOverdue { get; set; }
        public bool IsEarlyFinshed { get; set; }
        public float? TotalWorkedTime { get; set; }
        public List<UserTaskDE> TaskActivities { get; set; }
    }

}
