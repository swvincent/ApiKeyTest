namespace ApiKeyTest.Middleware
{
    public static class RequestApiKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKey(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
