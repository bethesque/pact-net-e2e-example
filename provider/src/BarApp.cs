using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Sample.Provider
{
    public class BarApp
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/thing", async context =>
                {
                    object body = new
                    {
                        Company = "My Company",
                        Factories = new object[] {
                            new {
                                Location = "Sydney",
                                Capacity = 5
                            },
                            new {
                                Location = "Sydney",
                                GeographicCoords = "-0.145,1.4445",
                                Capacity = 5
                            }
                        }
                    };

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(body, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
                });
            });
        }
    }
}
