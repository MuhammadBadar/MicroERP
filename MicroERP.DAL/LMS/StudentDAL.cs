﻿using Dapper;
using MicroERP.Core.Entities.LMS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL.LMS
{
    public class StudentDAL
    {

        #region Operations

        public bool ManageStudent(StudentDE std, MySqlCommand? cmd)
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
                cmd.CommandText = "Manage_Student";
                cmd.Parameters.AddWithValue("@prm_Id", std.Id);
                cmd.Parameters.AddWithValue("@prm_CityId", std.CityId);
                cmd.Parameters.AddWithValue("@prm_City", std.City);
                cmd.Parameters.AddWithValue("@prm_CellNo", std.CellNo);
                cmd.Parameters.AddWithValue("@prm_Name", std.Name);
                cmd.Parameters.AddWithValue("@prm_Email", std.Email);
                cmd.Parameters.AddWithValue("@prm_CreatedOn", std.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_CreatedBy", std.CreatedById);
                cmd.Parameters.AddWithValue("@prm_ModifiedOn", std.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_ModifiedBy", std.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_IsActive", std.IsActive);
                cmd.Parameters.AddWithValue("@prm_DBoperation", std.DBoperation.ToString());
                cmd.Parameters.AddWithValue("@prm_Filter", std.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public bool AlterStudent(StudentDE std, int? Id = null, MySqlCommand cmd = null)
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
                cmd.CommandText = "AlterStudent";
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@DBoperation", std.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<StudentDE> SearchStudent(string whereClause, MySqlCommand cmd = null)
        {
            List<StudentDE> top = new List<StudentDE>();
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
                top = cmd.Connection.Query<StudentDE>("call MicroERP.SearchStudent( '" + whereClause + "')").ToList();
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