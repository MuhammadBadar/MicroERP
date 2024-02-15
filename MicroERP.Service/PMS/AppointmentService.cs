using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.IDAL;
using MicroERP.Service.IServices;
using MySql.Data.MySqlClient;
using MicroERP.DAL.CTL;
using KeyAccounting.Core.Extenstions;

namespace MicroERP.Service
{
    public class AppointmentService : BaseService, IBaseService<AppointmentDE>, IApptService
    {
        #region Class Variables
        private readonly IBaseDAL<AppointmentDE> _baseDAL;
        private readonly IApptDAL _apptDAL;
        private CoreDAL _coreDAL;
        private readonly IBaseService<DoctorDE> _docSvc;
        #endregion
        #region Constructor
        public AppointmentService ( IBaseDAL<AppointmentDE> baseDAL, IApptDAL apptDAL, IBaseService<DoctorDE> docSvc )
        {
            _baseDAL = baseDAL;
            _apptDAL = apptDAL;
            _docSvc = docSvc;
            _coreDAL = new CoreDAL ();
        }
        #endregion
        #region Appointment
        public bool ManageData ( AppointmentDE mod )
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                if (mod.DBoperation == DBoperations.Insert)
                {
                    mod.Id = _coreDAL.GetNextIdByClient (TableNames.appointment.ToString (), mod.ClientId, "ClientId");
                    if (mod.TokenId == 0 || mod.TokenId == default)
                    {
                        List<AppointmentDE> appts = SearchData (new AppointmentDE { ClientId = mod.ClientId, ApptDate = mod.Date, DoctorId = mod.DoctorId });
                        AppointmentDE searchedApp = GetLastAppt (appts, mod.Date);
                        mod.TokenNo = _coreDAL.GetNextTokenno (searchedApp);
                    }
                    mod.Age = mod.DOB.GetAgeFromDOB ();
                }
                retVal = _baseDAL.ManageData (mod, cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public int GetNextTokenNo ( AppointmentDE mod )
        {
            int tknNo = 0;
            List<AppointmentDE> appts = SearchData (new AppointmentDE { ClientId = mod.ClientId, ApptDate = mod.Date, DoctorId = mod.DoctorId });
            AppointmentDE searchedApp = GetLastAppt (appts, mod.Date);
            var nmbr = (int)_coreDAL.GetNextTokenno (searchedApp);
            if (nmbr > 0)
                tknNo = int.Parse (nmbr.ToString ().Substring (6));
            return tknNo;
        }
        public AppointmentDE GetLastAppt ( List<AppointmentDE> appts, DateTime date )
        {
            var searchedApp = new AppointmentDE ();
                appts = appts.OrderBy (obj => obj.TokenNo).ToList ();
                for (int i = 0; i < appts.Count - 1; i++)
                {
                    if (int.Parse (appts[i].TokenNo.ToString ().Substring (6)) + 1 != int.Parse (appts[i+1].TokenNo.ToString ().Substring (6)))
                    {
                        searchedApp = appts[i];
                        break;
                    }
                }
                if (appts.Count > 0)
                    if (searchedApp.Id == 0)
                        searchedApp = appts.Last ();
            searchedApp.Date = date;
            return searchedApp;
        }
        public List<AppointmentDE> SearchData ( AppointmentDE _appoint )
        {
            List<AppointmentDE> retVal = new List<AppointmentDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_appoint.Id != default)
                    WhereClause += $" AND Id={_appoint.Id}";
                if (_appoint.TokenNo != default && _appoint.TokenNo != 0)
                    WhereClause += $" AND TokenNo={_appoint.TokenNo}";
                if (_appoint.StatusId != default && _appoint.StatusId != 0)
                    WhereClause += $" AND StatusId={_appoint.StatusId}";
                if (_appoint.ClientId != default && _appoint.ClientId != 0)
                    WhereClause += $" AND ClientId={_appoint.ClientId}";
                if (_appoint.PatientId != default && _appoint.PatientId != 0)
                    WhereClause += $" AND PatientId={_appoint.PatientId}";
                if (_appoint.DoctorId != default && _appoint.DoctorId != 0)
                    WhereClause += $" AND DoctorId={_appoint.DoctorId}";
                if (_appoint.GenderId != default && _appoint.GenderId != 0)
                    WhereClause += $" AND GenderId={_appoint.GenderId}";
                if (_appoint.From.HasValue && _appoint.From.Value > DateTime.MinValue)
                    WhereClause += $" AND Date >= ''{_appoint.From.Value:yyyy-MM-dd} 00:00:00''";
                if (_appoint.To.HasValue && _appoint.To.Value > DateTime.MinValue)
                    WhereClause += $" AND Date <= ''{_appoint.To.Value:yyyy-MM-dd} 23:59:59''";
                if (_appoint.PatientName != default)
                    WhereClause += $" AND PatientName like ''" + _appoint.PatientName + "''";
                if (_appoint.ApptDate.HasValue && _appoint.ApptDate.Value > DateTime.MinValue)
                    WhereClause += $" and Date(Date) like ''" + _appoint.ApptDate?.ToString ("yyyy-MM-dd") + "''";
                if (_appoint.Time != default)
                    WhereClause += $" AND Time like ''" + _appoint.Time + "''";
                if (_appoint.Age != default)
                    WhereClause += $" AND Age like ''" + _appoint.Age + "''";
                if (_appoint.Gender != default)
                    WhereClause += $" AND Gender like ''" + _appoint.Gender + "''";
                if (_appoint.Status != default)
                    WhereClause += $" AND Status like ''" + _appoint.Status + "''";
                if (_appoint.IsActive != default && _appoint.IsActive == true)
                    WhereClause += $" AND IsActive=1";

                retVal = _baseDAL.SearchData (WhereClause, cmd);
                foreach (var item in retVal)
                {
                    item.ShortAge = item.Age.GetShortAge ();
                }
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<AppointmentDE> SearchAdjacentAppts ( AppointmentDE _appoint )
        {
            List<AppointmentDE> retVal = new List<AppointmentDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                string WhereClause = "";
                if (_appoint.AdjacentType == "Next")
                    WhereClause += " SELECT Min(TokenNo) INTO @next_id FROM appointment WHERE 1=1";
                if (_appoint.AdjacentType == "Previous")
                    WhereClause = " SELECT Max(TokenNo) INTO @next_id FROM appointment WHERE 1=1";
                if (_appoint.ClientId != default)
                    WhereClause += $" AND ClientId={_appoint.ClientId}";
                if (_appoint.DoctorId != default && _appoint.DoctorId != 0)
                    WhereClause += $" AND DoctorId={_appoint.DoctorId}";
                if (_appoint.StatusId != default && _appoint.StatusId != 0)
                    WhereClause += $" AND StatusId={_appoint.StatusId}";
                if (_appoint.ApptDate.HasValue && _appoint.ApptDate.Value > DateTime.MinValue)
                    WhereClause += $" and Date(Date) like '" + _appoint.ApptDate?.ToString ("yyyy-MM-dd") + "'";
                if (_appoint.IsActive != default && _appoint.IsActive == true)
                    WhereClause += $" AND IsActive=1";
                if (_appoint.Id != default)
                    WhereClause += $" AND Id>{_appoint.Id}";
                if (_appoint.TokenNo != default)
                    if (_appoint.AdjacentType == "Next")
                        WhereClause += $" AND TokenNo>{_appoint.TokenNo}";
                    else if (_appoint.AdjacentType == "Previous")
                        WhereClause += $" AND TokenNo<{_appoint.TokenNo}";
                retVal = _apptDAL.SearchNextAppt (WhereClause, _appoint.ClientId, (int)_appoint.DoctorId, cmd);
                foreach (var item in retVal)
                {
                    item.ShortAge = item.Age.GetShortAge ();
                }
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public string ApptMinTime ( AppointmentDE app )
        {
            string? minTime = "";
            var doc = _docSvc.SearchData (new DoctorDE { Id = (int)app.DoctorId, ClientId = app.ClientId });
            if (doc != null && doc.Count > 0 && doc[0].StartTime!=null)
                minTime = doc[0].StartTime.ToString ();
            var appointments = SearchData (new AppointmentDE
            {
                ClientId = app.ClientId,
                DoctorId = app.DoctorId,
                ApptDate = app.Date
            });
            if (appointments != null)
            {
                if (appointments.Count > 0)
                {
                    var value = appointments.Find (x => x.Time == minTime);
                    while (value != null)
                    {
                        if (doc[0].DefApptDur != null)
                        {
                            TimeSpan duration = TimeSpan.FromMinutes ((double)doc[0].DefApptDur); //  minutes duration
                            if (minTime != null)
                            {
                                DateTime dateTime = DateTime.ParseExact (minTime, "h:mm tt", null);
                                TimeSpan time1 = dateTime.TimeOfDay;
                                TimeSpan sum = time1.Add (duration);
                                string sumTime = DateTime.MinValue.Add (sum).ToString ("h:mm tt");


                                minTime = sumTime;
                                var appt = appointments.Find (x => x.Time == minTime);
                                if (appt == null)
                                    value = null;
                            }
                        }
                    }
                }
            }
            return minTime;
        }
        #endregion
    }
}
