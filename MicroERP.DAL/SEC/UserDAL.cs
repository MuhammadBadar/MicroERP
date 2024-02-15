using Dapper;
using MicroERP.Core.Entities.SEC;
using MySql.Data.MySqlClient;
using System.Data;


namespace MicroERP.DAL.SEC
{
    public class UserDAL
    {
        public List<UserDE> SearchUser ( string whereClause, MySqlCommand cmd = null )
        {
            List<UserDE> top = new List<UserDE> ();
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
                top = cmd.Connection.Query<UserDE> ("call MicroERP.SearchUser( '" + whereClause + "')").ToList ();
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
    }
}
