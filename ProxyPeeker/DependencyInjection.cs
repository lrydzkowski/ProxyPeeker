using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProxyPeeker.Options;
using ProxyPeeker.Storage;

namespace ProxyPeeker;

public static class DependencyInjection
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.ConfigureOptions(configuration);
        services.ConfigureAppDbContext();
    }

    private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ConnectionStringsOptions>()
            .Bind(configuration.GetSection(ConnectionStringsOptions.Position));
        services.AddOptions<ProxyOptions>().Bind(configuration.GetSection(ProxyOptions.Position));
    }

    private static void ConfigureAppDbContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            (serviceProvider, options) =>
            {
                ConnectionStringsOptions connectionStrings =
                    serviceProvider.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
                options.UseSqlServer(connectionStrings.SqlServerDb);
            }
        );
    }
}
