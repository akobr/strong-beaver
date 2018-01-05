namespace StrongBeaver.Core.Services.Permissions
{
    public enum PermissionStatusEnum
    {
        /// <summary>
        /// Permission is in an unknown state.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Denied by user.
        /// </summary>
        Denied,
        /// <summary>
        /// Feature is disabled on device.
        /// </summary>
        Disabled,
        /// <summary>
        /// Restricted (only iOS).
        /// </summary>
        Restricted,
        /// <summary>
        /// Granted by user.
        /// </summary>
        Granted,
    }
}