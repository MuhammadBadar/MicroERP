using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.DAL.IDAL;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.EC;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL
{
    public class DoctorDAL:IBaseDAL<DoctorDE>
    {
        #region DbOperations
        public bool ManageData(DoctorDE _doc, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = SPNames.PMS_Manage_Doctor.ToString();
                cmd.Parameters.AddWithValue("prm_id", _doc.Id);
                cmd.Parameters.AddWithValue("prm_clientId", _doc.ClientId);
                cmd.Parameters.AddWithValue("prm_userId", _doc.UserId);
                cmd.Parameters.AddWithValue("prm_countryId", _doc.CountryId);
                cmd.Parameters.AddWithValue("prm_cityId", _doc.CityId);
                cmd.Parameters.AddWithValue("prm_areaId", _doc.AreaId);
                cmd.Parameters.AddWithValue("prm_defApptDur", _doc.DefApptDur);
                cmd.Parameters.AddWithValue("prm_doctorName", _doc.DoctorName);
                cmd.Parameters.AddWithValue("prm_dateOfBirth", _doc.DateOfBirth);
                cmd.Parameters.AddWithValue("prm_genderId", _doc.GenderId);
                cmd.Parameters.AddWithValue("prm_startTime", _doc.StartTime);
                cmd.Parameters.AddWithValue("prm_contactNo", _doc.ContactNo);
                cmd.Parameters.AddWithValue("prm_houseNo", _doc.HouseNo);
                cmd.Parameters.AddWithValue("prm_address", _doc.Address);
                cmd.Parameters.AddWithValue("prm_specialization", _doc.Specialization);
                cmd.Parameters.AddWithValue("prm_createdOn", _doc.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", _doc.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", _doc.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", _doc.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", _doc.IsActive);
                cmd.Parameters.AddWithValue("prm_DbOperation", _doc.DBoperation.ToString());
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
            }
        }
        public List<DoctorDE> SearchData(string WhereClause, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            List<DoctorDE> doc = new List<DoctorDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                doc = cmd.Connection.Query<DoctorDE>("call "+SPNames.PMS_Search_Doctor.ToString()+"('" + WhereClause + "')").ToList();
                return doc;
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
