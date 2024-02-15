using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Extensions
{
    public static class ConvertStringToListExtension
    {
        public static List<string> ConvertStringToList(this string inputStr )
        {
            return inputStr.Split (",").ToList ();
        }
    }
}
