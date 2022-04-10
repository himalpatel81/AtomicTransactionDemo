using AtomicTransactionDemo.Extension;

namespace AtomicTransactionDemo.Extension;

using AtomicTransactionDemo.Jobs;
using Quartz;
public static class QuartzExtension
{
    public static IServiceCollection ConfigureQuartz(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();

            // Register the job, loading the schedule from configuration
            q.AddJobAndTrigger<MonitorCustomerCreateJob>(configuration);
        });
        
        services.AddQuartzServer(q => q.WaitForJobsToComplete = true);
        
        return services;
    }
}
