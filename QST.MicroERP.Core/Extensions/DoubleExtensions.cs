using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToHHMMSS ( this double hours )
        {
            TimeSpan timeSpan = TimeSpan.FromHours (hours);
            if (timeSpan.TotalHours >= 1)
            {
                return $"{(int)timeSpan.TotalHours:D2} Hours";
            }
            else if (timeSpan.TotalMinutes >= 1)
            {
                return $"{(int)timeSpan.TotalMinutes:D2} Minutes";
            }
            else
            {
                return $"{(int)timeSpan.TotalSeconds:D2} Seconds";
            }
        }
        public static string ToHHMM ( this double hours )
        {
            TimeSpan timeSpan = TimeSpan.FromHours (hours);
            StringBuilder formattedTime = new StringBuilder ();
            formattedTime.Append($"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}");
            if (timeSpan.TotalHours >= 1)
                formattedTime.Append (" Hour(s)");
            else if (timeSpan.TotalMinutes >= 1)
                formattedTime.Append (" Minute(s)");
            return formattedTime.ToString ();
        }
    }
}
