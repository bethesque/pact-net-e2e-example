using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Sample.Provider.Pacts.Support
{
    internal class TestStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var apiStartup = new Startup();
            app.UseMiddleware<ProviderStateMiddleware>();
            apiStartup.Configure(app, env);
        }
    }
}