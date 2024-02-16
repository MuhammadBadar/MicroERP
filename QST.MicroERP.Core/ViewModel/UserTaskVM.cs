using QST.MicroERP.Core.Entities.TMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.ViewModel
{
    public class UserTaskVM:BaseViewModel
    {
        public string? User { get; set; }
        public string? Module { get; set; }
        public string? Status { get; set; }
        public int StatusId { get; set; }
        public string? Priority { get; set; }
        public int PriorityId { get; set; }
        public string? UserId { get; set; }
        public int ModuleId { get; set; }
        public string? Title { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? DirectSupervisorPhoneNumber { get; set; }
        public float SP { get; set; }
        public string? Description { get; set; }
        public string? DirectSupervisorId { get; set; }
        public string? UserMail { get; set; }
        public string? DirectSupervisorEmail { get; set; }
        public string? DirectSupervisorName { get; set; }
        public List<AttachmentsDE> Attachments { get; set; }
        public int ClaimPercent { get; set; }
        public int? ClaimId { get; set; }
        public float RemainingSPs { get; set; }
        public string? ActualSP { get; set; }
        public int UserTaskId { get; set; }
        public int ApprovedClaimId { get; set; }
        public int ApprovedClaim { get; set; }
        public DateTime? Date { get; set; }
        public string? TaskScore { get; set; }
        public string? TaskComPer { get; set; }
        public int LastClaimId { get; set; }
        public int LastClaim { get; set; }
        public float? WorkTime { get; set; }
        public UserTaskVM()
        {
            this.Attachments = new List<AttachmentsDE>();
        }
    }
}
