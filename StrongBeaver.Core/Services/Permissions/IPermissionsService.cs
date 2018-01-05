using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Permissions
{
    public interface IPermissionsService : IService
    {
        Task<bool> ShouldShowRequestPermissionRationaleAsync(PermissionTypeEnum permission);

        Task<PermissionStatusEnum> CheckPermissionStatusAsync(PermissionTypeEnum permission);

        Task<IPermissionsResult> CheckPermissionsStatusAsync(params PermissionTypeEnum[] permissions);

        Task<PermissionStatusEnum> RequestPermissionAsync(PermissionTypeEnum permission);

        Task<IPermissionsResult> RequestPermissionsAsync(params PermissionTypeEnum[] permissions);
    }
}