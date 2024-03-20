using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Extensions
{
    public static class StringExtension
    {
        public static string[] ToStringList ( this string value ) { return value.Split (","); }
    }
}
