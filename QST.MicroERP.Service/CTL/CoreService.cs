using Google.Protobuf.WellKnownTypes;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using MySql.Data.MySqlClient;
using NLog;

namespace QST.MicroERP.Service.CLT
{
    public class CoreService
    {
        #region Class Variables
        private CoreDAL _corDAL;
        private Logger _logger;
        #endregion
        #region Constructors
        public CoreService()
        {
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region SuccessMsg & ErrorMsg

        #endregion
    }
}
