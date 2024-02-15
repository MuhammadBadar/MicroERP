using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Service
{
    public abstract class BaseService
    {
        public Logger _logger;
        public String? _entity { get; set; }
        public BaseService ( )
        {
            _logger = LogManager.GetLogger ("fileLogger");
        }
    }

}
