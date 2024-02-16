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
                cmd.CommandText = "ManageStaff";
                cmd.Parameters.AddWithValue ("id", _staff.Id);
                cmd.Parameters.AddWithValue ("clientId", _staff.ClientId);
                cmd.Parameters.AddWithValue ("userId", _staff.UserId);
                cmd.Parameters.AddWithValue ("cityId", _staff.CityId);
                cmd.Parameters.AddWithValue ("countryId", _staff.CountryId);
                cmd.Parameters.AddWithValue ("areaId", _staff.AreaId);
                cmd.Parameters.AddWithValue ("name", _staff.Name);
                cmd.Parameters.AddWithValue ("dateOfBirth", _staff.DateOfBirth);
                cmd.Parameters.AddWithValue ("genderId", _staff.GenderId);
                cmd.Parameters.AddWithValue ("contactNo", _staff.ContactNo);
                cmd.Parameters.AddWithValue ("houseNo", _staff.HouseNo);
                cmd.Parameters.AddWithValue ("address", _staff.Address);
                cmd.Parameters.AddWithValue ("createdOn", _staff.CreatedOn);
                cmd.Parameters.AddWithValue ("createdById", _staff.CreatedById);
                cmd.Parameters.AddWithValue ("modifiedOn", _staff.ModifiedOn);
                cmd.Parameters.AddWithValue ("modifiedById", _staff.ModifiedById);
                cmd.Parameters.AddWithValue ("isActive", _staff.IsActive);
                cmd.Parameters.AddWithValue ("DbOperation", _staff.DBoperation.ToString ());
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
            List<StaffDE> doc = new List<StaffDE> ();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                doc = cmd.Connection.Query<StaffDE> ("call QST.MicroERP.SearchStaff('" + WhereClause + "')").ToList ();
                return doc;
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
