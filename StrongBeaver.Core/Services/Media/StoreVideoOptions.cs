using System;

namespace StrongBeaver.Core.Services.Media
{
    public class StoreVideoOptions : StoreCameraMediaOptions
    {
        public StoreVideoOptions()
        {
            DesiredQuality = VideoQualityEnum.High;
            DesiredLength = TimeSpan.FromMinutes(10);
        }

        public TimeSpan DesiredLength { get; set; }

        public VideoQualityEnum DesiredQuality { get; set; }
    }
}