using Microsoft.AspNetCore.Builder;

namespace Sample.Provider.Pacts.Support
{
    internal class TestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            var apiStartup = new Startup();
            app.UseMiddleware<ProviderStateMiddleware>();
            apiStartup.Configure(app);
        }
    }
}