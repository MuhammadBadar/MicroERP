﻿using QST.MicroERP.Core.Enums;

namespace QST.MicroERP.Core.Entities.SEC
{
    public class RequiredPermission
    {
        public RequiredPermission(string resourceId, PermissionActions action)
        {
            ResourceId = resourceId;
            Action = action;
        }

        public string ResourceId { get; }

        public PermissionActions Action { get; }
    }
}
