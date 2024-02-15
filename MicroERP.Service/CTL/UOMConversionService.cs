using MySql.Data.MySqlClient;
using NLog;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Core.ViewModel;
using MicroERP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroERP.DAL.CTL;

namespace MicroERP.Service
{
    public class UOMConversionService
    {
        #region Class Members/Class Variables

        private UOMConversionDAL _uomConDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        #endregion
        #region Constructors
        public UOMConversionService ( )
        {
            _uomConDAL = new UOMConversionDAL ();
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }

        #endregion
        #region UOMConversion
        public bool ManagementUOMConversion ( UOMConversionDE mod )
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId (TableNames.uomconversion.ToString ());
                retVal = _uomConDAL.ManageUOMConversion (mod);
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
        public List<UOMConversionDE> SearchUOMConversion ( UOMConversionDE mod )
        {
            List<UOMConversionDE> UOMConversion = new List<UOMConversionDE> ();
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
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";

                UOMConversion = _uomConDAL.SearchUOMConversion (whereClause);
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
            return UOMConversion;
        }

        #endregion
    }
}
