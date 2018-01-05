namespace StrongBeaver.Core.Services.Media
{
    public class PickMediaOptions
    {
        int customPhotoSize = 100;
        int quality = 100;

        public PhotoSizeEnum PhotoSize { get; set; } = PhotoSizeEnum.Full;

        /// <summary>
        /// The custom photo size to use, 100 full size (same as Full),
        /// and 1 being smallest size at 1% of original.
        /// Default is 100.
        /// </summary>
        public int CustomPhotoSize
        {
            get { return customPhotoSize; }
            set
            {
                if (value > 100)
                {
                    customPhotoSize = 100;
                }
                else if (value < 1)
                {
                    customPhotoSize = 1;
                }
                else
                {
                    customPhotoSize = value;
                }
            }
        }

        /// <summary>
        /// The compression quality to use, 0 is the maximum compression (worse quality),
        /// and 100 minimum compression (best quality).
        /// Default is 100.
        /// </summary>
        public int CompressionQuality
        {
            get { return quality; }
            set
            {
                if (value > 100)
                {
                    quality = 100;
                }
                else if (value < 0)
                {
                    quality = 0;
                }
                else
                {
                    quality = value;
                }
            }
        }
    }
}