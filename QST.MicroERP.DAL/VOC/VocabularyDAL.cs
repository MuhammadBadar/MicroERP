using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using QST.MicroERP.Core.Entities.VOC;
using MySql.Data.MySqlClient;

namespace QST.MicroERP.DAL.VOC
{
    public class VocabularyDAL
    {
        #region DbOperations

        public bool ManageVocabulary(VocabularyDE _vcb, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageVocabulary";
                cmd.Parameters.AddWithValue("id", _vcb.Id);
                cmd.Parameters.AddWithValue("word", _vcb.Word);
                cmd.Parameters.AddWithValue("englishMeaning", _vcb.EnglishMeaning);
                cmd.Parameters.AddWithValue("urduMeaning", _vcb.UrduMeaning);
                cmd.Parameters.AddWithValue("createdOn", _vcb.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", _vcb.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", _vcb.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", _vcb.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", _vcb.IsActive);
                cmd.Parameters.AddWithValue("DbOperation", _vcb.DBoperation.ToString());
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
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }

        public List<VocabularyDE> SearchVocabulary(string WhereClause, MySqlCommand cmd)
        {
            bool closeConnection = false;
            List<VocabularyDE> lec = new List<VocabularyDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                lec = cmd.Connection.Query<VocabularyDE>("call QST.MicroERP.SearchVocabulary('" + WhereClause + "')").ToList();
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

        public bool ManageUserVocabulary(UserVocabularyDE _uvcb, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = "ManageUserVocabulary";
                cmd.Parameters.AddWithValue("id", _uvcb.Id);
                cmd.Parameters.AddWithValue("wordId", _uvcb.WordId);
                cmd.Parameters.AddWithValue("userId", _uvcb.UserId);
                cmd.Parameters.AddWithValue("pronunciation", _uvcb.Pronunciation);
                cmd.Parameters.AddWithValue("sentence", _uvcb.Sentence);
                cmd.Parameters.AddWithValue("vocabDifficultyLevelId", _uvcb.VocabDifficultyLevelId);
                cmd.Parameters.AddWithValue("novelId", _uvcb.NovelId);
                cmd.Parameters.AddWithValue("comments", _uvcb.Comments);
                cmd.Parameters.AddWithValue("isNeedHelp", _uvcb.IsNeedHelp);
                cmd.Parameters.AddWithValue("chapterId", _uvcb.ChapterId);
                cmd.Parameters.AddWithValue("createdOn", _uvcb.CreatedOn);
                cmd.Parameters.AddWithValue("createdById", _uvcb.CreatedById);
                cmd.Parameters.AddWithValue("modifiedOn", _uvcb.ModifiedOn);
                cmd.Parameters.AddWithValue("modifiedById", _uvcb.ModifiedById);
                cmd.Parameters.AddWithValue("isActive", _uvcb.IsActive);
                cmd.Parameters.AddWithValue("DbOperation", _uvcb.DBoperation.ToString());
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
                if (closeConnection)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }

        public List<UserVocabularyDE> SearchUserVocabulary(string WhereClause, MySqlCommand cmd)
        {
            bool closeConnection = false;
            List<UserVocabularyDE> userVocabularies = new List<UserVocabularyDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                userVocabularies = cmd.Connection.Query<UserVocabularyDE>("call QST.MicroERP.SearchUserVocabulary('" + WhereClause + "')").ToList();
                return userVocabularies;
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
