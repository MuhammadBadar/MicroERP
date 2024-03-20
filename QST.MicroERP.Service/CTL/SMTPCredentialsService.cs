using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.IDAL;
using QST.MicroERP.Service.IServices;
using MySql.Data.MySqlClient;
using NLog;
using Org.BouncyCastle.Math.EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.DAL.CTL;

namespace QST.MicroERP.Service
{
    public class SMTPCredentialsService :BaseService, IBaseService<SMTPCredentialsDE>
    {
        #region Class Variables
        private readonly IBaseDAL<SMTPCredentialsDE> _baseDAL;
        private CoreDAL _coreDAL;
        #endregion
        #region Constructor
        public SMTPCredentialsService ( IBaseDAL<SMTPCredentialsDE> baseDAL )
        {
            _baseDAL = baseDAL;
            _coreDAL = new CoreDAL ();
        }
        #endregion
        #region SMTPCredentials
        public bool ManageData ( SMTPCredentialsDE mod )
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (TableNames.CTL_SMTPCredentials.ToString (), mod.ClientId, "ClientId");
                retVal = _baseDAL.ManageData (mod, cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<SMTPCredentialsDE> SearchData ( SMTPCredentialsDE _smtp )
        {
            List<SMTPCredentialsDE> retVal = new List<SMTPCredentialsDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                string WhereClause = " Where 1=1";
                if (_smtp.Id != default)
                    WhereClause += $" AND Id={_smtp.Id}";
                if (_smtp.ClientId != default)
                    WhereClause += $" AND ClientId={_smtp.ClientId}";
                if (_smtp.IsActive != default && _smtp.IsActive == true)
                    WhereClause += $" AND IsActive=1";

                retVal = _baseDAL.SearchData (WhereClause, cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        #endregion
    }
}
