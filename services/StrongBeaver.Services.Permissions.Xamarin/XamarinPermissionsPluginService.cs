using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace StrongBeaver.Services.Permissions
{
    public class XamarinPermissionsPluginService : IPermissionsService
    {
        private readonly IPermissions facade;
        public XamarinPermissionsPluginService()
        {
            facade = CrossPermissions.Current;
        }

        public Task<PermissionStatus> CheckPermissionStatusAsync(Permission permission)
        {
            return facade.CheckPermissionStatusAsync(permission);
        }

        public Task<Dictionary<Permission, PermissionStatus>> RequestPermissionsAsync(params Permission[] permissions)
        {
            return facade.RequestPermissionsAsync(permissions);
        }

        public Task<bool> ShouldShowRequestPermissionRationaleAsync(Permission permission)
        {
            return facade.ShouldShowRequestPermissionRationaleAsync(permission);
        }
    }
}
