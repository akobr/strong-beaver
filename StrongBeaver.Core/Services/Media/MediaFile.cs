using System;
using System.IO;

namespace StrongBeaver.Core.Services.Media
{
    public class MediaFile : IMediaFile
    {
        Func<Stream> streamGetter;
        bool isDisposed;

        public MediaFile(string path, Func<Stream> streamGetter, string directory)
        {
            this.streamGetter = streamGetter;

            Path = path;
            Directory = directory;
        }

        public string Path { get; }

        public string Directory { get; }

        public Stream GetStream()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(nameof(MediaFile));
            }

            return streamGetter();
        }

        public void Dispose()
        {
            OnDispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;

            if (disposing)
            {
                streamGetter = null;
            }
        }

        ~MediaFile()
        {
            OnDispose(false);
        }
    }
}