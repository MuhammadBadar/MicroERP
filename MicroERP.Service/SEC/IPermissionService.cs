using MicroERP.Core.Entities;
using MicroERP.Core.Entities.CTL;
using MicroERP.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Service.IServices
{
    public interface IPermissionService
    {
        List<PermissionDE> SearchPermissions ( PermissionDE mod );
        List<PermissionDE> GetPermsByUserOrRole ( string UserId, int RoleId );
        bool SavePermissions ( List<PermissionDE> perms );
        List<SettingsDE> GetRoutes ( );
    }
}
