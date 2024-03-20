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
    public class StaffDAL
    {
        #region DbOperations
        public bool ManageStaff ( StaffDE _staff, MySqlCommand? cmd )
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                cmd.CommandText = SPNames.PMS_Manage_Staff.ToString();
                cmd.Parameters.AddWithValue ("prm_id", _staff.Id);
                cmd.Parameters.AddWithValue ("prm_clientId", _staff.ClientId);
                cmd.Parameters.AddWithValue ("prm_userId", _staff.UserId);
                cmd.Parameters.AddWithValue ("prm_cityId", _staff.CityId);
                cmd.Parameters.AddWithValue ("prm_countryId", _staff.CountryId);
                cmd.Parameters.AddWithValue ("prm_areaId", _staff.AreaId);
                cmd.Parameters.AddWithValue ("prm_name", _staff.Name);
                cmd.Parameters.AddWithValue ("prm_dateOfBirth", _staff.DateOfBirth);
                cmd.Parameters.AddWithValue ("prm_genderId", _staff.GenderId);
                cmd.Parameters.AddWithValue ("prm_contactNo", _staff.ContactNo);
                cmd.Parameters.AddWithValue ("prm_houseNo", _staff.HouseNo);
                cmd.Parameters.AddWithValue ("prm_address", _staff.Address);
                cmd.Parameters.AddWithValue ("prm_createdOn", _staff.CreatedOn);
                cmd.Parameters.AddWithValue ("prm_createdById", _staff.CreatedById);
                cmd.Parameters.AddWithValue ("prm_modifiedOn", _staff.ModifiedOn);
                cmd.Parameters.AddWithValue ("prm_modifiedById", _staff.ModifiedById);
                cmd.Parameters.AddWithValue ("prm_isActive", _staff.IsActive);
                cmd.Parameters.AddWithValue ("prm_DbOperation", _staff.DBoperation.ToString ());
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
        public List<StaffDE> SearchStaff ( string WhereClause, MySqlCommand cmd )
        {
            bool closeConnection = false;
            List<StaffDE> staff = new List<StaffDE> ();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                staff = cmd.Connection.Query<StaffDE> ("call "+SPNames.PMS_Search_Staff.ToString()+"('" + WhereClause + "')").ToList ();
                return staff;
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
