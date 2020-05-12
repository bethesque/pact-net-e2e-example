using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sample.Provider.Pacts.Support
{
    internal class ProviderStateMiddleware
    {
        private readonly RequestDelegate _next;

        public ProviderStateMiddleware([NotNull]RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}