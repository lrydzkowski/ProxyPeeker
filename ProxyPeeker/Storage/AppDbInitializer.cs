using Microsoft.EntityFrameworkCore;

namespace ProxyPeeker.Storage;

public static class AppDbInitializer
{
    public static void Init(IServiceProvider services)
    {
        using IServiceScope serviceScope = services.CreateScope();
        AppDbContext context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}
