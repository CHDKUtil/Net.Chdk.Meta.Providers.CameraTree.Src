using Microsoft.Extensions.Logging;
using Net.Chdk.Meta.Model.CameraTree;

namespace Net.Chdk.Meta.Providers.CameraTree.Src
{
    sealed class RevisionProvider
    {
        private ILogger Logger { get; }
        private SourceProvider SourceProvider { get; }
        private EncodingProvider EncodingProvider { get; }

        public RevisionProvider(SourceProvider sourceProvider, EncodingProvider encodingProvider, ILogger<RevisionProvider> logger)
        {
            SourceProvider = sourceProvider;
            EncodingProvider = encodingProvider;
            Logger = logger;
        }

        public TreeRevisionData GetRevisionData(string platformPath, string platform, string revision)
        {
            Logger.LogTrace("Enter {0}", revision);

            var source = GetSource(platformPath, platform, revision);
            var sourcePlatform = GetPlatform(source, platform);
            var sourceRevision = GetRevision(source, revision);
            if (!sourcePlatform.Equals(platform) || !sourceRevision.Equals(revision))
                Logger.LogTrace("Source: {0}-{1}", sourcePlatform, sourceRevision);

            var encoding = GetEncoding(platformPath, sourcePlatform, sourceRevision);
            if (encoding != null)
                Logger.LogTrace("Encoding: {0}", encoding.Version);

            var revisionData = new TreeRevisionData
            {
                Source = source,
                Encoding = encoding,
            };

            Logger.LogTrace("Leave {0}", revision);
            return revisionData;
        }

        private TreeSourceData GetSource(string platformPath, string platform, string revision)
        {
            return SourceProvider.GetSource(platformPath, platform, revision);
        }

        private TreeEncodingData GetEncoding(string platformPath, string platform, string revision)
        {
            return EncodingProvider.GetEncoding(platformPath, platform, revision);
        }

        private string GetPlatform(TreeSourceData source, string platform)
        {
            return source != null
                ? source.Platform ?? platform
                : platform;
        }

        private string GetRevision(TreeSourceData source, string revision)
        {
            return source != null
                ? source.Revision ?? revision
                : revision;
        }
    }
}
