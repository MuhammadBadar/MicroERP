using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL
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
                cmd.CommandText = SPNames.PMS_Manage_RxExtraFieldsData.ToString();
                //cmd.Parameters.AddWithValue ("@id", _rxmefData.Id);
                cmd.Parameters.AddWithValue ("@prm_rxId", _rxmefData.RxId);
                cmd.Parameters.AddWithValue ("@prm_clientId", _rxmefData.ClientId);
                cmd.Parameters.AddWithValue ("@prm_fieldId", _rxmefData.FieldId);
                cmd.Parameters.AddWithValue ("@prm_fieldValue", _rxmefData.FieldValue);
                cmd.Parameters.AddWithValue ("@prm_createdOn", _rxmefData.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", _rxmefData.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", _rxmefData.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", _rxmefData.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", _rxmefData.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", _rxmefData.DBoperation.ToString ());

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
                top = cmd.Connection.Query<RxMedExtraFieldsDataDE> ("call "+SPNames.PMS_Search_RxExtraFieldsData.ToString()+"( '" + whereClause + "')").ToList ();
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
