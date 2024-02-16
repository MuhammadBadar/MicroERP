﻿using Dapper;
using QST.MicroERP.Core.Entities.LMS;
using MySql.Data.MySqlClient;
using System.Data;

namespace QST.MicroERP.DAL.LMS
{
    public class SchoolDAL
    {
        #region Operations
        public bool ManageSchool(SchoolDE _school, MySqlCommand cmd = null)
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                cmd.CommandText = "ManageSchool";
                cmd.Parameters.AddWithValue("@id", _school.Id);
                cmd.Parameters.AddWithValue("@name", _school.Name);
                cmd.Parameters.AddWithValue("@address", _school.Address);
                cmd.Parameters.AddWithValue("@contactPerson", _school.ContactPerson);
                cmd.Parameters.AddWithValue("@cellNo", _school.CellNo);
                cmd.Parameters.AddWithValue("@createdOn", _school.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", _school.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", _school.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", _school.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", _school.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", _school.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<SchoolDE> SearchSchool(string whereClause, MySqlCommand cmd = null)
        {
            List<SchoolDE> top = new List<SchoolDE>();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                top = cmd.Connection.Query<SchoolDE>("call QST.MicroERP.SearchSchool( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        #endregion
    }
}