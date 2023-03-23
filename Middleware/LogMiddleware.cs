using System.Diagnostics;
using lesson1.Interfaces;
using System.Threading.Tasks;

namespace lesson1.Middleware
{
    public class LogMiddleware
    {

        private readonly ILogService logger;
        private readonly RequestDelegate next;

        public LogMiddleware(RequestDelegate next, ILogService logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async System.Threading.Tasks.Task InvokeAsync(HttpContext ctx)
        {
            var sw = new Stopwatch();
            sw.Start();
            await next(ctx);
            sw.Stop();
            logger.Log(LogLevel.Debug,
                $"{ctx.Request.Path}:ms {sw.ElapsedMilliseconds}");
        }
    }
}