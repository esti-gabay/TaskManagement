
namespace lesson1.Middleware
{

    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseLogMiddleware(
            this IApplicationBuilder app
        )
        {
            return app.UseMiddleware<LogMiddleware>();

        }
    }
}