using Dapper;
using MicroERP.Core.Entities.TMS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL.TMS
{
    public class UserTaskDAL
    {
        #region DbOperations
        public bool ManageUserTask(UserTaskDE _tsk, MySqlCommand? cmd)
        {
            bool retVal = false;
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageUserTask";
                cmd.Parameters.AddWithValue("id", _tsk.Id);
                cmd.Parameters.AddWithValue("userId", _tsk.UserId);
                cmd.Parameters.AddWithValue("taskId", _tsk.TaskId);
                cmd.Parameters.AddWithValue("lastClaimId", _tsk.LastClaimId);
                cmd.Parameters.AddWithValue("statusId", _tsk.StatusId);
                cmd.Parameters.AddWithValue("parent", _tsk.Parent);
                cmd.Parameters.AddWithValue("date", _tsk.Date);
                cmd.Parameters.AddWithValue("claimId", _tsk.ClaimId);
                cmd.Parameters.AddWithValue("approvedClaimId", _tsk.ApprovedClaimId);
                cmd.Parameters.AddWithValue("sp", _tsk.Sp);
                cmd.Parameters.AddWithValue("workTime", _tsk.WorkTime);
                cmd.Parameters.AddWithValue("comments", _tsk.Comments);
                cmd.Parameters.AddWithValue("isDayEnded", _tsk.IsDayEnded);
                cmd.Parameters.AddWithValue("reviewedby", _tsk.ReviewedBy);
                cmd.Parameters.AddWithValue("reviewcomments", _tsk.ReviewComments);
                cmd.Parameters.AddWithValue("createdOn", _tsk.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", _tsk.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", _tsk.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", _tsk.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", _tsk.IsActive);
                cmd.Parameters.AddWithValue("DbOperation", _tsk.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                retVal = true;
                return retVal;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<UserTaskDE> SearchUserTask(string WhereClause, MySqlCommand cmd = null)
        {
            bool closeConnection = false;
            //WhereClause = string.Empty;
            List<UserTaskDE> lec = new List<UserTaskDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                lec = cmd.Connection.Query<UserTaskDE>("call MicroERP.SearchUserTask('" + WhereClause + "')").ToList();
                return lec;
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
        #region GetTodaysTasks
        public List<UserTaskDE> GetTodaysTasks(string UserId, MySqlCommand cmd = null)
        {
            bool closeConnection = false;
            //WhereClause = string.Empty;
            List<UserTaskDE> lec = new List<UserTaskDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                lec = cmd.Connection.Query<UserTaskDE>("call MicroERP.GetTodaysTasks('" + UserId + "')").ToList();
                return lec;
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
        #endregion
        #endregion
    }
}


