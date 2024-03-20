namespace QST.MicroERP.Core.Entities.Security
{
    public class PermissionDE : BaseDomain
    {
        public PermissionDE ( )
        {
            IsReadOnly = null;
        }
        public string? UserId { get; set; }
        public int RoleId { get; set; }
        public string? User { get; set; }
        public string? Role { get; set; }
        public int RouteId { get; set; }
        public string? Route { get; set; }
        public int PermissionId { get; set; }
        public string? Permission { get; set; }
        public bool? IsReadOnly { get; set; }
        public int CltId { get; set; }
    }
}