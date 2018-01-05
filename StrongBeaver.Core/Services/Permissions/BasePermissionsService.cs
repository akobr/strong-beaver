using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Permissions
{
    public abstract class BasePermissionsService : BaseService, IPermissionsService
    {
        public abstract Task<bool> ShouldShowRequestPermissionRationaleAsync(PermissionTypeEnum permission);

        public abstract Task<PermissionStatusEnum> CheckPermissionStatusAsync(PermissionTypeEnum permission);

        public Task<IPermissionsResult> CheckPermissionsStatusAsync(params PermissionTypeEnum[] permissions)
        {
            return ProcessPermissionsAsync(permissions, CheckPermissionStatusAsync);
        }

        public abstract Task<PermissionStatusEnum> RequestPermissionAsync(PermissionTypeEnum permission);

        public Task<IPermissionsResult> RequestPermissionsAsync(params PermissionTypeEnum[] permissions)
        {
            return ProcessPermissionsAsync(permissions, RequestPermissionAsync);
        }

        private async Task<IPermissionsResult> ProcessPermissionsAsync(
            IEnumerable<PermissionTypeEnum> permissions,
            Func<PermissionTypeEnum, Task<PermissionStatusEnum>> permissionOperation)
        {
            PermissionsResult result = new PermissionsResult();

            foreach (PermissionTypeEnum permission in permissions)
            {
                if (result.Contains(permission))
                {
                    continue;
                }

                result.SetResult(permission, await permissionOperation(permission));
            }

            return result;
        }
    }
}