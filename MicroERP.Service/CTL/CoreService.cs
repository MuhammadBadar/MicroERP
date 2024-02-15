using Google.Protobuf.WellKnownTypes;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MySql.Data.MySqlClient;
using NLog;

namespace MicroERP.Service.CLT
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
