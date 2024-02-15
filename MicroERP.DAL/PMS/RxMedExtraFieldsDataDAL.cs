using Dapper;
using MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL
{
    public class RxMedExtraFieldsDataDAL
    {
        #region Operations
        public bool ManageRxMedExtraFieldsData ( RxMedExtraFieldsDataDE _rxmefData, MySqlCommand cmd = null )
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
                cmd.CommandText = "ManageRxMedExtraFieldsData";
                //cmd.Parameters.AddWithValue ("@id", _rxmefData.Id);
                cmd.Parameters.AddWithValue ("@rxId", _rxmefData.RxId);
                cmd.Parameters.AddWithValue ("@clientId", _rxmefData.ClientId);
                cmd.Parameters.AddWithValue ("@fieldId", _rxmefData.FieldId);
                cmd.Parameters.AddWithValue ("@fieldValue", _rxmefData.FieldValue);
                cmd.Parameters.AddWithValue ("@createdOn", _rxmefData.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", _rxmefData.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", _rxmefData.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", _rxmefData.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", _rxmefData.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", _rxmefData.DBoperation.ToString ());

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
        public List<RxMedExtraFieldsDataDE> SearchRxMedExtraFieldsData ( string whereClause, MySqlCommand cmd = null )
        {
            List<RxMedExtraFieldsDataDE> top = new List<RxMedExtraFieldsDataDE> ();
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
                top = cmd.Connection.Query<RxMedExtraFieldsDataDE> ("call MicroERP.SearchRxMedExtraFieldsData( '" + whereClause + "')").ToList ();
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
