namespace StrongBeaver.Core.Services.Media
{
    public enum PhotoSizeEnum
    {
        /// <summary>
        /// 25% of original.
        /// </summary>
        Small,
        /// <summary>
        /// 50% of the original.
        /// </summary>
        Medium,
        /// <summary>
        /// 75% of the original.
        /// </summary>
        Large,
        /// <summary>
        /// Untouched.
        /// </summary>
        Full,
        /// <summary>
        /// Custom size between 1-100.
        /// Must set the CustomPhotoSize value.
        /// Only applies to iOS and Android.
        /// Windows will auto configure back to small, medium, large, and full.
        /// </summary>
        Custom
    }
}