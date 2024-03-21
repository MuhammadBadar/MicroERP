using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;
using QST.MicroERP.Core.Entities.VOC;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL.VOC;
using MySql.Data.MySqlClient;
using NLog;
using static Dapper.SqlMapper;
using System.Data;
using QST.MicroERP.Core.Entities.SEC;
using QST.MicroERP.Service.SEC;

namespace QST.MicroERP.Service.VOC
{
    public class VocabularyService : BaseService
    {
        #region Class Variables
        private VocabularyDAL _vcbDAL;
        private UserService _userSvc;
        #endregion
        #region Constructor
        public VocabularyService()
        {
            _vcbDAL = new VocabularyDAL();
            _userSvc = new UserService ();
        }
        #endregion
        #region  Vocabulary
        public VocabularyDE ManageVocabularyAsync(VocabularyDE _vcb)
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }
                MicroERPDataContext.StartTransaction(cmd);
                _entity = TableNames.VOC_Vocabulary.ToString();

                var words = new List<VocabularyDE>();
                if (_vcb.DBoperation == DBoperations.Insert || _vcb.DBoperation == DBoperations.Update)
                {
                    words = SearchVocabulary(new VocabularyDE { Word = _vcb.Word, ClientId=_vcb.ClientId });
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
                        _vcb.Id = _coreDAL.GetNextIdByClient (_entity.ToString (), _vcb.ClientId, "ClientId");

                    _logger.Info ($"Going to Call:_vcbDAL.ManageVocabulary(_vcb, cmd)");
                    if (_vcbDAL.ManageVocabulary(_vcb, cmd) == true)
                    {
                        _vcb.AddSuccessMessage(string.Format(AppConstants.CRUD_DB_OPERATION, _entity, _vcb.DBoperation.ToString()));
                        _logger.Info($"Success: " + string.Format(AppConstants.CRUD_DB_OPERATION, _entity, _vcb.DBoperation.ToString()));
                        if (_vcb.UserVocab != null)
                        {
                            if (_vcb.DBoperation == DBoperations.Insert || _vcb.DBoperation == DBoperations.Update)
                            {
                                var existingUserVocab = SearchUserVocabulary(new UserVocabularyDE { UserId = _vcb.UserVocab.UserId, WordId = _vcb.Id,ClientId=_vcb.ClientId }).FirstOrDefault();
                                if (existingUserVocab != null && existingUserVocab.Id > 0)
                                    _vcb.UserVocab.DBoperation = DBoperations.Update;
                                else
                                    _vcb.UserVocab.DBoperation = DBoperations.Insert;


                                _entity = TableNames.VOC_UserVocabulary.ToString();
                                if (_vcb.UserVocab.DBoperation == DBoperations.Insert)
                                    _vcb.UserVocab.Id = _coreDAL.GetNextIdByClient (_entity.ToString (), _vcb.ClientId, "ClientId");

                                _vcb.UserVocab.WordId = _vcb.Id;
                                _vcb.UserVocab.ClientId = _vcb.ClientId;

                                _logger.Info ($"Going to Call:_vcbDAL.ManageUserVocabulary(_vcb.UserVocab, cmd)");
                                if (_vcbDAL.ManageUserVocabulary(_vcb.UserVocab, cmd) == true)
                                {
                                    _vcb.UserVocab.AddSuccessMessage(string.Format(AppConstants.CRUD_DB_OPERATION, _entity, _vcb.UserVocab.DBoperation.ToString()));
                                    _logger.Info($"Success: " + string.Format(AppConstants.CRUD_DB_OPERATION, _entity, _vcb.UserVocab.DBoperation.ToString()));
                                }
                                else
                                {
                                    _vcb.UserVocab.AddErrorMessage(string.Format(AppConstants.CRUD_ERROR, _entity));
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
            bool closeConnectionFlag = false;
            List<VocabularyDE> retVal = new List<VocabularyDE>();
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                string whereClause = " Where 1=1";
                if (_vcb.Id != default)
                    whereClause += $" AND Id={_vcb.Id}";
                if (_vcb.ClientId != default && _vcb.ClientId != 0)
                    whereClause += $" AND ClientId={_vcb.ClientId}";
                if (_vcb.Word != default)
                    whereClause += $" and Word like ''" + _vcb.Word + "'' ";
                if (_vcb.NovelId != default)
                    whereClause += $" and NovelId like ''" + _vcb.NovelId + "'' ";
                if (_vcb.Id != default)
                    whereClause += $" AND Id={_vcb.Id}";
                if (_vcb.EnglishMeaning != default)
                    whereClause += $" and EnglishMeaning like ''" + _vcb.EnglishMeaning + "''";
                if (_vcb.UrduMeaning != default)
                    whereClause += $" and UrduMeaning like ''" + _vcb.UrduMeaning + "''";
                if (_vcb.IsActive != default && _vcb.IsActive == true)
                    whereClause += $" AND IsActive=1";
                if (_vcb.IncludeSubordinatesData && _vcb.UserId != default)
                {
                    var user = new UserDE ();
                    user.Id = _vcb.UserId;
                    user.ClientId = _vcb.ClientId;
                    var subordinateUsers = _userSvc.GetSubordinates (user);

                    if (subordinateUsers.Count > 0)
                    {
                        string subordinateIds = string.Join ("'',''", subordinateUsers.Select (x => x.Id));
                        whereClause += $" and (UserId like ''" + _vcb.UserId + "'' or UserId IN (''" + subordinateIds + "''))";
                    }
                }
                else
                {
                    if (_vcb.UserId != default)
                        whereClause += $" AND UserId like ''{_vcb.UserId}''";
                }
                retVal = _vcbDAL.SearchVocabulary(whereClause, cmd);
                foreach (var vocab in retVal)
                {
                    vocab.UserVocab = SearchUserVocabulary(new UserVocabularyDE { WordId = vocab.Id, UserId = _vcb.UserVocab.UserId,ClientId=vocab.ClientId }).FirstOrDefault();
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
            bool closeConnectionFlag = false;
            List<UserVocabularyDE> retVal = new List<UserVocabularyDE>();
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection ();
                    closeConnectionFlag = true;
                }

                string whereClause = " Where 1=1";
                if (_vcb.WordId != default)
                    whereClause += $" and WordId like ''" + _vcb.WordId + "'' ";
                if (_vcb.ClientId != default && _vcb.ClientId != 0)
                    whereClause += $" AND ClientId={_vcb.ClientId}";
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
