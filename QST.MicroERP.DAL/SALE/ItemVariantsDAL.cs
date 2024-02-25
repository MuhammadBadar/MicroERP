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
    public class ItemVariantsDAL
    {
        #region Operations
        public bool ManageItemVariants ( ItemVariantsDE itemVar, MySqlCommand? cmd  )
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
                cmd.CommandText = "ManageItemVariants";
                cmd.Parameters.AddWithValue ("@id", itemVar.Id);
                cmd.Parameters.AddWithValue ("@clientId", itemVar.ClientId);
                cmd.Parameters.AddWithValue ("@itemId", itemVar.ItemId);
                cmd.Parameters.AddWithValue ("@attributeValuesIds", itemVar.AttributeValuesIds);
                cmd.Parameters.AddWithValue ("@saleExtraRate", itemVar.SaleExtraRate);
                cmd.Parameters.AddWithValue ("@purchaseExtraRate", itemVar.PurchaseExtraRate);
                cmd.Parameters.AddWithValue ("@barCode", itemVar.BarCode);
                cmd.Parameters.AddWithValue ("@stockValue", itemVar.StockValue);
                cmd.Parameters.AddWithValue ("@createdOn", itemVar.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", itemVar.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", itemVar.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", itemVar.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", itemVar.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", itemVar.DBoperation.ToString ());

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
        public List<ItemVariantsDE> SearchItemVariants ( string whereClause, MySqlCommand cmd = null )
        {
            List<ItemVariantsDE> top = new List<ItemVariantsDE> ();
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
                top = cmd.Connection.Query<ItemVariantsDE> ("call SearchItemVariants( '" + whereClause + "')").ToList ();
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
        public int GetUsedCount ( int mod )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "ItemVariantUsedCount");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@id", mod
                    ));    
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return retVal;
        }
        #endregion
    }
}
