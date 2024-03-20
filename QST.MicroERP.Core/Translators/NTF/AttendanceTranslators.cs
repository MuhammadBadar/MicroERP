using QST.MicroERP.Core.Constants;
using QST.MicroERP.Core.Entities.ATT;
using QST.MicroERP.Core.Entities.NTF;
using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Common;

namespace QST.MicroERP.Core.Translators.NTF
{
    public static class AttendanceTranslators
    {
        public static Dictionary<string, string> Translate(this AttendanceDE mod, ref Dictionary<string, string> dict)
        {
            //_keys = configurations;
            if (!dict.ContainsKey(NotificationDictionaryKeywords.Supervisor))
                dict.Add(NotificationDictionaryKeywords.Supervisor, mod.Supervisor);
            if(!dict.ContainsKey(NotificationDictionaryKeywords.Date))
                dict.Add(NotificationDictionaryKeywords.Date, mod.Date.Value.ToShortDateString());
            if (!dict.ContainsKey(NotificationDictionaryKeywords.Time))
                dict.Add(NotificationDictionaryKeywords.Time, mod.Date.Value.ToShortTimeString());
            if (!dict.ContainsKey (NotificationDictionaryKeywords.User))
                dict.Add (NotificationDictionaryKeywords.User, mod.User);
            if (!dict.ContainsKey (NotificationDictionaryKeywords.CompanyName))
                dict.Add (NotificationDictionaryKeywords.CompanyName, CompanyConstants.COMPANY_NAME);
            if (!dict.ContainsKey (NotificationDictionaryKeywords.SiteUrl))
                dict.Add (NotificationDictionaryKeywords.SiteUrl, CompanyConstants.SITE_URL);
            if (!dict.ContainsKey (NotificationDictionaryKeywords.WebMasterDisplayName))
                dict.Add (NotificationDictionaryKeywords.WebMasterDisplayName, CompanyConstants.WEB_MASTER_DISPLAY_NAME);

            return dict;
        }

        public static Dictionary<string, string> Translate(this UserTaskDE mod, ref Dictionary<string, string> dict)
        {
            //_keys = configurations;
            if (!dict.ContainsKey(NotificationDictionaryKeywords.TMS_Title))
                dict.Add(NotificationDictionaryKeywords.TMS_Title, mod.Title);
            if (!dict.ContainsKey (NotificationDictionaryKeywords.TMS_WorkTime))
                dict.Add (NotificationDictionaryKeywords.TMS_WorkTime, mod.WorkTime.ToString());
            if (!dict.ContainsKey(NotificationDictionaryKeywords.TMS_Description))
                dict.Add(NotificationDictionaryKeywords.TMS_Description, mod.Description??"---");
            if (!dict.ContainsKey(NotificationDictionaryKeywords.TMS_DueTime))
                dict.Add(NotificationDictionaryKeywords.TMS_DueTime, mod.Sp.ToString());
            if (!dict.ContainsKey (NotificationDictionaryKeywords.TMS_Comment))
                dict.Add (NotificationDictionaryKeywords.TMS_Comment, mod.Comments);
            if (!dict.ContainsKey (NotificationDictionaryKeywords.TMS_ClaimPercent))
                dict.Add (NotificationDictionaryKeywords.TMS_ClaimPercent, ((mod.ApprovedClaim ?? mod.ClaimPercent) ?? "0") + "%");
            if (!dict.ContainsKey (NotificationDictionaryKeywords.TMS_Formatted_Title))
                dict.Add (NotificationDictionaryKeywords.TMS_Formatted_Title, mod.Title.ToString ());
            if (!dict.ContainsKey(NotificationDictionaryKeywords.TMS_Priority))
                dict.Add(NotificationDictionaryKeywords.TMS_Priority, mod.Priority);
            if (!dict.ContainsKey(NotificationDictionaryKeywords.TMS_Status))
                dict.Add(NotificationDictionaryKeywords.TMS_Status, mod.Status);
            return dict;
        }

        public static NotificationLogDE Translate(this NotificationTemplateDE temp, Dictionary<string, string> dict)
        {
            NotificationLogDE mod = new NotificationLogDE();

            mod.Subject = temp.Subject;
            mod.Body = temp.Body;

            foreach (KeyValuePair<string, string> pair in dict)
            {
                //if (!string.IsNullOrWhiteSpace(mod.Subject))
                    mod.Subject = string.IsNullOrWhiteSpace(temp.Subject) ? string.Empty : mod.Subject.Replace(pair.Key, pair.Value, StringComparison.InvariantCultureIgnoreCase);
                //if (!string.IsNullOrWhiteSpace(mod.Body))
                    mod.Body = string.IsNullOrEmpty(temp.Body) ? string.Empty : mod.Body.Replace(pair.Key, pair.Value, StringComparison.InvariantCultureIgnoreCase);
            }
            return mod;
        }

        public static NotificationLogDE AddTaskLine(this NotificationLogDE notificationLog, string lineBaseContent, Dictionary<string, string> dict, int sectionNo)
        {
            notificationLog.Body = notificationLog.Body.Replace("\r\n", string.Empty);

            //var startPoint = $"<!-- Detail Section Start Section {sectionNo} -->";
            var startPoint = $"<!-- Detail Section Start Section 1 -->";
            //var endPoint = $"<!-- Detail Section End Section {sectionNo} -->";
            var endPoint = $"<!-- Detail Section End Section 1 -->";
            var startIndex = lineBaseContent.IndexOf(startPoint) + startPoint.Length;
            var endIndex = lineBaseContent.IndexOf(endPoint);

            var templateHTML = lineBaseContent[startIndex..endIndex];

            var content = notificationLog.Body;
            //startPoint = $"<!-- Detail Section Start Section {sectionNo} -->";
            startPoint = $"<!-- Detail Section Start Section 1 -->";
            //endPoint = $"<!-- Detail Section End Section {sectionNo} -->";
            endPoint = $"<!-- Detail Section End Section 1 -->";
            startIndex = content.IndexOf(startPoint) + startPoint.Length;
            endIndex = content.IndexOf(endPoint);

            //if (mode == OperationModes.Update)

            if (sectionNo > 1)
                notificationLog.Body = notificationLog.Body.Insert(endIndex, templateHTML);

            foreach (KeyValuePair<string, string> pair in dict)
                notificationLog.Body = notificationLog.Body.Replace(pair.Key, pair.Value, StringComparison.InvariantCultureIgnoreCase);

            return notificationLog;
        }
    }
}
