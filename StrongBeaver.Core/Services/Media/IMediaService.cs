using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Media
{
    public interface IMediaService : IService
    {
        Task<bool> Initialize();

        bool IsCameraAvailable { get; }

        bool IsTakePhotoSupported { get; }

        bool IsPickPhotoSupported { get; }

        bool IsTakeVideoSupported { get; }

        bool IsPickVideoSupported { get; }

        Task<IMediaFile> PickPhotoAsync();

        Task<IMediaFile> PickPhotoAsync(PickMediaOptions options);

        Task<IMediaFile> TakePhotoAsync(StoreCameraMediaOptions options);

        Task<IMediaFile> PickVideoAsync();

        Task<IMediaFile> TakeVideoAsync(StoreVideoOptions options);
    }
}