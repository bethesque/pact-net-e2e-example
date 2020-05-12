using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Sample.Provider
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/thing", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
