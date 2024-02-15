﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Core.Entities.NTF
{
    public class NotificationLogDE : BaseDomain
    {
        public string? UserId { get; set; }
        public string? Phone { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsSent { get; set; }
        public string? SMS { get; set; }
        public NotificationLogDE()
        {
            IsSent = false;
            DateTime = DateTime.Now;
        }

    }
}