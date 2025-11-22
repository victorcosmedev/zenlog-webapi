using System.Diagnostics;

namespace ZenLogAPI.Middlewares
{
    public class TracingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ActivitySource ActivitySource = new ActivitySource("ZenLogAPI");
        public TracingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogger<TracingMiddleware> logger)
        {
            using var activity = ActivitySource.StartActivity("HTTP Request", ActivityKind.Server);

            if(activity != null)
            {
                activity.SetTag("http.method", context.Request.Method);
                activity.SetTag("http.url", context.Request.Path);
            }

            var traceId = activity?.TraceId.ToString() ?? Guid.NewGuid().ToString();

            logger.LogInformation("Iniciando requisição. TraceId={TraceId}", traceId);

            context.Response.Headers.Add("trace-id", traceId);

            await _next(context);

            logger.LogInformation("Finalizando requisição. TraceId={TraceId}", traceId);
        }
    }
}
