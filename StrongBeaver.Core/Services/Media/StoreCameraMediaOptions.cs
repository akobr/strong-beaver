using System;

namespace StrongBeaver.Core.Services.Media
{
    public class StoreCameraMediaOptions : StoreMediaOptions
    {
        int customPhotoSize = 100;
        int quality = 100;

        /// <summary>
        /// Allow cropping on photos and trimming on videos.
        /// If null will use default.
        /// Photo: UWP cropping can only be disabled on full size.
        /// Video: UWP trimming when disabled won't allow time limit to be set.
        /// </summary>
        public bool? AllowCropping { get; set; } = null;

        /// <summary>
        /// Default camera.
        /// Should work on iOS and Windows, but not guaranteed on Android as not every camera implements it.
        /// </summary>
        public CameraDeviceTypeEnum DefaultCamera { get; set; }

        /// <summary>
        /// Get or set for an OverlayViewProvider.
        /// </summary>
        public Func<object> OverlayViewProvider { get; set; }

        /// <summary>
        // Get or set if the image should be stored public.
        /// </summary>
        public bool SaveToAlbum { get; set; }

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