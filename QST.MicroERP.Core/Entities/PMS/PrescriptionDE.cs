using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities
{
    public class PrescriptionDE:BaseDomain
    {
        #region Properties
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public string? PatientEmail { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public int? TokenNo { get; set; }
        public float? Amount { get; set; }
        public string? Remarks { get; set; }
        public int? NextVisitNo { get; set; }
        public int? BPStatusId { get; set; }
        public bool IsSugarPatient { get; set; }
        public string? Temperature { get; set; }
        public string? BP { get; set; }
        public string? Weight { get; set; }
        public string? MedDetRemarks { get; set; }
        public string? Patient { get; set; }
        public string? Doctor { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public string? Comments { get; set; }
        public string? Precautions { get; set; }
        public string? PrecautionIds { get; set; }
        public List<int>? Precauts { get; set; }
        public string? PatientCity { get; set; }
        public string? PatientContact { get; set; }
        public string? DoctorContact { get; set; }
        public string? DoctorSpecialization { get; set; }
        public DateTime PatientDOB { get; set; }
        public string? PatientAge { get; set; }
        public List<RxMedicineDE> RxMedicines { get; set; }
        public List<RxMedExtraFieldsDataDE> rxmefData { get; set; }
        public List<PatientReportDE> Reports { get; set; }
        #endregion
        #region Constructor
        public PrescriptionDE ( )
        {
            Reports = new List<PatientReportDE> ();
            RxMedicines = new List<RxMedicineDE> ();
            rxmefData = new List<RxMedExtraFieldsDataDE> ();
        }
        #endregion
    }
}
