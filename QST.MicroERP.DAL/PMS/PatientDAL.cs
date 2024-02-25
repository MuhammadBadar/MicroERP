using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;

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
                cmd.CommandText = "ManagePatient";
                cmd.Parameters.AddWithValue("id", _pat.Id);
                cmd.Parameters.AddWithValue ("clientId", _pat.ClientId);
                cmd.Parameters.AddWithValue ("countryId", _pat.CountryId);
                cmd.Parameters.AddWithValue ("cityId", _pat.CityId);
                cmd.Parameters.AddWithValue ("areaId", _pat.AreaId);
                cmd.Parameters.AddWithValue ("email", _pat.Email);
                cmd.Parameters.AddWithValue("patientName", _pat.PatientName);
                cmd.Parameters.AddWithValue("dateOfBirth", _pat.DateOfBirth);
                cmd.Parameters.AddWithValue("genderId", _pat.GenderId);
                cmd.Parameters.AddWithValue("contactNo", _pat.ContactNo);
                cmd.Parameters.AddWithValue("houseNo", _pat.HouseNo);
                cmd.Parameters.AddWithValue("address", _pat.Address);
                cmd.Parameters.AddWithValue("remarks", _pat.Remarks);
                cmd.Parameters.AddWithValue("createdOn", _pat.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", _pat.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", _pat.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", _pat.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", _pat.IsActive);
                cmd.Parameters.AddWithValue("DbOperation", _pat.DBoperation.ToString());
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
                pat = cmd.Connection.Query<PatientDE>("call SearchPatient('" + WhereClause + "')").ToList();
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
