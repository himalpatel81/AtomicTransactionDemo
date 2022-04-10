using Hangfire;
using Hangfire.SqlServer;

namespace AtomicTransactionDemo.Extension;

public static class HangFireExtension
{
    public static IServiceCollection RegisterHangFire(this IServiceCollection services, IConfiguration myconfiguration)
    {
        services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(myconfiguration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));

        // Add the processing server as IHostedService
        services.AddHangfireServer();
        return services;
    }
}
