using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Enums
{
    public enum NotificationTemplates
    {
        None = 0,
        ATT_NotificationToSupervisor_OnDayStart = 1,
        ATT_NotificationToSupervisor_OnDayEnd = 2,
        ATT_TaskDetail = 3,
        ATT_DayEndTaskDetail=4
    }
}
