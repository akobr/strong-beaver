using System;
using System.IO;

namespace StrongBeaver.Core.Services.Media
{
    public interface IMediaFile : IDisposable
    {
        string Path { get; }

        string Directory { get; }

        Stream GetStream();
    }
}