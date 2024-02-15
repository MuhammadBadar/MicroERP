﻿using Dapper;
using MicroERP.Core.Entities.LMS;
using MySql.Data.MySqlClient;
using System.Data;

namespace MicroERP.DAL.LMS
{
    public class BranchschoolDAL
    {
        #region Operations
        public bool ManageBranchschool(BranchschoolDE _branchschool, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageBranchschool";
                cmd.Parameters.AddWithValue("@id", _branchschool.Id);
                cmd.Parameters.AddWithValue("@schoolId", _branchschool.SchoolId);
                cmd.Parameters.AddWithValue("@name", _branchschool.Name);
                cmd.Parameters.AddWithValue("@address", _branchschool.Address);
                cmd.Parameters.AddWithValue("@contactPerson", _branchschool.ContactPerson);
                cmd.Parameters.AddWithValue("@cellNo", _branchschool.CellNo);
                cmd.Parameters.AddWithValue("@createdOn", _branchschool.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", _branchschool.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", _branchschool.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", _branchschool.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", _branchschool.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", _branchschool.DBoperation.ToString());

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
        public List<BranchschoolDE> SearchBranchschool(string whereClause, MySqlCommand cmd = null)
        {
            List<BranchschoolDE> top = new List<BranchschoolDE>();
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
                top = cmd.Connection.Query<BranchschoolDE>("call MicroERP.SearchBranchschool( '" + whereClause + "')").ToList();
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