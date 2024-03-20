using Dapper;
using QST.MicroERP.Core.Entities.TMS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.DAL.TMS
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
                cmd.CommandText = SPNames.TMS_Manage_UserTask.ToString ();
                cmd.Parameters.AddWithValue("prm_id", _tsk.Id);
                cmd.Parameters.AddWithValue("prm_clientId", _tsk.ClientId);
                cmd.Parameters.AddWithValue("prm_stalledReason", _tsk.StalledReason);
                cmd.Parameters.AddWithValue("prm_userId", _tsk.UserId);
                cmd.Parameters.AddWithValue("prm_taskId", _tsk.TaskId);
                cmd.Parameters.AddWithValue("prm_lastClaimId", _tsk.LastClaimId);
                cmd.Parameters.AddWithValue("prm_statusId", _tsk.StatusId);
                cmd.Parameters.AddWithValue("prm_parent", _tsk.Parent);
                cmd.Parameters.AddWithValue("prm_date", _tsk.Date);
                cmd.Parameters.AddWithValue("prm_claimId", _tsk.ClaimId);
                cmd.Parameters.AddWithValue("prm_approvedClaimId", _tsk.ApprovedClaimId);
                cmd.Parameters.AddWithValue("prm_sp", _tsk.Sp);
                cmd.Parameters.AddWithValue("prm_workTime", _tsk.WorkTime);
                cmd.Parameters.AddWithValue("prm_comments", _tsk.Comments);
                cmd.Parameters.AddWithValue("prm_isDayEnded", _tsk.IsDayEnded);
                cmd.Parameters.AddWithValue("prm_reviewedby", _tsk.ReviewedBy);
                cmd.Parameters.AddWithValue("prm_reviewcomments", _tsk.ReviewComments);
                cmd.Parameters.AddWithValue("prm_createdOn", _tsk.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", _tsk.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", _tsk.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", _tsk.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", _tsk.IsActive);
                cmd.Parameters.AddWithValue("prm_DbOperation", _tsk.DBoperation.ToString());
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
                lec = cmd.Connection.Query<UserTaskDE>("call "+SPNames.TMS_Search_UserTask.ToString () + "('" + WhereClause + "')").ToList();
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
    }
}


