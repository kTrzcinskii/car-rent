namespace AppRental.Infrastructure.Middleware
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _config = config;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API key missing");
                return;
            }

            var apiKey0 = _config["Auth:ApiKey0"];
            if(apiKey0 == null || !apiKey0.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API keys don't match");
                return;
            }

            await _next(context);
        }
    }
}