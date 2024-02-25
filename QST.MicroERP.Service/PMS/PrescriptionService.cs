using Google.Protobuf.WellKnownTypes;
using KeyAccounting.Core.Extenstions;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.Service.IServices;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Service
{
    public class PrescriptionService
    {
        #region Data Members

        private PrescriptionDAL _rxMedDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        private RxMedExtraFieldsDataDAL _extraFieldsDataDAL;
        private PatientReportDAL _patientRptDAL;
        private readonly IBaseService<AppointmentDE> _apptSvc;
        private  PatientService _patSvc;
        private readonly IBaseService<DoctorDE> _docSvc;

        #endregion
        #region Constructors
        public PrescriptionService ( IBaseService<AppointmentDE> apptSvc , IBaseService<DoctorDE> docSvc )
        {
            _patientRptDAL = new PatientReportDAL ();
            _extraFieldsDataDAL = new RxMedExtraFieldsDataDAL ();
            _rxMedDAL = new PrescriptionDAL ();
            _patSvc = new PatientService ();
            _docSvc = docSvc;
            _apptSvc = apptSvc;
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }
        #endregion
        #region Prescription
        public  PrescriptionDE ManagementPrescriptionAsync ( PrescriptionDE mod, int? Id = null )
        {
            MySqlCommand cmd = null;
            try
            {
                bool retVal = false;
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                MicroERPDataContext.StartTransaction (cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetNextIdByClient (TableNames.PMS_Prescription.ToString (), mod.ClientId, "ClientId");
                if (mod.Precauts != null)
                    mod.PrecautionIds = string.Join (",", mod.Precauts.ToArray ());
                var _rptId = _corDAL.GetMaxLineIdByClt (TableNames.PMS_PatientReport.ToString (), "RxId", mod.Id, mod.ClientId);
                var _medId = _corDAL.GetMaxLineIdByClt (TableNames.PMS_RxMedicine.ToString (), "RxId", mod.Id, mod.ClientId);
                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                {
                    #region Medicines
                    foreach (var rxDet in mod.RxMedicines)
                    {
                        rxDet.RxId = mod.Id;
                        rxDet.ClientId = mod.ClientId;
                        if (rxDet.DBoperation == DBoperations.Insert)
                        {
                            _medId += 1;
                            rxDet.Id = _medId;
                        }
                        retVal = _rxMedDAL.ManageRxMedicine (rxDet, cmd);
                    }
                    #endregion
                    #region RxParameters
                    foreach (var line in mod.rxmefData)
                    {
                        string whereClause = "where 1=1";
                        var list = _extraFieldsDataDAL.SearchRxMedExtraFieldsData (whereClause += $" AND RxId={mod.Id}  and FieldId ={line.FieldId}  and ClientId={mod.ClientId}");
                        if (list != null)
                            if (list.Count > 0)
                            {
                                line.DBoperation = DBoperations.Update;
                                line.IsActive = true;
                            }
                            else line.DBoperation = DBoperations.Insert;

                        line.RxId = mod.Id;
                        line.ClientId = mod.ClientId;
                        _extraFieldsDataDAL.ManageRxMedExtraFieldsData (line, cmd);
                    }
                    #endregion
                    #region Reports
                    foreach (var rpt in mod.Reports)
                    {
                        rpt.RxId = mod.Id;
                        rpt.ClientId = mod.ClientId;
                        if (rpt.DBoperation == DBoperations.Insert)
                        {
                            _rptId += 1;
                            rpt.Id = _rptId;
                        }
                        retVal = _patientRptDAL.ManagePatientReport (rpt, cmd);
                    }
                    #endregion
                }
                if (mod.NextVisitDate.HasValue && mod.NextVisitDate != DateTime.MinValue)
                {
                    var appt = new AppointmentDE
                    {
                        DoctorId = mod.DoctorId,
                        PatientId = mod.PatientId,
                        StatusId = (int)AppStatuses.Due,
                        DBoperation = DBoperations.Insert,
                        Date= (DateTime)mod.NextVisitDate,
                        ClientId=mod.ClientId,
                        IsActive=true
                    };
                    var patVisit = SearchPrescriptions (new PrescriptionDE { Id = mod.Id, ClientId=mod.ClientId });
                    if (patVisit!=null && patVisit.Count > 0)
                    {
                        if (patVisit[0].NextVisitDate.HasValue && patVisit[0].NextVisitDate != DateTime.MinValue)
                        {
                            if (patVisit[0].NextVisitDate.Value.ToString ("yyyy-MM-dd") != mod.NextVisitDate.Value.ToString ("yyyy-MM-dd"))
                            {
                                var app = _apptSvc.SearchData (new AppointmentDE
                                {
                                    PatientId = mod.PatientId,
                                    DoctorId = mod.DoctorId,
                                    ClientId= mod.ClientId,
                                    TokenNo=mod.NextVisitNo,
                                    ApptDate = patVisit[0].NextVisitDate,

                                });
                                if (app!=null && app.Count > 0)
                                {
                                    app[0].StatusId = (int)AppStatuses.Cancled;
                                    app[0].DBoperation = DBoperations.Update;
                                    _apptSvc.ManageData (app[0]);
                                }
                                appt= CreateAppt (appt);
                            }
                        }
                        else
                        {
                            appt= CreateAppt (appt);
                        }
                    }
                    else
                    {
                        appt= CreateAppt (appt);
                    }
                    mod.NextVisitNo = appt.TokenNo;
                }
                retVal = _rxMedDAL.ManagePrescription (mod, cmd);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                MicroERPDataContext.EndTransaction (cmd);
            }
            catch (Exception exp)
            {
                _logger.Error (exp);
                MicroERPDataContext.CancelTransaction (cmd);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
                string whereClause = " Where 1=1";
                var rx = _rxMedDAL.SearchPrescriptions (whereClause += $" AND Id={mod.Id} AND IsActive ={true} and ClientId={mod.ClientId}").FirstOrDefault ();
                if (rx != null)
                    mod = rx;
                whereClause = " Where 1=1";
                mod.RxMedicines = _rxMedDAL.SearchRxMedicine (whereClause += $" AND RxId={mod.Id} AND IsActive ={true} and ClientId={mod.ClientId}");
                string _whereClause = "where 1=1";
                mod.rxmefData = _extraFieldsDataDAL.SearchRxMedExtraFieldsData (_whereClause += $" AND RxId={mod.Id} and ClientId={mod.ClientId}");
                whereClause = " Where 1=1";
                mod.Reports = _patientRptDAL.SearchPatientReport (whereClause += $" AND RxId={mod.Id} AND IsActive ={true} and ClientId={mod.ClientId}");
                if (mod.PrecautionIds != null && mod.PrecautionIds != "")
                {
                    List<string> result = mod.PrecautionIds.Split (',').ToList ();
                    mod.Precauts = new List<int> ();
                    foreach (var line in result)
                    {
                        mod.Precauts.Add (int.Parse (line));
                    }
                }
            }
            return mod;
        }
        public AppointmentDE CreateAppt ( AppointmentDE app )
        {
            try
            {
                var doctor = _docSvc.SearchData (new DoctorDE { Id = (int)app.DoctorId, ClientId=app.ClientId }).FirstOrDefault ();
                var patient = _patSvc.SearchPatient (new PatientDE { Id = (int)app.PatientId, ClientId = app.ClientId }).FirstOrDefault ();
                app.DOB = patient.DateOfBirth;
                app.Time = doctor.StartTime;
                app.GenderId = patient.GenderId;
                var appointments= _apptSvc.SearchData(new AppointmentDE { 
                    DoctorId=app.DoctorId,
                    ClientId=app.ClientId,
                    ApptDate=app.Date});
                if(appointments != null)
                {
                    if (appointments.Count > 0)
                    {
                        appointments=appointments.Where(x=>x.StatusId !=(int)AppStatuses.Cancled).ToList();
                        var value = appointments.Find (x => x.Time == app.Time );
                        while (value != null)
                        {
                            TimeSpan duration = TimeSpan.FromMinutes ((double)doctor.DefApptDur); 
                            if (app.Time != null)
                            {
                                DateTime dateTime = DateTime.ParseExact (app.Time, "h:mm tt", null);
                                TimeSpan time1 = dateTime.TimeOfDay;
                                TimeSpan sum = time1.Add (duration);
                                string sumTime = DateTime.MinValue.Add (sum).ToString ("h:mm tt");

                                
                                app.Time = sumTime;
                                var appt = appointments.Find (x => x.Time == app.Time );
                                
                                if (appt == null)
                                    value = null;
                            }
                        }
                    }
                }
                _apptSvc.ManageData (app);
                return app;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PrescriptionDE> SearchPrescriptions ( PrescriptionDE mod )
        {
            List<PrescriptionDE> rx = new List<PrescriptionDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.PatientId != default && mod.PatientId != 0)
                    whereClause += $" AND PatientId={mod.PatientId}";
                if (mod.DoctorId != default && mod.DoctorId != 0)
                    whereClause += $" AND DoctorId={mod.DoctorId}";
                if (mod.From.HasValue && mod.From.Value > DateTime.MinValue)
                    whereClause += $" AND Date >= ''{mod.From.Value:yyyy-MM-dd} 00:00:00''";
                if (mod.To.HasValue && mod.To.Value > DateTime.MinValue)
                    whereClause += $" AND Date <= ''{mod.To.Value:yyyy-MM-dd} 23:59:59''";

                rx = _rxMedDAL.SearchPrescriptions (whereClause);
                foreach (var line in rx)
                {
                    line.PatientAge = line.PatientDOB.GetAgeFromDOB ();
                    if (line.PrecautionIds != null && line.PrecautionIds != "")
                    {
                        List<string> result = line.PrecautionIds.Split (',').ToList ();
                        line.Precauts = new List<int> ();
                        foreach (var pre in result)
                        {
                            line.Precauts.Add (int.Parse (pre));
                        }
                    }
                    whereClause = " Where 1=1";
                    line.RxMedicines = _rxMedDAL.SearchRxMedicine (whereClause += $" AND RxId={line.Id} AND IsActive ={true} and ClientId={line.ClientId}");
                    string _whereClause = "where 1=1";
                    line.rxmefData = _extraFieldsDataDAL.SearchRxMedExtraFieldsData (_whereClause += $" AND RxId={line.Id} AND IsActive ={true}  and ClientId={line.ClientId}");
                    whereClause = " Where 1=1";
                    line.Reports = _patientRptDAL.SearchPatientReport (whereClause += $" AND RxId={line.Id} AND IsActive ={true} and ClientId={line.ClientId}");
                }
                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error (exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return rx;
        }
        #endregion
    }
}
