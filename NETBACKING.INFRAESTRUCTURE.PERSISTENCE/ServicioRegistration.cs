using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE;

public static class ServicioRegistration
{
    public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("IdentityDb");
            });
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    mbox => mbox.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });
        }
    }
}