using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                cmd.CommandText = "ManagePrescription";
                cmd.Parameters.AddWithValue ("@id", _rx.Id);
                cmd.Parameters.AddWithValue ("@clientId", _rx.ClientId);
                cmd.Parameters.AddWithValue ("@doctorId", _rx.DoctorId);
                cmd.Parameters.AddWithValue ("@patientId", _rx.PatientId);
                cmd.Parameters.AddWithValue ("@tokenNo", _rx.TokenNo);
                cmd.Parameters.AddWithValue ("@date", _rx.Date);
                cmd.Parameters.AddWithValue ("@time", _rx.Time);
                cmd.Parameters.AddWithValue ("@amount", _rx.Amount);
                cmd.Parameters.AddWithValue ("@remarks", _rx.Remarks);
                cmd.Parameters.AddWithValue ("@medDetRemarks", _rx.MedDetRemarks);
                cmd.Parameters.AddWithValue ("@temperature", _rx.Temperature);
                cmd.Parameters.AddWithValue ("@nextVisitDate", _rx.NextVisitDate);
                cmd.Parameters.AddWithValue ("@nextVisitNo", _rx.NextVisitNo);
                cmd.Parameters.AddWithValue ("@comments", _rx.Comments);
                cmd.Parameters.AddWithValue ("@bPStatusId", _rx.BPStatusId);
                cmd.Parameters.AddWithValue ("@weight", _rx.Weight);
                cmd.Parameters.AddWithValue ("@isSugarPatient", _rx.IsSugarPatient);
                cmd.Parameters.AddWithValue ("@precautionIds", _rx.PrecautionIds);
                cmd.Parameters.AddWithValue ("@createdOn", _rx.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", _rx.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", _rx.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", _rx.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", _rx.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", _rx.DBoperation.ToString ());

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
                top = cmd.Connection.Query<PrescriptionDE> ("call SearchPrescription( '" + whereClause + "')").ToList ();
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
                cmd.CommandText = "ManageRxMedicine";
                cmd.Parameters.AddWithValue ("@id", rxMed.Id);
                cmd.Parameters.AddWithValue ("@medId", rxMed.MedId);
                cmd.Parameters.AddWithValue ("@clientId",rxMed.ClientId);
                cmd.Parameters.AddWithValue ("@mRId", rxMed.MRId);
                cmd.Parameters.AddWithValue ("@rxId", rxMed.RxId);
                cmd.Parameters.AddWithValue ("@aMQty", rxMed.AMQty);
                cmd.Parameters.AddWithValue ("@noonQty", rxMed.NoonQty);
                cmd.Parameters.AddWithValue ("@eveQty", rxMed.EveQty);
                cmd.Parameters.AddWithValue ("@days", rxMed.Days);
                cmd.Parameters.AddWithValue ("@remarksId", rxMed.RemarksId);
                cmd.Parameters.AddWithValue ("@createdOn", rxMed.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", rxMed.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", rxMed.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", rxMed.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", rxMed.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", rxMed.DBoperation.ToString ());

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
                top = cmd.Connection.Query<RxMedicineDE> ("call SearchRxMedicine( '" + whereClause + "')").ToList ();
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
