namespace API.Middleware
{
    // Middleware is a component in the HTTP request pipeline that can inspect, modify, or handle the request and response.
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}