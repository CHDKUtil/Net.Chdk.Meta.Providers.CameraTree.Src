﻿using Net.Chdk.Meta.Model.CameraTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Chdk.Meta.Providers.CameraTree.Src
{
    sealed class SrcCameraTreeProvider : ICameraTreeProvider
    {
        private PlatformProvider PlatformProvider { get; }

        public SrcCameraTreeProvider(PlatformProvider platformProvider)
        {
            PlatformProvider = platformProvider;
        }

        public IDictionary<string, TreePlatformData> GetCameraTree(string path)
        {
            var platformPath = Path.Combine(path, "platform");
            return Directory.EnumerateDirectories(platformPath)
                .Select(Path.GetFileName)
                .Where(platform => !"generic".Equals(platform, StringComparison.InvariantCulture))
                .ToDictionary(platform => platform, platform => GetPlatform(platformPath, platform));
        }

        private TreePlatformData GetPlatform(string platformPath, string platform)
        {
            return PlatformProvider.GetPlatform(platformPath, platform);
        }
    }
}