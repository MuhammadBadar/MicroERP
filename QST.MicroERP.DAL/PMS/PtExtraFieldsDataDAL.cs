using Dapper;
using QST.MicroERP.Core.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                cmd.CommandText = "ManagePtExtraFieldsData";
                //cmd.Parameters.AddWithValue ("@id", _fieData.Id);
                cmd.Parameters.AddWithValue ("@patientId", _fieData.PatientId);
                cmd.Parameters.AddWithValue ("@fieldId", _fieData.FieldId);
                cmd.Parameters.AddWithValue ("clientId", _fieData.ClientId);
                cmd.Parameters.AddWithValue ("@fieldValue", _fieData.FieldValue);
                cmd.Parameters.AddWithValue ("@createdOn", _fieData.CreatedOn);
                cmd.Parameters.AddWithValue ("@createdById", _fieData.CreatedById);
                cmd.Parameters.AddWithValue ("@modifiedOn", _fieData.ModifiedOn);
                cmd.Parameters.AddWithValue ("@modifiedById", _fieData.ModifiedById);
                cmd.Parameters.AddWithValue ("@isActive", _fieData.IsActive);
                cmd.Parameters.AddWithValue ("@DBoperation", _fieData.DBoperation.ToString ());

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
                top = cmd.Connection.Query<PtExtraFieldsDataDE> ("call QST.MicroERP.SearchPtExtraFieldsData( '" + whereClause + "')").ToList ();
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
        public List<Object> GetPtExtraFieldsData ( int TypeId, MySqlCommand cmd = null )
        {
            List<Object> ds = new List<Object> ();
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
                ds = cmd.Connection.Query<Object> ("call QST.MicroERP.GetExtraFieldsData( " + TypeId + ")").ToList ();
                return ds;
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
