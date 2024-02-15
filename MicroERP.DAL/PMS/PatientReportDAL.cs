using Dapper;
using MySql.Data.MySqlClient;
using MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL
{
    public class PatientReportDAL
    {
        #region Operations
        public bool ManagePatientReport ( PatientReportDE patRpt, MySqlCommand? cmd  )
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
                cmd.CommandText = "ManagePatientReport";
                cmd.Parameters.AddWithValue ("@id", patRpt.Id);
                cmd.Parameters.AddWithValue ("@rxId", patRpt.RxId);
                cmd.Parameters.AddWithValue ("@clientId", patRpt.ClientId);
                cmd.Parameters.AddWithValue ("@date", patRpt.Date);
                cmd.Parameters.AddWithValue ("@categoryId", patRpt.CategoryId);
                cmd.Parameters.AddWithValue ("@name", patRpt.Name);
                cmd.Parameters.AddWithValue ("@reportBase64Path", patRpt.ReportBase64Path);
                cmd.Parameters.AddWithValue ("@createdOn", patRpt.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", patRpt.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", patRpt.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", patRpt.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", patRpt.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", patRpt.DBoperation.ToString ());

                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception)
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
        public List<PatientReportDE> SearchPatientReport ( string whereClause, MySqlCommand cmd = null )
        {
            List<PatientReportDE> top = new List<PatientReportDE> ();
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
                top = cmd.Connection.Query<PatientReportDE> ("call MicroERP.SearchPatientReport( '" + whereClause + "')").ToList ();
                return top;
            }
            catch (Exception)
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
