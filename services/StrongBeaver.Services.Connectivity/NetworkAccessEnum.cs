namespace StrongBeaver.Services.Connectivity
{
    public enum NetworkAccessEnum
    {
        /// <summary>
        /// The state of the connectivity is not known.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// No connectivity.
        /// </summary>
        None = 1,
        /// <summary>
        /// Local network access only.
        /// </summary>
        Local = 2,
        /// <summary>
        /// Limited internet access.
        /// </summary>
        ConstrainedInternet = 3,
        /// <summary>
        /// Local and Internet access.
        /// </summary>
        Internet = 4,
    }
}