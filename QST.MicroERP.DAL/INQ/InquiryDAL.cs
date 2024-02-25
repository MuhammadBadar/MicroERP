using Dapper;
using QST.MicroERP.Core.Entities.INQ;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL.INQ
{
    public class InquiryDAL
    {
        #region Inquiry Operations

        public bool ManageInquiry(InquiryDE Inquiry, MySqlCommand cmd = null)
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
                cmd.CommandText = "ManageInquiry";
                cmd.Parameters.AddWithValue("@id", Inquiry.Id);
                cmd.Parameters.AddWithValue("@name", Inquiry.Name);
                cmd.Parameters.AddWithValue("@cell", Inquiry.Cell);
                cmd.Parameters.AddWithValue("@email", Inquiry.Email);
                cmd.Parameters.AddWithValue("@servicesId", Inquiry.ServicesId);
                cmd.Parameters.AddWithValue("@comments", Inquiry.Comments);
                cmd.Parameters.AddWithValue("@createdOn", Inquiry.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", Inquiry.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", Inquiry.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", Inquiry.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", Inquiry.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", Inquiry.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<InquiryDE> SearchInquirys(string whereClause, MySqlCommand cmd = null)
        {
            List<InquiryDE> top = new List<InquiryDE>();
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
                top = cmd.Connection.Query<InquiryDE>("call SearchInquiry( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception exp)
            {
                return top;
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
