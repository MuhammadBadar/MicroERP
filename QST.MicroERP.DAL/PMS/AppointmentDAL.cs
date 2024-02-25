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
                appts = _cmd.Connection.Query<AppointmentDE> ("call SearchAppointment('" + whereClause + "')").ToList ();
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
                _cmd.CommandText = "ManageAppointment";
                _cmd.Parameters.AddWithValue ("id", _appoint.Id);
                _cmd.Parameters.AddWithValue ("clientId", _appoint.ClientId);
                _cmd.Parameters.AddWithValue ("patientid", _appoint.PatientId);
                _cmd.Parameters.AddWithValue ("doctorId", _appoint.DoctorId);
                _cmd.Parameters.AddWithValue ("tokenNo", _appoint.TokenNo);
                _cmd.Parameters.AddWithValue ("date", _appoint.Date);
                _cmd.Parameters.AddWithValue ("time", _appoint.Time);
                _cmd.Parameters.AddWithValue ("age", _appoint.Age);
                _cmd.Parameters.AddWithValue ("genderId", _appoint.GenderId);
                _cmd.Parameters.AddWithValue ("statusId", _appoint.StatusId);
                _cmd.Parameters.AddWithValue ("createdOn", _appoint.CreatedOn);
                _cmd.Parameters.AddWithValue ("createdById", _appoint.CreatedById);
                _cmd.Parameters.AddWithValue ("modifiedOn", _appoint.ModifiedOn);
                _cmd.Parameters.AddWithValue ("modifiedById", _appoint.ModifiedById);
                _cmd.Parameters.AddWithValue ("isActive", _appoint.IsActive);
                _cmd.Parameters.AddWithValue ("DbOperation", _appoint.DBoperation.ToString ());
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
                string procedureName = "QST.MicroERP.GetNextAppt";
                string sql = $"CALL {procedureName}(@whereClause, @clientId, @doctorId)";
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
        #region Operations
        public bool ManageAppointment ( AppointmentDE _appoint, MySqlCommand? cmd )
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageAppointment";
                cmd.Parameters.AddWithValue ("id", _appoint.Id);
                cmd.Parameters.AddWithValue ("patientid", _appoint.PatientId);
                cmd.Parameters.AddWithValue ("doctorId", _appoint.DoctorId);
                cmd.Parameters.AddWithValue ("date", _appoint.Date);
                cmd.Parameters.AddWithValue ("time", _appoint.Time);
                cmd.Parameters.AddWithValue ("age", _appoint.Age);
                cmd.Parameters.AddWithValue ("genderId", _appoint.GenderId);
                cmd.Parameters.AddWithValue ("status", _appoint.Status);
                cmd.Parameters.AddWithValue ("createdOn", _appoint.CreatedOn);
                cmd.Parameters.AddWithValue ("createdById", _appoint.CreatedById);
                cmd.Parameters.AddWithValue ("modifiedOn", _appoint.ModifiedOn);
                cmd.Parameters.AddWithValue ("modifiedById", _appoint.ModifiedById);
                cmd.Parameters.AddWithValue ("isActive", _appoint.IsActive);
                cmd.Parameters.AddWithValue ("DbOperation", _appoint.DBoperation.ToString ());
                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<AppointmentDE> SearchAppointment ( string WhereClause, MySqlCommand cmd )
        {
            bool closeConnection = false;

            List<AppointmentDE> test = new List<AppointmentDE> ();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                test = cmd.Connection.Query<AppointmentDE> ("call SearchAppointment('" + WhereClause + "')").ToList ();
                return test;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<AppointmentDE> SearchNextAppmt ( string WhereClause, MySqlCommand cmd )
        {
            bool closeConnection = false;

            List<AppointmentDE> test = new List<AppointmentDE> ();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                test = cmd.Connection.Query<AppointmentDE> ("call GetNextAppt('" + WhereClause + "')").ToList ();
                return test;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        #endregion
    }
}
