using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace QST.ERP.Domain.Data
{
    //Database helper class 
    public static class DBHelper
    {
        //Returns the database connection string
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["csPQS"].ConnectionString;
            }
        }
    }
}
