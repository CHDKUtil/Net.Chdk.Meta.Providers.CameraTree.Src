﻿using Microsoft.Extensions.DependencyInjection;

namespace Net.Chdk.Meta.Providers.CameraTree.Src
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSrcCameraTreeProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<ICameraTreeProvider, SrcCameraTreeProvider>()
                .AddSingleton<SourceProvider>()
                .AddSingleton<EncodingProvider>()
                .AddSingleton<PlatformProvider>()
                .AddSingleton<CameraProvider>()
                .AddSingleton<RevisionProvider>();
        }
    }
}
