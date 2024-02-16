﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.LMS
{
    public class TopicDE : BaseDomain
    {

        public string? TopicTitle { get; set; }
        public string? Description { get; set; }
        public int CourseId { get; set; }
        public string? Course { get; set; }
    }
}