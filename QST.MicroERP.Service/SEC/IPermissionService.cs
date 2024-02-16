using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Service.IServices
{
    public interface IPermissionService
    {
        List<PermissionDE> SearchPermissions ( PermissionDE mod );
        List<PermissionDE> GetPermsByUserOrRole ( string UserId, int RoleId );
        bool SavePermissions ( List<PermissionDE> perms );
        List<SettingsDE> GetRoutes ( );
    }
}
