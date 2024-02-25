using MySql.Data.MySqlClient;
using NLog;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.DAL.CTL;

namespace QST.MicroERP.Service
{
    public class DocExtrasService
    {
        #region Class Members/Class Variables

        private DocExtrasDAL _docExtraDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        #endregion
        #region Constructors
        public DocExtrasService ( )
        {
            _docExtraDAL = new DocExtrasDAL ();
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }

        #endregion
        #region DocExtras
        public bool ManagementDocExtras ( DocExtrasDE mod )
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId (TableNames.SAL_DocumentExtras.ToString ());
                retVal = _docExtraDAL.ManageDocExtras (mod, cmd);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<DocExtrasDE> SearchDocExtras ( DocExtrasDE mod )
        {
            List<DocExtrasDE> DocExtras = new List<DocExtrasDE> ();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.DocExtraId != default && mod.DocExtraId!=0)
                    whereClause += $" AND DocExtraId={mod.DocExtraId}";
                if (mod.FormulaId != default && mod.FormulaId!=0)
                    whereClause += $" AND FormulaId={mod.FormulaId}";
                if (mod.DocExtraTypeId != default && mod.DocExtraTypeId != 0)
                    whereClause += $" AND DocExtraTypeId={mod.DocExtraTypeId}";
                if (mod.IncDecTypeId != default && mod.IncDecTypeId!=0)
                    whereClause += $" AND IncDecTypeId={mod.IncDecTypeId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";

                DocExtras = _docExtraDAL.SearchDocExtras (whereClause);
                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error (exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return DocExtras;
        }

        #endregion
    }
}
