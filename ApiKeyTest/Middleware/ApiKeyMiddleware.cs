using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace ApiKeyTest.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            IHeaderDictionary headersDictionary = context.Request.Headers;
            
            // GetTypedHeaders extension method provides strongly typed access to many headers
            // For known header, knownheaderValues has 1 item and knownheaderValue is the value
            // Obtain strongly typed header class in ASP.NET Core using AuthenticationHeaderValue.Parse
            
            var authenticationHeaderValue = AuthenticationHeaderValue.Parse(headersDictionary[HeaderNames.Authorization]);
            
            if (!(authenticationHeaderValue.Scheme == ApiKeyValues.ApplicationKeyAuthenticationScheme &&
                ApiKeyValues.ApplicationKeyValue == new Guid(authenticationHeaderValue.Parameter)))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized Application");
                return;
            }
            
            // Call the next delegate/middleware in the pipeline
            await _next.Invoke(context);
        }
    }
}
