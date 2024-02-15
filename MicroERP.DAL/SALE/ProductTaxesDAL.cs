using Dapper;
using MySql.Data.MySqlClient;
using MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroERP.Core.ViewModel;

namespace MicroERP.DAL
{
    public class ProductTaxesDAL
    {
        #region Operations
        public bool ManageProductTaxes ( ProductTaxesDE proTax, MySqlCommand? cmd = null )
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                cmd.CommandText = "ManageProductTaxes";
                cmd.Parameters.AddWithValue ("@id", proTax.Id);
                cmd.Parameters.AddWithValue ("@clientId", proTax.ClientId);
                cmd.Parameters.AddWithValue ("@productId", proTax.ProductId);
                cmd.Parameters.AddWithValue ("@taxId", proTax.TaxId);
                cmd.Parameters.AddWithValue ("@amount", proTax.Amount);
                cmd.Parameters.AddWithValue ("@isVariant", proTax.IsVariant);
                cmd.Parameters.AddWithValue ("@createdOn", proTax.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", proTax.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", proTax.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", proTax.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", proTax.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", proTax.DBoperation.ToString ());

                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
                cmd.Parameters.Clear ();
            }
        }
        public List<ProductTaxesDE> SearchProductTaxes ( string whereClause, MySqlCommand cmd = null )
        {
            List<ProductTaxesDE> top = new List<ProductTaxesDE> ();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                top = cmd.Connection.Query<ProductTaxesDE> ("call MicroERP.SearchProductTaxes( '" + whereClause + "')").ToList ();
                return top;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<ProductWithVariantsVM> SearchItemswithVariants ( string whereClause, MySqlCommand? cmd = null )
        {
            List<ProductWithVariantsVM> top = new List<ProductWithVariantsVM> ();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                top = cmd.Connection.Query<ProductWithVariantsVM> ("call MicroERP.GetProductwithVariants( '" + whereClause + "')").ToList ();
                return top;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        #endregion
    }
}
