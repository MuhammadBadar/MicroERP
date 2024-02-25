using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.NTF
{
    public class NotificationLogDE : BaseDomain
    {
        public NotificationLogDE()
        {
            IsSent = false;
            IsActive = true;
            //DateTime = DateTime.Now;
        }

        public string UserId { get; set; } = string.Empty;
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsSent { get; set; }    
        public string SMS { get; set; } = string.Empty;

    }

    public class NotificationLogSearchCriteria : BaseDomain
    {
        public NotificationLogSearchCriteria()
        {
            IsActive = true;
        }
        public string UserId { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool? IsSent { get; set; } = false;
        public string SMS { get; set; } = string.Empty;
    }
}
