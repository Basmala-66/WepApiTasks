namespace Health_Care_System.Middlewares
{
    public class RateLimitingMiddleware(RequestDelegate next)
    {
        private static int _counter = 0;
        private static DateTime _lastRequestData = DateTime.Now;

        public async Task InvokeAsync(HttpContext context)
        {
            _counter++;

            if(DateTime.Now.Subtract(_lastRequestData).Seconds >10)
            {
                _counter = 1;
                _lastRequestData = DateTime.Now;
                await next(context);
            }

            else
            {
                if(_counter > 15)
                {
                    _lastRequestData = DateTime.Now;
                    await context.Response.WriteAsync("rate limit exceded");
                }
                else
                {
                    _lastRequestData = DateTime.Now;
                    await next(context);
                }
            }
        }
    }
}
