using Dapper;
using MySql.Data.MySqlClient;
using MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL
{
    public class ItemUOMDAL
    {
        #region Operations
        public bool ManageItemUOM ( ItemUOMDE itmUom, MySqlCommand? cmd=null )
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
                cmd.CommandText = "ManageItemUOM";
                cmd.Parameters.AddWithValue ("@id", itmUom.Id);
                cmd.Parameters.AddWithValue ("@clientId", itmUom.ClientId);
                cmd.Parameters.AddWithValue ("@uOMId", itmUom.UOMId);
                cmd.Parameters.AddWithValue ("@uOMTypeId", itmUom.UOMTypeId);
                cmd.Parameters.AddWithValue ("@itemId", itmUom.ItemId);
                cmd.Parameters.AddWithValue ("@purPrice", itmUom.PurPrice);
                cmd.Parameters.AddWithValue ("@salePrice", itmUom.SalePrice);
                cmd.Parameters.AddWithValue ("@createdOn", itmUom.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", itmUom.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", itmUom.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", itmUom.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", itmUom.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", itmUom.DBoperation.ToString ());

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
        public List<ItemUOMDE> SearchItemUOM ( string whereClause, MySqlCommand cmd = null )
        {
            List<ItemUOMDE> top = new List<ItemUOMDE> ();
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
                top = cmd.Connection.Query<ItemUOMDE> ("call MicroERP.SearchItemUOM( '" + whereClause + "')").ToList ();
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
