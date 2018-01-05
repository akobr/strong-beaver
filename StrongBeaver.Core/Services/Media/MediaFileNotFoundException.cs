using System;
using StrongBeaver.Core.Exceptions;

namespace StrongBeaver.Core.Services.Media
{
    public class MediaFileNotFoundException : CoreException
    {
        public MediaFileNotFoundException(string path)
          : base($"Unable to locate media file at {path}.")
        {
            Path = path;
        }

        public MediaFileNotFoundException(string path, Exception innerException)
          : base($"Unable to locate media file at {path}", innerException)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}