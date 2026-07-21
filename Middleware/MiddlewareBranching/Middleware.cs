namespace MiddlewareBranching
{
    public static class Middleware
    {
        public static void Branch1(IApplicationBuilder app)
        {
            CommonBranch(app);
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 3 ");
                await next();
            });
            app.Use((RequestDelegate next) =>
            {
                return async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Middleware 4 ");
                    await next(context);
                };
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 5 ");
                await next();
            });
            app.Use((RequestDelegate next) =>
            {
                return async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Middleware 6 ");
                    await next(context);
                };
            });
            app.Run(async (HttpContext context) =>
            {
                await context.Response.WriteAsync("Middleware Final ");
            });
        }
        public static void Branch2(IApplicationBuilder app)
        {
            CommonBranch(app);
            app.Use((RequestDelegate next) =>
            {
                return async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Middleware 8 ");
                    await next(context);
                };
            });
            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Middleware 9 ");
                await next();
            });
            app.Use((RequestDelegate next) =>
            {
                return async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Middleware 10 ");
                    await next(context);
                };
            });
            app.Run(async (HttpContext context) =>
            {
                await context.Response.WriteAsync("Middleware Final ");
            });

        }

        private static void CommonBranch(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 1 ");
                await next();
            });
            app.Use((RequestDelegate next) =>
            {
                return async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Middleware 2 ");
                    await next(context);
                };
            });
        }
    }
}
