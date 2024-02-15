using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities.TMS
{
    public class UserTaskDE : BaseDomain
    {
        public string? UserId { get; set; }
        public int TaskId { get; set; }
        public int ApprovedClaimId { get; set; }
        public string? ApprovedClaim { get; set; }
        public int LastClaimId { get; set; }
        public string? LastClaim { get; set; }
        public string? Title { get; set; }
        public string? User { get; set; }
        public string? Parent { get; set; }
        public DateTime Date { get; set; }
        public int ClaimId { get; set; }
        public string? ClaimPercent { get; set; }
        public float Sp { get; set; }
        public string? Comments { get; set; }
        public bool IsDayEnded { get; set; }
        public string? ReviewedBy { get; set; }
        public string? ReviewComments { get; set; }
        public string? Reason { get; set; }
        public int ModuleId { get; set; }
        public string? Module { get; set; }
        public string? ClaimError { get; set; }
        public int? StatusId { get; set; }
        public string? TaskScore { get; set; }
        public string? TaskComPer { get; set; }
        public float? WorkTime { get; set; }
        public string? ClaimWorkTime { get; set; }
        public double? WorkSP { get; set; }
        public string? FinalScore { get; set; }
        public bool IsEarlyFinshed { get; set; }
        public string? ExtraTime { get; set; }
        public bool IsLastExistence { get; set; }
        public string? Priority { get; set; }
        public bool IsOverdue { get; set; }
    }
}
