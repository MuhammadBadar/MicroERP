using Dapper;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.DAL.IDAL;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.EC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL
{
    public class AppointmentDAL :  IBaseDAL<AppointmentDE>, IApptDAL
    {
        #region Constructor
        public AppointmentDAL()
        {
            
        }
        #endregion
        #region Operations
        public List<AppointmentDE> SearchData ( string whereClause, MySqlCommand? _cmd )
        {
            bool closeConnection = false;

            List<AppointmentDE> appts = new List<AppointmentDE> ();
            try
            {
                if (_cmd == null)
                {
                    _cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                appts = _cmd.Connection.Query<AppointmentDE> ("call "+SPNames.PMS_Search_Appointment.ToString()+"('" + whereClause + "')").ToList ();
                return appts;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection (_cmd);
            }
        }
        public  bool ManageData ( AppointmentDE _appoint, MySqlCommand? _cmd )
        {
            bool closeConnection = false;
            try
            {
                if (_cmd == null)
                {
                    _cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                _cmd.CommandText = SPNames.PMS_Manage_Appointment.ToString();
                _cmd.Parameters.AddWithValue ("prm_id", _appoint.Id);
                _cmd.Parameters.AddWithValue ("prm_clientId", _appoint.ClientId);
                _cmd.Parameters.AddWithValue ("prm_patientid", _appoint.PatientId);
                _cmd.Parameters.AddWithValue ("prm_doctorId", _appoint.DoctorId);
                _cmd.Parameters.AddWithValue ("prm_tokenNo", _appoint.TokenNo);
                _cmd.Parameters.AddWithValue ("prm_date", _appoint.Date);
                _cmd.Parameters.AddWithValue ("prm_time", _appoint.Time);
                _cmd.Parameters.AddWithValue ("prm_age", _appoint.Age);
                _cmd.Parameters.AddWithValue ("prm_genderId", _appoint.GenderId);
                _cmd.Parameters.AddWithValue ("prm_statusId", _appoint.StatusId);
                _cmd.Parameters.AddWithValue ("prm_createdOn", _appoint.CreatedOn);
                _cmd.Parameters.AddWithValue ("prm_createdById", _appoint.CreatedById);
                _cmd.Parameters.AddWithValue ("prm_modifiedOn", _appoint.ModifiedOn);
                _cmd.Parameters.AddWithValue ("prm_modifiedById", _appoint.ModifiedById);
                _cmd.Parameters.AddWithValue ("prm_isActive", _appoint.IsActive);
                _cmd.Parameters.AddWithValue ("prm_DbOperation", _appoint.DBoperation.ToString ());
                _cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection (_cmd);
            }
        }
        public List<AppointmentDE> SearchNextAppt ( string whereClause,int ClientId,int DoctorId, MySqlCommand? _cmd )
        {
            bool closeConnection = false;

            List<AppointmentDE> appts = new List<AppointmentDE> ();
            try
            {
                if (_cmd == null)
                {
                    _cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                var parameters = new
                {
                    whereClause = $""+whereClause+"",
                    clientId =ClientId,
                    doctorId=DoctorId
                };
                string procedureName = SPNames.PMS_GET_NextAppt.ToString();
                string sql = $"CALL {procedureName}(@whereClause, @prm_clientId, @prm_doctorId)";
                //appts = _cmd.Connection.Query<AppointmentDE> ("call GetNextAppt('" + whereClause + ""+","+"" + ClientId + " ')").ToList (); 
                appts = _cmd.Connection.Query<AppointmentDE> (sql, parameters, commandType: CommandType.Text).ToList ();
                return appts;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection (_cmd);
            }
        }

        #endregion
    }
}
