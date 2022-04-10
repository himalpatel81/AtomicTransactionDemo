using AtomicTransactionDemo.Core;
using Microsoft.EntityFrameworkCore;

namespace AtomicTransactionDemo.Extension;

public static class StartupExtension
{
    public static IServiceCollection ConfigureCustomerDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CustomerDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("CustomersDatabase")));

        services.AddAndMigrateDatabases(configuration);
        services.AddScoped<ICustomerDbContext>(provider => provider.GetService<CustomerDbContext>());
        return services;
    }
    public static IServiceCollection AddAndMigrateDatabases(this IServiceCollection services, IConfiguration config)
    {
        var defaultConnectionString = config.GetConnectionString("CustomersDatabase");
        
        services.AddDbContext<CustomerDbContext>(m =>
        m.UseSqlServer(e => e.MigrationsAssembly(typeof(CustomerDbContext).Assembly.FullName)));
        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
        dbContext.Database.SetConnectionString(defaultConnectionString);
        if (dbContext.Database.GetMigrations().Count() > 0)
        {
            dbContext.Database.Migrate();
        }

        return services;
    }
}
