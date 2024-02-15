using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeyAccounting.Core.Extenstions
{
    public static class MicroERP
    {
        public static string GetAgeFromDOB ( this DateTime dob )
        {
            string age="0";
            try
            {
                DateTime today = DateTime.Today;
                int years = today.Year - dob.Year;
                int months = today.Month - dob.Month;
                int days = today.Day - dob.Day;
                if (days < 0)
                {
                    months--;
                    days += DateTime.DaysInMonth (today.Year, today.Month);
                }
                if (months < 0)
                {
                    years--;
                    months += 12;
                }
                age = $"{(years > 0 ? years + " Y," : "")} {(months > 0 ? months + " M," : "0 M")} {(days > 0 ? days + " D" : "0 D")}";
                if (string.IsNullOrWhiteSpace (age))
                    age = "0D";
            }
            catch (Exception) { }
            return age;
        }
        public static int GetShortAge (this string? age)
        {
            int shortAge = 0;
            if (!string.IsNullOrWhiteSpace (age))
            {
                string[] parts = age.Split (' ');
                //char[] parts = age.ToCharArray ();
                //string pattern = @"(?<=[a-zA-Z])(?=\d)|(?<=\d)(?=[a-zA-Z])";
                //string[] parts = Regex.Split (age, pattern);

                for (int i = 0; i < parts.Length; i += 1)
                {
                    if (parts[i] == "Y,")
                    {
                            shortAge = int.Parse (parts[i - 1]);
                    }
                    else if (parts[i] == "M,")
                    {
                        int months = 0;
                        if (int.TryParse (parts[i - 1], out months))
                        {
                            if (months >= 5)
                            {
                                shortAge += 1;
                            }
                            break;
                        }
                    }
                }
            }
            return shortAge;
        }
    }
}
