using Dapper;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace MicroERP.DAL.CTL
{
    public class CoreDAL
    {
        public int GetnextId ( string mod )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxId");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", mod.ToString ()
                    ));
                retVal += 1;
                //if (retVal != -1)
                //    retVal += 1;
                //else     
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
        public int GetNextTokenNo ( DateTime Date, int clientId )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxTokenNo");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_Date", Date.ToString ("yyMMdd"), "@prm_clientId", clientId
                    ));
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString ("yyMMdd");
                if (retVal > 0)
                {
                    var maxDate = retVal.ToString ().Substring (0, 6);
                    var id = int.Parse (retVal.ToString ().Substring (6));
                    //if (maxDate == formattedDate)
                    return int.Parse (retVal.ToString ().Substring (0, 6) + (id + 1).ToString ());
                    //else
                    //    return int.Parse (formattedDate + 1.ToString ());
                }
                else
                    return int.Parse (formattedDate + 1.ToString ());
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
        public int? GetNextTokenno ( AppointmentDE appt )
        {
            int? retVal = 0;
            retVal = appt.TokenNo;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                DateTime currentDate = DateTime.Now;
                string formattedDate = appt.Date.ToString ("yyMMdd");
                if (retVal > 0)
                {
                    var maxDate = retVal?.ToString ().Substring (0, 6);
                    var id = int.Parse (retVal?.ToString ().Substring (6));
                    return int.Parse (retVal?.ToString ().Substring (0, 6) + (id + 1).ToString ());
                }
                else
                    return int.Parse (formattedDate + 1.ToString ());
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
        public int GetMaxId ( string mod )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxId");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", mod.ToString ()
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
        public string GetnextVchNo ( string mod, int clientId )
        {
            string? retVal = null;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine ("Connection  has been created");
                else
                    Console.WriteLine ("Connection error");
                cmd.CommandText = "GetNextVchNo";
                cmd.Parameters.AddWithValue ("@VchKeyCode", mod);
                cmd.Parameters.AddWithValue ("@clientId", clientId);
                cmd.Parameters.Add (new MySqlParameter ("@MaxVchNo", MySqlDbType.VarChar, 200));
                cmd.Parameters["@MaxVchNo"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery ();
                retVal = cmd.Parameters["@MaxVchNo"].Value.ToString ();

                //cmd = MicroERPDataContext.SetStoredProcedure(cmd, "GetNextVchNo");
                //retVal = MicroERPDataContext.ExecuteScalarString(MicroERPDataContext.AddParameters(cmd
                //    , "@VchKeyCode", "'"+mod +"'",  "@clientId", clientId));
                var numVal = 0;

                if (retVal != null && retVal != "")
                {
                    String numStr = Regex.Match (retVal, @"\d+\.*\d*").Value;
                    numVal = int.Parse (numStr);
                    numVal = numVal + 1;
                }
                else
                    numVal = 1;
                retVal = mod + numVal;
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
        public int GetnextLineId ( string mod, int headerId, string columnName )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxLineId");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", mod.ToString (),
                                "@prm_HeaderId", headerId,
                                "@prm_ColumnName", columnName
                    ));
                retVal += 1;
                //if (retVal != -1)
                //    retVal += 1;
                //else     
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
        public int GetNextIdByClient ( string tblname, int headerId, string columnName )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxIdByClient");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", tblname.ToString (),
                                "@prm_HeaderId", headerId,
                                "@prm_ColumnName", columnName
                    ));
                retVal += 1;
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
        public int GetNextLineIdByClt ( string tblname, string columnName, int headerId, int clientId )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxLineIdByClt");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", tblname.ToString (),
                                "@prm_HeaderId", headerId,
                                "@prm_ClientId", clientId,
                                "@prm_ColumnName", columnName
                    ));
                retVal += 1;
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
        public int GetMaxIdByClient ( string tblname, int headerId, string columnName )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxIdByClient");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", tblname.ToString (),
                                "@prm_HeaderId", headerId,
                                "@prm_ColumnName", columnName
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
        public int GetMaxLineIdByClt ( string tblname, string columnName, int? headerId, int clientId )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxLineIdByClt");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", tblname.ToString (),
                                "@prm_HeaderId", headerId,
                                "@prm_ClientId", clientId,
                                "@prm_ColumnName", columnName
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

        public int GetMaxLineId ( string mod, int headerId, string columnName )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxLineId");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", mod.ToString (),
                                "@prm_HeaderId", headerId,
                                "@prm_ColumnName", columnName
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
        public int GetMaxVariantId ( string tblname, string columnName, int headerId, int clientId )
        {
            int retVal = 0;
            MySqlCommand cmd = MicroERPDataContext.OpenMySqlConnection ();
            try
            {
                cmd = MicroERPDataContext.SetStoredProcedure (cmd, "GetMaxLineIdByClt");
                retVal = MicroERPDataContext.ExecuteScalar (MicroERPDataContext.AddParameters (cmd
                    , "@prm_TableName", tblname.ToString (),
                                "@prm_HeaderId", headerId,
                                "@prm_ClientId", clientId,
                                "@prm_ColumnName", columnName
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
    }

}
