using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL
{
    public class PatientDAL
    {
        #region DbOperations
        public bool ManagePatient(PatientDE _pat, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = SPNames.PMS_Manage_Patient.ToString();
                cmd.Parameters.AddWithValue("prm_id", _pat.Id);
                cmd.Parameters.AddWithValue("prm_clientId", _pat.ClientId);
                cmd.Parameters.AddWithValue("prm_countryId", _pat.CountryId);
                cmd.Parameters.AddWithValue("prm_cityId", _pat.CityId);
                cmd.Parameters.AddWithValue("prm_areaId", _pat.AreaId);
                cmd.Parameters.AddWithValue("prm_email", _pat.Email);
                cmd.Parameters.AddWithValue("prm_patientName", _pat.PatientName);
                cmd.Parameters.AddWithValue("prm_dateOfBirth", _pat.DateOfBirth);
                cmd.Parameters.AddWithValue("prm_genderId", _pat.GenderId);
                cmd.Parameters.AddWithValue("prm_contactNo", _pat.ContactNo);
                cmd.Parameters.AddWithValue("prm_houseNo", _pat.HouseNo);
                cmd.Parameters.AddWithValue("prm_address", _pat.Address);
                cmd.Parameters.AddWithValue("prm_remarks", _pat.Remarks);
                cmd.Parameters.AddWithValue("prm_createdOn", _pat.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", _pat.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", _pat.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", _pat.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", _pat.IsActive);
                cmd.Parameters.AddWithValue("prm_DbOperation", _pat.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
                cmd.Parameters.Clear ();
            }
        }
        public List<PatientDE> SearchPatient(string WhereClause, MySqlCommand cmd)
        {
            bool closeConnection = false;
            //WhereClause = string.Empty;
            List<PatientDE> pat = new List<PatientDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                pat = cmd.Connection.Query<PatientDE>("call "+SPNames.PMS_Search_Patient.ToString()+"('" + WhereClause + "')").ToList();
                return pat;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        #endregion
    }
}
