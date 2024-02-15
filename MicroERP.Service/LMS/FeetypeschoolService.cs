using MicroERP.Core.Entities.LMS;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MicroERP.DAL.LMS;
using MySql.Data.MySqlClient;
using NLog;

namespace MicroERP.Service.LMS
{
    public class FeetypeschoolService
    {
        #region Class Members/Class Variables

        private FeetypeschoolDAL _feetypeschoolDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public FeetypeschoolService()
        {
            _feetypeschoolDAL = new FeetypeschoolDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region FeeType
        public bool ManageFeetypeschool(FeetypeschoolDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.feetypeschool.ToString());
                retVal = _feetypeschoolDAL.ManageFeetypeschool(mod);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<FeetypeschoolDE> SearchFeetypeschool(FeetypeschoolDE mod)
        {
            List<FeetypeschoolDE> Feetypeschool = new List<FeetypeschoolDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.Title != default)
                    whereClause += $" and Title like ''" + mod.Title + "''";
                if (mod.Type != default)
                    whereClause += $" and Type like ''" + mod.Type + "''";
                if (mod.IsRefundable != default)
                    whereClause += $" and IsRefundable like ''" + mod.IsRefundable + "''";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                Feetypeschool = _feetypeschoolDAL.SearchFeetypeschool(whereClause);

                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error(exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return Feetypeschool;
        }
        #endregion
    }
}
