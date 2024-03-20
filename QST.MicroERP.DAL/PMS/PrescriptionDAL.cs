using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL
{
    public class PrescriptionDAL
    {
        #region Header Operations
        public bool ManagePrescription ( PrescriptionDE _rx, MySqlCommand cmd = null )
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                cmd.CommandText = SPNames.PMS_Manage_Prescription.ToString();
                cmd.Parameters.AddWithValue ("@prm_id", _rx.Id);
                cmd.Parameters.AddWithValue ("@prm_clientId", _rx.ClientId);
                cmd.Parameters.AddWithValue ("@prm_doctorId", _rx.DoctorId);
                cmd.Parameters.AddWithValue ("@prm_patientId", _rx.PatientId);
                cmd.Parameters.AddWithValue ("@prm_tokenNo", _rx.TokenNo);
                cmd.Parameters.AddWithValue ("@prm_date", _rx.Date);
                cmd.Parameters.AddWithValue ("@prm_time", _rx.Time);
                cmd.Parameters.AddWithValue ("@prm_amount", _rx.Amount);
                cmd.Parameters.AddWithValue ("@prm_remarks", _rx.Remarks);
                cmd.Parameters.AddWithValue ("@prm_medDetRemarks", _rx.MedDetRemarks);
                cmd.Parameters.AddWithValue ("@prm_temperature", _rx.Temperature);
                cmd.Parameters.AddWithValue ("@prm_nextVisitDate", _rx.NextVisitDate);
                cmd.Parameters.AddWithValue ("@prm_nextVisitNo", _rx.NextVisitNo);
                cmd.Parameters.AddWithValue ("@prm_comments", _rx.Comments);
                cmd.Parameters.AddWithValue ("@prm_bPStatusId", _rx.BPStatusId);
                cmd.Parameters.AddWithValue ("@prm_weight", _rx.Weight);
                cmd.Parameters.AddWithValue ("@prm_isSugarPatient", _rx.IsSugarPatient);
                cmd.Parameters.AddWithValue ("@prm_precautionIds", _rx.PrecautionIds);
                cmd.Parameters.AddWithValue ("@prm_createdOn", _rx.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", _rx.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", _rx.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", _rx.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", _rx.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", _rx.DBoperation.ToString ());

                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
                cmd.Parameters.Clear ();
            }
        }
        public List<PrescriptionDE> SearchPrescriptions ( string whereClause, MySqlCommand cmd = null )
        {
            List<PrescriptionDE> top = new List<PrescriptionDE> ();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                top = cmd.Connection.Query<PrescriptionDE> ("call "+SPNames.PMS_Search_Prescription.ToString()+"( '" + whereClause + "')").ToList ();
                return top;
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }

        #endregion
        #region Line Operations
        public bool ManageRxMedicine ( RxMedicineDE rxMed, MySqlCommand cmd = null )
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                cmd.CommandText = SPNames.PMS_Manage_RxMedicine.ToString();
                cmd.Parameters.AddWithValue ("@prm_id", rxMed.Id);
                cmd.Parameters.AddWithValue ("@prm_medId", rxMed.MedId);
                cmd.Parameters.AddWithValue ("@prm_clientId", rxMed.ClientId);
                cmd.Parameters.AddWithValue ("@prm_mRId", rxMed.MRId);
                cmd.Parameters.AddWithValue ("@prm_rxId", rxMed.RxId);
                cmd.Parameters.AddWithValue ("@prm_aMQty", rxMed.AMQty);
                cmd.Parameters.AddWithValue ("@prm_noonQty", rxMed.NoonQty);
                cmd.Parameters.AddWithValue ("@prm_eveQty", rxMed.EveQty);
                cmd.Parameters.AddWithValue ("@prm_days", rxMed.Days);
                cmd.Parameters.AddWithValue ("@prm_remarksId", rxMed.RemarksId);
                cmd.Parameters.AddWithValue ("@prm_createdOn", rxMed.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", rxMed.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", rxMed.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", rxMed.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", rxMed.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", rxMed.DBoperation.ToString ());

                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
                cmd.Parameters.Clear ();
            }
        }
        public List<RxMedicineDE> SearchRxMedicine ( string whereClause, MySqlCommand cmd = null )
        {
            List<RxMedicineDE> top = new List<RxMedicineDE> ();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                top = cmd.Connection.Query<RxMedicineDE> ("call "+SPNames.PMS_Search_RxMedicine.ToString()+"( '" + whereClause + "')").ToList ();
                return top;
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        #endregion
    }
}
