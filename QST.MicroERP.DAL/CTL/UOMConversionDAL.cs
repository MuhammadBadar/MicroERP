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
    public class UOMConversionDAL
    {
        #region Operations
        public bool ManageUOMConversion ( UOMConversionDE uomCon, MySqlCommand? cmd = null )
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
                cmd.CommandText = "ManageUOMConversion";
                cmd.Parameters.AddWithValue ("@id", uomCon.Id);
                cmd.Parameters.AddWithValue ("@uOMId", uomCon.UOMId);
                cmd.Parameters.AddWithValue ("@convertedUOMId", uomCon.ConvertedUOMId);
                cmd.Parameters.AddWithValue ("@isBaseUnit", uomCon.IsBaseUnit);
                cmd.Parameters.AddWithValue ("@qty", uomCon.Qty);
                cmd.Parameters.AddWithValue ("@multiplier", uomCon.Multiplier);
                cmd.Parameters.AddWithValue ("@displayUOM", uomCon.DisplayUOM);
                cmd.Parameters.AddWithValue ("@createdOn", uomCon.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", uomCon.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", uomCon.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", uomCon.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", uomCon.IsActive);
                cmd.Parameters.AddWithValue("@clientId", uomCon.ClientId);
                cmd.Parameters.AddWithValue ("@DBoperation", uomCon.DBoperation.ToString ());

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
        public List<UOMConversionDE> SearchUOMConversion ( string whereClause, MySqlCommand cmd = null )
        {
            List<UOMConversionDE> top = new List<UOMConversionDE> ();
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
                top = cmd.Connection.Query<UOMConversionDE> ("call QST.MicroERP.SearchUOMConversion( '" + whereClause + "')").ToList ();
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
