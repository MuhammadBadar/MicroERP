using Dapper;
using QST.MicroERP.Core.Entities.SCH;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL.SCH
{
    public class ScheduleDayEventDAL
    {
        #region ScheduleDayEvents Operations
        public bool ManageScheduleDayEvent ( ScheduleDayEventDE Events, MySqlCommand cmd = null )
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
                cmd.CommandText = SPNames.SCH_Manage_ScheduleDayEvent.ToString ();
                cmd.Parameters.AddWithValue ("@prm_Id", Events.Id);
                cmd.Parameters.AddWithValue ("@prm_ClientId", Events.ClientId);
                cmd.Parameters.AddWithValue ("@prm_StartTime", Events.StartTime);
                cmd.Parameters.AddWithValue ("@prm_EndTime", Events.EndTime);
                cmd.Parameters.AddWithValue ("@prm_EventTypeId", Events.EventTypeId);
                cmd.Parameters.AddWithValue ("@prm_schId", Events.SchId);
                cmd.Parameters.AddWithValue ("@prm_Sp", Events.Sp);
                cmd.Parameters.AddWithValue ("@prm_ScheduleDayId", Events.SchDayId);
                cmd.Parameters.AddWithValue ("@prm_LocationId", Events.LocationId);
                cmd.Parameters.AddWithValue ("@prm_createdOn", Events.CreatedOn);
                cmd.Parameters.AddWithValue ("@prm_createdById", Events.CreatedById);
                cmd.Parameters.AddWithValue ("@prm_modifiedOn", Events.ModifiedOn);
                cmd.Parameters.AddWithValue ("@prm_modifiedById", Events.ModifiedById);
                cmd.Parameters.AddWithValue ("@prm_isActive", Events.IsActive);
                cmd.Parameters.AddWithValue ("@prm_DBoperation", Events.DBoperation.ToString ());

                cmd.ExecuteNonQuery ();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Parameters.Clear ();
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<ScheduleDayEventDE> SearchScheduleDayEvent ( string whereClause, MySqlCommand cmd = null )
        {
            List<ScheduleDayEventDE> top = new List<ScheduleDayEventDE> ();
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
                top = cmd.Connection.Query<ScheduleDayEventDE> ("call "+SPNames.SCH_Search_ScheduleDayEvent.ToString()+"( '" + whereClause + "')").ToList ();
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
                cmd.Parameters.Clear ();
            }
        }

        #endregion
    }
}
