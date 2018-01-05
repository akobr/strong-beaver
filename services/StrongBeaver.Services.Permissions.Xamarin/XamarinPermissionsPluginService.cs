using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Permissions
{
    public class XamarinPermissionsPluginService : BasePermissionsService
    {
        private IPermissions permissions;
        public XamarinPermissionsPluginService()
        {
            permissions = CrossPermissions.Current;
        }

        public override async Task<PermissionStatusEnum> CheckPermissionStatusAsync(PermissionTypeEnum permission)
        {
            if (!CrossPermissions.IsSupported)
            {
                return PermissionStatusEnum.Unknown;
            }

            return TranslatePermissionStatus(await permissions.CheckPermissionStatusAsync(TranslatePermissionType(permission)));
        }

        public override async Task<PermissionStatusEnum> RequestPermissionAsync(PermissionTypeEnum permission)
        {
            if (!CrossPermissions.IsSupported)
            {
                return PermissionStatusEnum.Unknown;
            }

            Permission translatedPermission = TranslatePermissionType(permission);
            return TranslatePermissionStatus((await permissions.RequestPermissionsAsync())[translatedPermission]);
        }

        public override async Task<bool> ShouldShowRequestPermissionRationaleAsync(PermissionTypeEnum permission)
        {
            if (!CrossPermissions.IsSupported)
            {
                return false;
            }

            return await permissions.ShouldShowRequestPermissionRationaleAsync(TranslatePermissionType(permission));
        }

        private Permission TranslatePermissionType(PermissionTypeEnum permissionType)
        {
            return (Permission)(int)permissionType;
        }

        private PermissionStatusEnum TranslatePermissionStatus(PermissionStatus permissionStatus)
        {
            switch (permissionStatus)
            {
                case PermissionStatus.Denied:
                    return PermissionStatusEnum.Denied;

                case PermissionStatus.Disabled:
                    return PermissionStatusEnum.Disabled;

                case PermissionStatus.Granted:
                    return PermissionStatusEnum.Granted;

                case PermissionStatus.Restricted:
                    return PermissionStatusEnum.Restricted;

                // case PermissionStatus.Unknown:
                default:
                    return PermissionStatusEnum.Unknown;
            }
        }
    }
}
