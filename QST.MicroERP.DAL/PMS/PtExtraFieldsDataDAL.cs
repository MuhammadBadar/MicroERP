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
    public class PtExtraFieldsDataDAL
    {
        #region Operations
        public bool ManagePtExtraFieldsData ( PtExtraFieldsDataDE _fieData, MySqlCommand cmd = null )
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
                cmd.CommandText = SPNames.PMS_Manage_PTExtraFieldsData.ToString();
                //cmd.Parameters.AddWithValue ("@id", _fieData.Id);
                cmd.Parameters.AddWithValue ("@prm_patientId", _fieData.PatientId);
                cmd.Parameters.AddWithValue ("@prm_fieldId", _fieData.FieldId);
                cmd.Parameters.AddWithValue ("@prm_clientId", _fieData.ClientId);
                cmd.Parameters.AddWithValue ("@prm_fieldValue", _fieData.FieldValue);
                cmd.Parameters.AddWithValue ("@prm_createdOn", _fieData.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", _fieData.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", _fieData.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", _fieData.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", _fieData.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", _fieData.DBoperation.ToString ());

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
        public List<PtExtraFieldsDataDE> SearchPtExtraFieldsData ( string whereClause, MySqlCommand cmd = null )
        {
            List<PtExtraFieldsDataDE> top = new List<PtExtraFieldsDataDE> ();
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
                top = cmd.Connection.Query<PtExtraFieldsDataDE> ("call"+SPNames.PMS_Search_PTExtraFieldsData.ToString()+"( '" + whereClause + "')").ToList ();
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
