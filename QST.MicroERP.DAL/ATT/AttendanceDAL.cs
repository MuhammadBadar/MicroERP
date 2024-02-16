using Dapper;
using QST.MicroERP.Core.Entities.ATT;
using QST.MicroERP.Core.ViewModel;
using MySql.Data.MySqlClient;
using System.Data;

namespace QST.MicroERP.DAL.ATT
{
    public class AttendanceDAL
    {

        public bool ManageAttendance(AttendanceDE Attendance, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageAttendance";
                cmd.Parameters.AddWithValue("prm_Id", Attendance.Id);
                cmd.Parameters.AddWithValue("prm_SchDayId", Attendance.SchDayId);
                cmd.Parameters.AddWithValue("prm_UserId", Attendance.UserId);
                cmd.Parameters.AddWithValue("prm_DayStartTime", Attendance.DayStartTime);
                cmd.Parameters.AddWithValue("prm_DayEndTime", Attendance.DayEndTime);
                cmd.Parameters.AddWithValue("prm_Date", Attendance.Date);
                cmd.Parameters.AddWithValue("prm_CreatedOn", Attendance.CreatedOn);
                cmd.Parameters.AddWithValue("prm_CreatedBy", Attendance.CreatedById);
                cmd.Parameters.AddWithValue("prm_ModifiedOn", Attendance.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_ModifiedBy", Attendance.ModifiedById);
                cmd.Parameters.AddWithValue("prm_IsActive", Attendance.IsActive);
                cmd.Parameters.AddWithValue("prm_DBoperation", Attendance.DBoperation.ToString());
                cmd.Parameters.AddWithValue("prm_Filter", Attendance.DBoperation.ToString());
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
        public List<AttendanceDE> SearchAttendance(string whereClause, MySqlCommand cmd = null)
        {

            List<AttendanceDE> top = new List<AttendanceDE>();
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
                top = cmd.Connection.Query<AttendanceDE>("call QST.MicroERP.SearchAttendance( '" + whereClause + "')").ToList();
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
