using Dapper;
using QST.MicroERP.Core.Entities.LMS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL.LMS
{
    public class CityStudentDAL
    {
        #region DbOperations
        public bool ManageCityStudent(CityStudentDE _ctystd, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageCityStudent";
                cmd.Parameters.AddWithValue("id", _ctystd.Id);
                cmd.Parameters.AddWithValue("cityId", _ctystd.CityId);
                cmd.Parameters.AddWithValue("studentId", _ctystd.StudentId);
                cmd.Parameters.AddWithValue("createdOn", _ctystd.CreatedOn);
                cmd.Parameters.AddWithValue("createdBy", _ctystd.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", _ctystd.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedBy", _ctystd.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", _ctystd.IsActive);
                cmd.Parameters.AddWithValue("DbOperation", _ctystd.DBoperation.ToString());
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
        public List<CityStudentDE> SearchCityStudent(string WhereClause, MySqlCommand cmd)
        {
            bool closeConnection = false;
            //WhereClause = string.Empty;
            List<CityStudentDE> lec = new List<CityStudentDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                lec = cmd.Connection.Query<CityStudentDE>("call SearchCityStudent('" + WhereClause + "')").ToList();
                return lec;
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
