using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class AppointmentDE : BaseDomain
    {
        public int? TokenNo { get; set; }
        public int? TokenId { get; set; }
        public int? TokenDate { get; set; }
        public int? PatientId { get; set; }
        public string? PatientName { get; set; }
        public int? DoctorId { get; set; }
        public string? Doctor { get; set; }
        public DateTime Date { get; set; }
        public int? ShortAge { get; set; }
        public string? Time { get; set; }
        public string? Age { get; set; }
        public string? AgeTooltip { get; set; }
        public DateTime DOB { get; set; }
        public int? GenderId { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }
        public bool SearchByDate { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public DateTime? ApptDate { get; set; }
        public int? StatusId { get; set; }
        public string? AdjacentType { get; set; }
    }
}
