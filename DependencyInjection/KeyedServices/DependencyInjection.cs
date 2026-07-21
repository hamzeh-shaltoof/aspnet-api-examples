namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
     
        public static IServiceCollection AddSomthing(this IServiceCollection services)
        {
            services.AddTransient<ISomething, SomethingV1>();
            services.AddTransient<ISomething, SomethingV2>();

            return services;
        }
    }
}
