using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Permissions
{
    public class PermissionsResult : IPermissionsResult
    {
        private readonly IDictionary<PermissionTypeEnum, PermissionStatusEnum> results;

        public PermissionsResult(IDictionary<PermissionTypeEnum, PermissionStatusEnum> results)
        {
            this.results = results;
        }

        public PermissionsResult()
            : this(new Dictionary<PermissionTypeEnum, PermissionStatusEnum>())
        {
            // Empty
        }

        public PermissionStatusEnum this[PermissionTypeEnum permission]
        {
            get
            {
                if (!results.ContainsKey(permission))
                {
                    return PermissionStatusEnum.Unknown;
                }

                return results[permission];
            }
        }

        public bool Contains(PermissionTypeEnum permission)
        {
            return results.ContainsKey(permission);
        }

        public void SetResult(PermissionTypeEnum permission, PermissionStatusEnum status)
        {
            results[permission] = status;
        }
    }
}