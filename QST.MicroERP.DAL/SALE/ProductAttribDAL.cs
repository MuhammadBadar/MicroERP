using Dapper;
using MySql.Data.MySqlClient;
using QST.MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL
{
    public class ProductAttribDAL
    {
        #region Operations
        public bool ManageProductAttrib(ProductAttribDE proAtt, MySqlCommand cmd )
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
                cmd.CommandText = "ManageProductAttrib";
                cmd.Parameters.AddWithValue("@id", proAtt.Id);
                cmd.Parameters.AddWithValue ("@clientId", proAtt.ClientId);
                cmd.Parameters.AddWithValue("@productId", proAtt.ProductId);
                cmd.Parameters.AddWithValue("@attribId", proAtt.AttribId);
                cmd.Parameters.AddWithValue("@attribValId", proAtt.AttribValId);
                cmd.Parameters.AddWithValue("@purRate", proAtt.PurRate);
                cmd.Parameters.AddWithValue("@saleRate", proAtt.SaleRate);
                cmd.Parameters.AddWithValue("@createdOn", proAtt.CreatedOn);
                cmd.Parameters.AddWithValue("@createdById", proAtt.CreatedById);
                cmd.Parameters.AddWithValue("@modifiedOn", proAtt.ModifiedOn);
                cmd.Parameters.AddWithValue("@modifiedById", proAtt.ModifiedById);
                cmd.Parameters.AddWithValue("@isActive", proAtt.IsActive);
                cmd.Parameters.AddWithValue("@DBoperation", proAtt.DBoperation.ToString());

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
                cmd.Parameters.Clear();
            }
        }
        public List<ProductAttribDE> SearchProductAttrib(string whereClause, MySqlCommand cmd = null)
        {
            List<ProductAttribDE> top = new List<ProductAttribDE>();
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
                top = cmd.Connection.Query<ProductAttribDE>("call QST.MicroERP.SearchProductAttrib( '" + whereClause + "')").ToList();
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
