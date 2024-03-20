using Dapper;
using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.ViewModel;
using MySql.Data.MySqlClient;
using System.Data;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL.CTL
{
    public class LogEventDAL
    {
        public bool ManageLogEvent(LogEventDE LogEvent, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = SPNames.CTL_Manage_LogEvent.ToString ();
                cmd.Parameters.AddWithValue("prm_id", LogEvent.Id);
                cmd.Parameters.AddWithValue("prm_clientId", LogEvent.ClientId);
                cmd.Parameters.AddWithValue("prm_userId", LogEvent.UserId);
                cmd.Parameters.AddWithValue("prm_inTime", LogEvent.InTime);
                cmd.Parameters.AddWithValue("prm_outTime", LogEvent.OutTime);
                cmd.Parameters.AddWithValue("prm_date", LogEvent.Date);
                cmd.Parameters.AddWithValue("prm_message", LogEvent.Message);
                cmd.Parameters.AddWithValue("prm_createdOn", LogEvent.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", LogEvent.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", LogEvent.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", LogEvent.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", LogEvent.IsActive);
                cmd.Parameters.AddWithValue("prm_DBoperation", LogEvent.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<LogEventDE> SearchLogEvent(string whereClause, MySqlCommand cmd = null)
        {

            List<LogEventDE> top = new List<LogEventDE>();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                top = cmd.Connection.Query<LogEventDE>("call "+SPNames.CTL_Search_LogEvent.ToString () + "( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
    }
}
