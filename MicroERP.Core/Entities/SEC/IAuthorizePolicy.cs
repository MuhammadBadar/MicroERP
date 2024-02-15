namespace MicroERP.Core.Entities.SEC
{
    public interface IAuthorizePolicy
    {
        RequiredPermission[] Permissions { get; }

        string Key { get; }
    }
}
