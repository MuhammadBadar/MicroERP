using Dapper;
using MySql.Data.MySqlClient;
using QST.MicroERP.Core.Constants;
using QST.MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL
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
                cmd.CommandText = SPNames.PMS_Manage_PatientReport.ToString();
                cmd.Parameters.AddWithValue ("@prm_id", patRpt.Id);
                cmd.Parameters.AddWithValue ("@prm_rxId", patRpt.RxId);
                cmd.Parameters.AddWithValue ("@prm_clientId", patRpt.ClientId);
                cmd.Parameters.AddWithValue ("@prm_date", patRpt.Date);
                cmd.Parameters.AddWithValue ("@prm_categoryId", patRpt.CategoryId);
                cmd.Parameters.AddWithValue ("@prm_name", patRpt.Name);
                cmd.Parameters.AddWithValue ("@prm_reportBase64Path", patRpt.ReportBase64Path);
                cmd.Parameters.AddWithValue ("@prm_createdOn", patRpt.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", patRpt.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", patRpt.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", patRpt.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", patRpt.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", patRpt.DBoperation.ToString ());

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
                top = cmd.Connection.Query<PatientReportDE> ("call "+SPNames.PMS_Search_PatientReport.ToString()+"( '" + whereClause + "')").ToList ();
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
