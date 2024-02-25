using NLog;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Service.CTL
{
    public partial class CatalogueService
    {
        #region Class Variables
        public ClientsDAL _clientsDAL;
        public CoreDAL _coreDAL;
        public Logger _logger;

        public CityDAL _ctyDAL;
        //private CoreDAL _corDAL;
        
        #endregion
        public CatalogueService()
        {
            _clientsDAL = new ClientsDAL();
            _coreDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");

            _ctyDAL = new CityDAL();
        }
    }
}
