using QST.MicroERP.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.Entities.SEC
{
    public class UserDE
    {
        public UserDE ( ) { Permissions = new List<PermissionDE> (); }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? UserPassword { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public  string? Id { get; set; }
        public String? UserName { get; set; }
        public int RoleId { get; set; }
        public string? FatherName { get; set; }
        public string? CNIC { get; set; }
        public string? Address { get; set; }
        public string? Designation { get; set; }
        public int ClientId { get; set; }
        public string? Client { get; set; }
        public string? ModuleIds { get; set; }
        public int ModuleId { get; set; }
        public string? Module { get; set; }
        public string? CLTModuleIds { get; set; }
        public string? SupervisorId { get; set; }
        public string? Supervisor { get; set; }
        public string? SupervisorEmail { get; set; }
        public int DoctorId { get; set; }
        public bool IsActive { get; set; }
        public List<PermissionDE> Permissions { get; set; }
        public bool IncludeSubordinatesData { get; set; }
        public int CltId { get; set; }
        public string? CtlName { get; set; }

    }
}
