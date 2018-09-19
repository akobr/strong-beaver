namespace StrongBeaver.Services.Geolocation
{
    public enum GeolocationAccuracyEnum
    {
        /// <summary>
        /// Represents the lowest accuracy, using the least power to obtain and typically within 1000-5000 meters.
        /// </summary>
        Lowest = 0,
        /// <summary>
        /// Represents low accuracy, typically within 300-3000 meters.
        /// </summary>
        Low = 1,
        /// <summary>
        /// Represents medium accuracy, typically within 30-500 meters.
        /// </summary>
        Medium = 2,
        /// <summary>
        /// Represents high accuracy, typically within 10-100 meters.
        /// </summary>
        High = 3,
        /// <summary>
        /// Represents the best accuracy, using the most power to obtain and typically within 10 meters.
        /// </summary>
        Best = 4
    }
}