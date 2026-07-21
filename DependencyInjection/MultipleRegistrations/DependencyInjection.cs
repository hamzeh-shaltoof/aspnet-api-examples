namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWeatherServices(this IServiceCollection services) {
        
            services.AddTransient<IWeatherClient, WeatherClient>();
            services.AddTransient<IWeatherService, WeatherService>();
            return services;
        }

        public static IServiceCollection AddSomthing(this IServiceCollection services)
        {
            services.AddTransient<ISomething, SomethingV1>();
            services.AddTransient<ISomething, SomethingV2>();

            return services;
        }
    }
}
