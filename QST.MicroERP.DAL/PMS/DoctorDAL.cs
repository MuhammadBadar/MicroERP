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
                cmd.CommandText = "ManageDoctor";
                cmd.Parameters.AddWithValue("id", _doc.Id);
                cmd.Parameters.AddWithValue ("clientId", _doc.ClientId);
                cmd.Parameters.AddWithValue ("userId", _doc.UserId);
                cmd.Parameters.AddWithValue ("countryId", _doc.CountryId);
                cmd.Parameters.AddWithValue ("cityId", _doc.CityId);
                cmd.Parameters.AddWithValue ("areaId", _doc.AreaId);
                cmd.Parameters.AddWithValue ("defApptDur", _doc.DefApptDur);
                cmd.Parameters.AddWithValue("doctorName", _doc.DoctorName);
                cmd.Parameters.AddWithValue("dateOfBirth", _doc.DateOfBirth);
                cmd.Parameters.AddWithValue("genderId", _doc.GenderId);
                cmd.Parameters.AddWithValue ("startTime", _doc.StartTime);
                cmd.Parameters.AddWithValue("contactNo", _doc.ContactNo);
                cmd.Parameters.AddWithValue("houseNo", _doc.HouseNo);
                cmd.Parameters.AddWithValue("address", _doc.Address);
                cmd.Parameters.AddWithValue("specialization", _doc.Specialization);
                cmd.Parameters.AddWithValue("createdOn", _doc.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", _doc.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", _doc.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", _doc.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", _doc.IsActive);
                cmd.Parameters.AddWithValue("DbOperation", _doc.DBoperation.ToString());
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
                doc = cmd.Connection.Query<DoctorDE>("call SearchDoctor('" + WhereClause + "')").ToList();
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
        #region DbOperations
        public bool ManageDoctor ( DoctorDE _doc, MySqlCommand? cmd )
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageDoctor";
                cmd.Parameters.AddWithValue ("id", _doc.Id);
                cmd.Parameters.AddWithValue ("userId", _doc.UserId);
                cmd.Parameters.AddWithValue ("cityId", _doc.CityId);
                cmd.Parameters.AddWithValue ("areaId", _doc.AreaId);
                cmd.Parameters.AddWithValue ("doctorName", _doc.DoctorName);
                cmd.Parameters.AddWithValue ("dateOfBirth", _doc.DateOfBirth);
                cmd.Parameters.AddWithValue ("genderId", _doc.GenderId);
                cmd.Parameters.AddWithValue ("contactNo", _doc.ContactNo);
                cmd.Parameters.AddWithValue ("houseNo", _doc.HouseNo);
                cmd.Parameters.AddWithValue ("address", _doc.Address);
                cmd.Parameters.AddWithValue ("specialization", _doc.Specialization);
                cmd.Parameters.AddWithValue ("createdOn", _doc.CreatedOn);
                cmd.Parameters.AddWithValue ("createdById", _doc.CreatedById);
                cmd.Parameters.AddWithValue ("modifiedOn", _doc.ModifiedOn);
                cmd.Parameters.AddWithValue ("modifiedById", _doc.ModifiedById);
                cmd.Parameters.AddWithValue ("isActive", _doc.IsActive);
                cmd.Parameters.AddWithValue ("DbOperation", _doc.DBoperation.ToString ());
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
        public List<DoctorDE> SearchDoctor ( string WhereClause, MySqlCommand cmd )
        {
            bool closeConnection = false;
            //WhereClause = string.Empty;
            List<DoctorDE> doc = new List<DoctorDE> ();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnection = true;
                }
                doc = cmd.Connection.Query<DoctorDE> ("call SearchDoctor('" + WhereClause + "')").ToList ();
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
