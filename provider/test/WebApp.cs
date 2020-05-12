using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Sample.Provider.Pacts
{
    internal class WebApp : IAsyncDisposable
    {
        private readonly IHost _host;

        private WebApp(IHost host) => _host = host;

        public static async Task<IAsyncDisposable> Start<TStartup>(string listenUri) where TStartup : class
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls(listenUri)
                        .UseStartup<TStartup>();
                });

            var app = new WebApp(builder.Build());
            await app.StartAsync();

            return app;
        }

        private async Task StartAsync()
        {
            await _host.StartAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _host.StopAsync();
        }
    }
}