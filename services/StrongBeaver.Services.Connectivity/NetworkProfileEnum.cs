// ReSharper disable InconsistentNaming
namespace StrongBeaver.Services.Connectivity
{
    public enum NetworkProfileEnum
    {
        /// <summary>
        /// Bluetooth connection.
        /// </summary>
        Bluetooth = 0,
        /// <summary>
        /// Cellular connection, 3G, Edge, 4G, LTE.
        /// </summary>
        Cellular = 1,
        /// <summary>
        /// Ethernet connection.
        /// </summary>
        Ethernet = 2,
        /// <summary>
        /// Wimax (only android).
        /// </summary>
        WiMAX = 3,
        /// <summary>
        /// Wifi connection.
        /// </summary>
        WiFi = 4,
        /// <summary>
        /// Other (unknown) type of connection.
        /// </summary>
        Other = 5,
    }
}
