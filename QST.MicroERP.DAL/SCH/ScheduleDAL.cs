using Dapper;
using QST.MicroERP.Core.Entities.SCH;
using Microsoft.Extensions.Logging;
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
    public class ScheduleDAL
    {
        #region Schedule Operations
        public bool ManageSchedule(ScheduleDE sch, MySqlCommand? cmd)
        {
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
                cmd.CommandText =SPNames.SCH_Manage_Schedule.ToString ();
                cmd.Parameters.AddWithValue("@prm_Id", sch.Id);
                cmd.Parameters.AddWithValue("@prm_UserId", sch.UserId);
                cmd.Parameters.AddWithValue("@prm_ClientId", sch.ClientId);
                cmd.Parameters.AddWithValue("@prm_RoleId", sch.RoleId);
                cmd.Parameters.AddWithValue("@prm_EntityId", sch.EntityId);
                cmd.Parameters.AddWithValue("@prm_ScheduleTypeId", sch.ScheduleTypeId);
                cmd.Parameters.AddWithValue("@prm_WorkingTypeId", sch.WorkingTypeId);
                cmd.Parameters.AddWithValue("@prm_WorkingHours", sch.WorkingHours);
                cmd.Parameters.AddWithValue("@prm_StartDate", sch.StartDate);
                cmd.Parameters.AddWithValue("@prm_EndDate", sch.EndDate);
                cmd.Parameters.AddWithValue("@prm_EffectiveDate", sch.EffectiveDate);
                cmd.Parameters.AddWithValue("@prm_CreatedOn", sch.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_CreatedBy", sch.CreatedById);
                cmd.Parameters.AddWithValue("@prm_ModifiedOn", sch.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_ModifiedBy", sch.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_IsActive", sch.IsActive);
                cmd.Parameters.AddWithValue("@prm_DBoperation", sch.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<ScheduleDE> SearchSchedule(string whereClause, MySqlCommand cmd = null)
        {
            List<ScheduleDE> top = new List<ScheduleDE>();
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
                top = cmd.Connection.Query<ScheduleDE>("call "+SPNames.SCH_Search_Schedule.ToString () + "( '" + whereClause + "')").ToList();
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
        #endregion
        #region ScheduleDayEvents Operations
        public bool ManageScheduleDayEvent(ScheduleDayEventDE Events, MySqlCommand cmd = null)
        {
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
                cmd.CommandText = SPNames.SCH_Manage_ScheduleDayEvent.ToString ();
                cmd.Parameters.AddWithValue("@prm_Id", Events.Id);
                cmd.Parameters.AddWithValue("@prm_ClientId", Events.ClientId);
                cmd.Parameters.AddWithValue("@prm_StartTime", Events.StartTime);
                cmd.Parameters.AddWithValue("@prm_EndTime", Events.EndTime);
                cmd.Parameters.AddWithValue("@prm_EventTypeId", Events.EventTypeId);
                cmd.Parameters.AddWithValue("@prm_schId", Events.SchId);
                cmd.Parameters.AddWithValue("@prm_ScheduleDayId", Events.SchDayId);
                cmd.Parameters.AddWithValue("@prm_LocationId", Events.LocationId);
                cmd.Parameters.AddWithValue("@prm_createdOn", Events.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdById", Events.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", Events.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedById", Events.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", Events.IsActive);
                cmd.Parameters.AddWithValue("@prm_DBoperation", Events.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<ScheduleDayEventDE> SearchScheduleDayEvent(string whereClause, MySqlCommand cmd = null)
        {
            List<ScheduleDayEventDE> top = new List<ScheduleDayEventDE>();
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
                top = cmd.Connection.Query<ScheduleDayEventDE>("call "+SPNames.SCH_Search_ScheduleDayEvent.ToString () + "( '" + whereClause + "')").ToList();
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
                cmd.Parameters.Clear();
            }
        }
        #endregion
        #region ScheduleDay Operations
        public bool ManageScheduleDay(ScheduleDayDE day, MySqlCommand cmd = null)
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection has been created");
                else
                    Console.WriteLine("Connection error");

                cmd.CommandText = SPNames.SCH_Manage_ScheduleDay.ToString ();

                // Clear existing parameters before adding new ones
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@prm_Id", day.Id);
                cmd.Parameters.AddWithValue("@prm_ClientId", day.ClientId);
                cmd.Parameters.AddWithValue("@prm_DayId", day.DayId);
                cmd.Parameters.AddWithValue("@prm_SchId", day.SchId);
                cmd.Parameters.AddWithValue("@prm_WorkTime", day.WorkTime);
                cmd.Parameters.AddWithValue("@prm_createdOn", day.CreatedOn);
                cmd.Parameters.AddWithValue("@prm_createdBy", day.CreatedById);
                cmd.Parameters.AddWithValue("@prm_modifiedOn", day.ModifiedOn);
                cmd.Parameters.AddWithValue("@prm_modifiedBy", day.ModifiedById);
                cmd.Parameters.AddWithValue("@prm_isActive", day.IsActive);
                cmd.Parameters.AddWithValue("@prm_DBoperation", day.DBoperation.ToString());
                cmd.Parameters.AddWithValue("@prm_Filter", day.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<ScheduleDayDE> SearchScheduleDay(string whereClause, MySqlCommand cmd = null)
        {
            List<ScheduleDayDE> top = new List<ScheduleDayDE>();
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
                top = cmd.Connection.Query<ScheduleDayDE>("call "+SPNames.SCH_Search_ScheduleDay.ToString () + "( '" + whereClause + "')").ToList();
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
                cmd.Parameters.Clear();
            }
        }
        #endregion
    }
}
