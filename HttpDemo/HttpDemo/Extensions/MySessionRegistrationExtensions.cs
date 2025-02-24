using HttpDemo.SessionStorage;

namespace HttpDemo.Extensions
{
    public static class MySessionRegistrationExtensions
    {
        public static IServiceCollection AddMySession(this IServiceCollection services)
        {
            services.AddSingleton<IMySessionStorageEngine>(services =>
            {
                var path = Path.Combine(services.GetRequiredService<IHostEnvironment>().ContentRootPath, "sessions");
                Directory.CreateDirectory(path);

                return new FileSessionStorageEngine(path);
            });
            services.AddSingleton<IMySessionStorage, MySessionStorage>();
            services.AddScoped<MySessionScopedContainer>();

            return services;
        }
    }
}
