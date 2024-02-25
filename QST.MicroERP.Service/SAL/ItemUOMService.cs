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
    public class ItemUOMService
    {
        #region Class Members/Class Variables

        private ItemUOMDAL _itmUOM;
        private CoreDAL _corDAL;
        private Logger _logger;
        #endregion
        #region Constructors
        public ItemUOMService ( )
        {
            _itmUOM = new ItemUOMDAL ();
            _corDAL = new CoreDAL ();
            _logger = LogManager.GetLogger ("fileLogger");
        }

        #endregion
        #region ItemUOM
        public bool ManagementItemUOM ( ItemUOMDE mod )
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetNextIdByClient (TableNames.SAL_ItemUOM.ToString (), mod.ClientId, "ClientId");
                retVal = _itmUOM.ManageItemUOM (mod, cmd);
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
        public List<ItemUOMDE> SearchItemUOM ( ItemUOMDE mod )
        {
            List<ItemUOMDE> ItemUOM = new List<ItemUOMDE> ();
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
                if (mod.UOMTypeId != default)
                    whereClause += $" AND UOMTypeId={mod.UOMTypeId}";
                if (mod.UOMId != default)
                    whereClause += $" AND UOMId={mod.UOMId}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";

                ItemUOM = _itmUOM.SearchItemUOM (whereClause);
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
            return ItemUOM;
        }

        #endregion
    }
}
