using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroERP.Core.Constants;
using MicroERP.Core.Entities.VOC;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MicroERP.DAL.VOC;
using MySql.Data.MySqlClient;
using NLog;
using static Dapper.SqlMapper;

namespace MicroERP.Service.VOC
{
    public class VocabularyService : BaseService
    {
        #region Class Variables
        private VocabularyDAL _vcbDAL;
        private CoreDAL _coreDAL;
        #endregion
        #region Constructor
        public VocabularyService()
        {
            _vcbDAL = new VocabularyDAL();
            _coreDAL = new CoreDAL();
        }
        #endregion
        #region  Vocabulary
        public VocabularyDE ManageVocabularyAsync(VocabularyDE _vcb)
        {
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                closeConnectionFlag = true;
                _entity = TableNames.Vocabulary.ToString();
                var words = new List<VocabularyDE>();
                if (_vcb.DBoperation == DBoperations.Insert || _vcb.DBoperation == DBoperations.Update)
                {
                    words = SearchVocabulary(new VocabularyDE { Word = _vcb.Word });
                    if (_vcb.DBoperation == DBoperations.Update)
                        words = words.Where(x => x.Id != _vcb.Id).ToList();
                }
                if (words != null && words.Count > 0)
                {
                    _vcb.AddErrorMessage(string.Format(AppConstants.EXISTS, _vcb.Word));
                    _logger.Info($"Error: " + string.Format(AppConstants.EXISTS, _vcb.Word));
                }
                else
                {
                    if (_vcb.DBoperation == DBoperations.Insert)
                        _vcb.Id = _coreDAL.GetnextId(_entity);
                    if (_vcbDAL.ManageVocabulary(_vcb, cmd) == true)
                    {
                        _vcb.AddSuccessMessage(string.Format(AppConstants.CRUD_DB_OPERATION, _entity, _vcb.DBoperation.ToString()));
                        _logger.Info($"Success: " + string.Format(AppConstants.CRUD_DB_OPERATION, _entity, _vcb.DBoperation.ToString()));
                        if (_vcb.UserVocab != null)
                        {
                            if (_vcb.DBoperation == DBoperations.Insert || _vcb.DBoperation == DBoperations.Update)
                            {
                                var existingUserVocab = SearchUserVocabulary(new UserVocabularyDE { UserId = _vcb.UserVocab.UserId, WordId = _vcb.Id }).FirstOrDefault();
                                if (existingUserVocab != null && existingUserVocab.Id > 0)
                                    _vcb.UserVocab.DBoperation = DBoperations.Update;
                                else
                                    _vcb.UserVocab.DBoperation = DBoperations.Insert;


                                _entity = TableNames.UserVocabulary.ToString();
                                if (_vcb.UserVocab.DBoperation == DBoperations.Insert)
                                    _vcb.UserVocab.Id = _coreDAL.GetnextId(_entity);


                                _vcb.UserVocab.WordId = _vcb.Id;
                                if (_vcbDAL.ManageUserVocabulary(_vcb.UserVocab, cmd) == true)
                                {
                                    _vcb.AddSuccessMessage(string.Format(AppConstants.CRUD_DB_OPERATION, _entity, _vcb.DBoperation.ToString()));
                                    _logger.Info($"Success: " + string.Format(AppConstants.CRUD_DB_OPERATION, _entity, _vcb.DBoperation.ToString()));
                                }
                                else
                                {
                                    _vcb.AddErrorMessage(string.Format(AppConstants.CRUD_ERROR, _entity));
                                    _logger.Info($"Error: " + string.Format(AppConstants.CRUD_ERROR, _entity));
                                }
                            }
                        }
                    }
                    else
                    {
                        _vcb.AddErrorMessage(string.Format(AppConstants.CRUD_ERROR, _entity));
                        _logger.Info($"Error: " + string.Format(AppConstants.CRUD_ERROR, _entity));
                    }
                }

                MicroERPDataContext.EndTransaction(cmd);
            }
            catch (Exception ex)
            {
                _vcb.AddErrorMessage(string.Format(AppConstants.CRUD_ERROR, _entity));
                _logger.Info($"Error: " + string.Format(AppConstants.CRUD_ERROR, _entity));
                _logger.Error(ex);
                MicroERPDataContext.CancelTransaction(cmd);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return _vcb;
        }
        public List<VocabularyDE> SearchVocabulary(VocabularyDE _vcb)
        {
            List<VocabularyDE> retVal = new List<VocabularyDE>();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                string whereClause = " Where 1=1";
                if (_vcb.Id != default)
                    whereClause += $" AND Id={_vcb.Id}";
                if (_vcb.Word != default)
                    whereClause += $" and Word like ''" + _vcb.Word + "'' ";
                if (_vcb.EnglishMeaning != default)
                    whereClause += $" and EnglishMeaning like ''" + _vcb.EnglishMeaning + "''";
                if (_vcb.UrduMeaning != default)
                    whereClause += $" and UrduMeaning like ''" + _vcb.UrduMeaning + "''";
                if (_vcb.IsActive != default && _vcb.IsActive == true)
                    whereClause += $" AND IsActive=1";

                retVal = _vcbDAL.SearchVocabulary(whereClause, cmd);
                foreach (var vocab in retVal)
                {
                    vocab.UserVocab = SearchUserVocabulary(new UserVocabularyDE { WordId = vocab.Id, UserId = _vcb.UserVocab.UserId }).FirstOrDefault();
                }
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<UserVocabularyDE> SearchUserVocabulary(UserVocabularyDE _vcb)
        {
            List<UserVocabularyDE> retVal = new List<UserVocabularyDE>();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                string whereClause = " Where 1=1";
                if (_vcb.WordId != default)
                    whereClause += $" and WordId like ''" + _vcb.WordId + "'' ";
                if (_vcb.UserId != default)
                    whereClause += $" and UserId like ''" + _vcb.UserId + "'' ";

                retVal = _vcbDAL.SearchUserVocabulary(whereClause, cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }

        #endregion
    }
}
