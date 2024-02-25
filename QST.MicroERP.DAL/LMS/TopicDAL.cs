using Dapper;
using QST.MicroERP.Core.Entities.LMS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.DAL.LMS
{
    public class TopicDAL
    {
        #region DbOperations
        public bool ManageTopic(TopicDE _topic, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageTopic";
                cmd.Parameters.AddWithValue("id", _topic.Id);
                cmd.Parameters.AddWithValue("topicTitle", _topic.TopicTitle);
                cmd.Parameters.AddWithValue("description", _topic.Description);
                cmd.Parameters.AddWithValue("courseId", _topic.CourseId);
                cmd.Parameters.AddWithValue("createdOn", _topic.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", _topic.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", _topic.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", _topic.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", _topic.IsActive);
                cmd.Parameters.AddWithValue("DbOperation", _topic.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<TopicDE> SearchTopic(string WhereClause, MySqlCommand cmd)
        {
            bool closeConnection = false;
            List<TopicDE> topics = new List<TopicDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                topics = cmd.Connection.Query<TopicDE>("call SearchTopic('" + WhereClause + "')").ToList();
                return topics;
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
