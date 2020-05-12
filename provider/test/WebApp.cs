using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Sample.Provider.Pacts
{
    internal static class WebApp
    {
        public static IDisposable Start<TStartup>(string listenUri) where TStartup : class
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls(listenUri)
                        .UseStartup<TStartup>();
                });

            return builder.Build();
        }
    }
}