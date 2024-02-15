using Dapper;
using MicroERP.Core.Entities.CTL;
using MicroERP.Core.ViewModel;
using MySql.Data.MySqlClient;
using System.Data;

namespace MicroERP.DAL.CTL
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
                cmd.CommandText = "ManageLogEvent";
                cmd.Parameters.AddWithValue("id", LogEvent.Id);
                cmd.Parameters.AddWithValue("userId", LogEvent.UserId);
                cmd.Parameters.AddWithValue("inTime", LogEvent.InTime);
                cmd.Parameters.AddWithValue("outTime", LogEvent.OutTime);
                cmd.Parameters.AddWithValue("date", LogEvent.Date);
                cmd.Parameters.AddWithValue("message", LogEvent.Message);
                cmd.Parameters.AddWithValue("createdOn", LogEvent.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", LogEvent.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", LogEvent.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", LogEvent.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", LogEvent.IsActive);
                cmd.Parameters.AddWithValue("DBoperation", LogEvent.DBoperation.ToString());
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
                top = cmd.Connection.Query<LogEventDE>("call MicroERP.SearchLogEvent( '" + whereClause + "')").ToList();
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
