using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities.NTF
{
    public class NotificationTemplateDE : BaseDomain
    {
        public string? KeyCode { get; set; }
        public string? TemplateName { get; set; }
        public string? Body { get; set; }
        public string? Subject { get; set; }
        public string? SMS { get; set; }

    }
}
