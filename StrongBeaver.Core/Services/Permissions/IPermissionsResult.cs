namespace StrongBeaver.Core.Services.Permissions
{
    public interface IPermissionsResult
    {
        PermissionStatusEnum this[PermissionTypeEnum permission] { get; }

        bool Contains(PermissionTypeEnum permission);
    }
}