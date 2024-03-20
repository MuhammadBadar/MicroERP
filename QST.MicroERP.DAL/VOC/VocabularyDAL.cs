using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using QST.MicroERP.Core.Entities.VOC;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;

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
                cmd.CommandText = SPNames.VOC_Manage_Vocabulary.ToString ();
                cmd.Parameters.AddWithValue("prm_id", _vcb.Id);
                cmd.Parameters.AddWithValue("prm_clientId", _vcb.ClientId);
                cmd.Parameters.AddWithValue("prm_word", _vcb.Word);
                cmd.Parameters.AddWithValue("prm_englishMeaning", _vcb.EnglishMeaning);
                cmd.Parameters.AddWithValue("prm_urduMeaning", _vcb.UrduMeaning);
                cmd.Parameters.AddWithValue("prm_createdOn", _vcb.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", _vcb.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", _vcb.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", _vcb.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", _vcb.IsActive);
                cmd.Parameters.AddWithValue("prm_DbOperation", _vcb.DBoperation.ToString());
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
                lec = cmd.Connection.Query<VocabularyDE>("call "+SPNames.VOC_Search_Vocabulary.ToString() + "('" + WhereClause + "')").ToList ();
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
                cmd.CommandText = "VOC_Manage_UserVocabulary";
                cmd.Parameters.AddWithValue("prm_id", _uvcb.Id);
                cmd.Parameters.AddWithValue("prm_wordId", _uvcb.WordId);
                cmd.Parameters.AddWithValue ("prm_clientId", _uvcb.ClientId);
                cmd.Parameters.AddWithValue("prm_userId", _uvcb.UserId);
                cmd.Parameters.AddWithValue("prm_pronunciation", _uvcb.Pronunciation);
                cmd.Parameters.AddWithValue("prm_sentence", _uvcb.Sentence);
                cmd.Parameters.AddWithValue("prm_vocabDifficultyLevelId", _uvcb.VocabDifficultyLevelId);
                cmd.Parameters.AddWithValue("prm_novelId", _uvcb.NovelId);
                cmd.Parameters.AddWithValue("prm_comments", _uvcb.Comments);
                cmd.Parameters.AddWithValue("prm_isNeedHelp", _uvcb.IsNeedHelp);
                cmd.Parameters.AddWithValue("prm_chapterId", _uvcb.ChapterId);
                cmd.Parameters.AddWithValue("prm_createdOn", _uvcb.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", _uvcb.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", _uvcb.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", _uvcb.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", _uvcb.IsActive);
                cmd.Parameters.AddWithValue("prm_DbOperation", _uvcb.DBoperation.ToString());
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
                userVocabularies = cmd.Connection.Query<UserVocabularyDE>("call VOC_Search_UserVocabulary('" + WhereClause + "')").ToList();
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
