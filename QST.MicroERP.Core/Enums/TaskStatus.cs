using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace QST.MicroERP.Core.Enums
{
    public enum TaskStatus
    {
        Open= 1107001,
        InProgress,
        InTesting,
        ReOpen,
        Resolve,
        Stalled,
        Closed
    }

}
