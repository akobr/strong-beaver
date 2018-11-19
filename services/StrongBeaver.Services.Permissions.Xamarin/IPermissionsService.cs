using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;
using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Permissions
{
    public interface IPermissionsService : IService
    {
        Task<PermissionStatus> CheckPermissionStatusAsync(Permission permission);

        Task<Dictionary<Permission, PermissionStatus>> RequestPermissionsAsync(params Permission[] permissions);

        Task<bool> ShouldShowRequestPermissionRationaleAsync(Permission permission);
    }
}