using MySql.Data.MySqlClient;
using NLog;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Core.ViewModel;
using MicroERP.DAL;
using MicroERP.DAL.CTL;

namespace MicroERP.Service
{
    public class ItemVariantsService
    {
        #region Class Members/Class Variables

        private ItemVariantsDAL _iVarDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        #endregion
        #region Constructors
        public ItemVariantsService ( )
        {
            _iVarDAL = new ItemVariantsDAL ();
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }

        #endregion
        #region ItemVariants
        public bool ManagementItemVariants ( ItemVariantsDE mod )
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetNextIdByClient (TableNames.itemvariants.ToString (), mod.ClientId,"ClientId");
                retVal = _iVarDAL.ManageItemVariants (mod,cmd);
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
        public List<ItemVariantsDE> SearchItemVariants ( ItemVariantsDE mod )
        {
            List<ItemVariantsDE> ItemVariants = new List<ItemVariantsDE> ();
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
                if (mod.ItemId != default)
                    whereClause += $" AND ItemId={mod.ItemId}";
                if (mod.AttributeValuesIds != default)
                    whereClause += $" AND AttributeValuesIds like ''" + mod.AttributeValuesIds + "'' ";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";

                ItemVariants = _iVarDAL.SearchItemVariants (whereClause);
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
            return ItemVariants;
        }

        #endregion
    }
}
