﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.LMS
{
    public class BranchschoolDE : BaseDomain
    {
        public int SchoolId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? CellNo { get; set; }
    }
}
