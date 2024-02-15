﻿using MicroERP.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicroERP.Core.Entities
{
    public class ResultMsg
    {
        public string? MessageType { get; set; }
        public ResultCodes ResultCode { get; set; }
        public string? Message { get; set; }
    }
}