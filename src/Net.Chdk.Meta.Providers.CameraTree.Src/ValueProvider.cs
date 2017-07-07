using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Net.Chdk.Meta.Providers.CameraTree.Src
{
    abstract class ValueProvider
    {
        private ILogger Logger { get; }

        protected ValueProvider(ILogger logger)
        {
            Logger = logger;
        }

        protected string GetFilePath(string platformPath, string platform, string revision)
        {
            var path = GetPath(platformPath, platform, revision);
            var filePath = Path.Combine(path, FileName);
            if (!File.Exists(filePath))
            {
                var name = GetName(platform, revision);
                throw new InvalidOperationException($"{name}: {FileName} missing");
            }

            return filePath;
        }

        protected string GetPath(string platformPath, string platform, string revision)
        {
            return revision != null
                ? Path.Combine(platformPath, platform, "sub", revision)
                : Path.Combine(platformPath, platform);
        }

        protected string GetName(string platform, string revision = null)
        {
            return revision != null
                ? $"{platform}-{revision}"
                : platform;
        }

        protected abstract string FileName { get; }
    }
}
