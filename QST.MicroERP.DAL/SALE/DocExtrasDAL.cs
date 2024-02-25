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
    public class DocExtrasDAL
    {
        #region Operations
        public bool ManageDocExtras ( DocExtrasDE docExt, MySqlCommand? cmd = null )
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
                cmd.CommandText = "ManageDocumentExtras";
                cmd.Parameters.AddWithValue ("@id", docExt.Id);
                cmd.Parameters.AddWithValue ("@docExtraId", docExt.DocExtraId);
                cmd.Parameters.AddWithValue ("@docExtraTypeId", docExt.DocExtraTypeId);
                cmd.Parameters.AddWithValue ("@incDecTypeId", docExt.IncDecTypeId);
                cmd.Parameters.AddWithValue ("@formulaId", docExt.FormulaId);
                cmd.Parameters.AddWithValue ("@value", docExt.Value);
                cmd.Parameters.AddWithValue ("@createdOn", docExt.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", docExt.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", docExt.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", docExt.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", docExt.IsActive);
                cmd.Parameters.AddWithValue("@clientId", docExt.ClientId);
                cmd.Parameters.AddWithValue ("@DBoperation", docExt.DBoperation.ToString ());

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
        public List<DocExtrasDE> SearchDocExtras ( string whereClause, MySqlCommand cmd = null )
        {
            List<DocExtrasDE> top = new List<DocExtrasDE> ();
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
                top = cmd.Connection.Query<DocExtrasDE> ("call SearchDocumentExtras( '" + whereClause + "')").ToList ();
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
