namespace StrongBeaver.Core.Services.Device.Connectivity
{
    public enum ConnectionTypeEnum
    {
        /// <summary>
        /// No connection.
        /// </summary>
        NotConnected = -1,
        /// <summary>
        /// Other (unknown) type of connection.
        /// </summary>
        Other = 0,
        /// <summary>
        /// Bluetooth connection.
        /// </summary>
        Bluetooth = 1,
        /// <summary>
        /// Cellular connection, 3G, Edge, 4G, LTE.
        /// </summary>
        Cellular,
        /// <summary>
        /// Wifi connection.
        /// </summary>
        WiFi,
        /// <summary>
        /// Desktop or ethernet connection.
        /// </summary>
        Desktop,
        /// <summary>
        /// Wimax (only android).
        /// </summary>
        Wimax,
    }
}
