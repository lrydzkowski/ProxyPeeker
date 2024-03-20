using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using ProxyPeeker.Options;

namespace ProxyPeeker.Storage;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AppDbContext> builder = GetDbContextOptionsBuilder();

        return new AppDbContext(builder.Options);
    }

    private DbContextOptionsBuilder<AppDbContext> GetDbContextOptionsBuilder()
    {
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<AppDbContext>().Build();
        IConfigurationProvider secretProvider = config.Providers.First();
        DbContextOptionsBuilder<AppDbContext> builder = new();

        string? connectionString = GetConnectionString(
            secretProvider,
            nameof(ConnectionStringsOptions.SqlServerDb)
        );
        if (IsConnectionStringCorrect(connectionString))
        {
            builder.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            );

            return builder;
        }

        throw new Exception(
            "There is no connection string in user secrets."
        );
    }

    private string? GetConnectionString(IConfigurationProvider secretProvider, string optionName)
    {
        secretProvider.TryGet(
            $"{ConnectionStringsOptions.Position}:{optionName}",
            out string? connectionString
        );

        return connectionString;
    }

    private bool IsConnectionStringCorrect(string? connectionString)
    {
        return !string.IsNullOrEmpty(connectionString);
    }
}
