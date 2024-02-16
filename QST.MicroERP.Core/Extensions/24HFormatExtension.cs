using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Extensions
{
    public static class _24HFormatExtension
    {
        public static string ConvertTo24HFormat ( this DateTime date )
        {
            string retVal = "";
            try
            {
                TimeSpan time = date.TimeOfDay;
                retVal = time.ToString (@"hh\:mm");

            }
            catch (Exception) { throw; }
            return retVal;
        }
        public static string ConvertToDateWith24HFormat ( this DateTime date )
        {
            string retVal = "";
            try
            {
                TimeSpan time = date.TimeOfDay;
                retVal = time.ToString (@"hh\:mm\:ss") + "(" + date.Date.ToString ("yyyy-MM-dd") + ")";
            }
            catch (Exception) { throw; }
            return retVal;
        }
    }
}
