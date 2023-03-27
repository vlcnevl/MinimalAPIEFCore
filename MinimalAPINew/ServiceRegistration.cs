using Microsoft.EntityFrameworkCore;

namespace MinimalAPINew
{
    public static class ServiceRegistration
    {

        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext<MinimalDbContext>(options => options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=MinimapApiDb;"));

        }

    }
}
