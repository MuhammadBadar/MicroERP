using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace MicroERP.DAL
{
    public class MicroERPDataContext
    {
        public readonly IConfiguration? _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        public MicroERPDataContext()
        {

        }

        public static MySqlCommand OpenMySqlConnection()
        {
            try
            {
                MicroERPDataContext res = new MicroERPDataContext();
                MySqlConnection con = new MySqlConnection(res._config.GetConnectionString("ConnStr"));

                MySqlCommand cmd = new MySqlCommand()
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure,
                    Connection = con
                };
                con.Open();
                return cmd;
            }
            catch (Exception )
            {
                throw ;
            }
           
        }

        //      public static MySqlCommand OpenMySqlConnection()
        //{
        //	try
        //	{
        //		MySqlConnection con = new MySqlConnection("Server = localhost; Database = MicroERP; Uid = root; Pwd = Ss3s3Ss3s3;");
        //		MySqlCommand cmd = new MySqlCommand()
        //		{
        //			CommandTimeout = 0,
        //			CommandType = CommandType.StoredProcedure,
        //			Connection = con
        //		};
        //		con.Open();
        //		return cmd;
        //	}
        //	catch (Exception ex)
        //	{
        //		throw ex;
        //	}
        //}

        public static bool CloseMySqlConnection(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public static MySqlCommand StartTransaction(MySqlCommand cmd)
        {
            try
            {
                MySqlTransaction trans = cmd.Connection.BeginTransaction();

                cmd.Transaction = trans;
                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public static MySqlCommand EndTransaction(MySqlCommand cmd)
        {
            try
            {
                cmd.Transaction.Commit();
                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public static MySqlCommand CancelTransaction(MySqlCommand cmd)
        {
            try
            {
                cmd.Transaction.Rollback();
                return cmd;
            }
            catch (Exception ex)
            {
                throw ;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        internal static MySqlCommand SetStoredProcedure(MySqlCommand cmd, string GetNextId)
        {

            try
            {
                cmd.CommandText = GetNextId;
                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        internal static MySqlCommand AddParameters(MySqlCommand cmd, params object[] parameters)
        {
            try
            {
                for (int liX = 0, liY = 1; liY < parameters.Length; liX += 2, liY += 2)
                    cmd.Parameters.AddWithValue(parameters[liX].ToString(), parameters[liY]);

                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        internal static int ExecuteScalar(MySqlCommand cmd)
        {
            try
            {
                Object obj = cmd.ExecuteScalar();

                return obj != null ? Convert.ToInt32(obj) : -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }

    }
}
