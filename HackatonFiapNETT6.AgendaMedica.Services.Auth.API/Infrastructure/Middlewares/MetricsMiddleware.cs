using Prometheus;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Middlewares;

public class MetricsMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly Counter RequestCounter = Metrics.CreateCounter(
    
        "auth_api_http_requests_total",
        "Contador de requisições HTTP",
        
        new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint", "status_code" }
            });
    
        public MetricsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
    
        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "unknown";
    
            await _next(context);
    
            RequestCounter
                .WithLabels(context.Request.Method, path, context.Response.StatusCode.ToString())
                .Inc();
        }
}