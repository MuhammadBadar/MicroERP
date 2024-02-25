using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Constants
{
    public class NotificationDictionaryKeywords
    {
        public const string Supervisor = @"#SUPERVISOR#";
        public const string User = @"#USER#";
        public const string Date = @"#DATE#";
        public const string Time = @"#TIME#";
        public const string From = @"#FROM#";
        public const string To = @"#TO#";

        // User Tasks
        public const string TMS_Title = @"#TMS_TITLE#";
        public const string TMS_Description = @"#TMS_DESCRIPTION#";
        public const string TMS_DueTime = @"#TMS_DUE_TIME#";
        public const string TMS_Priority = @"#TMS_PRIORITY#";
        public const string TMS_Status = @"#TMS_STATUS#";
    }
}
