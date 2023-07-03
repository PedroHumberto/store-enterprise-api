using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreEnterprise.WebAPI.CORE.Identity;
using StoreEnterpriseApp.Identity.API.Data;
using StoreEnterpriseApp.Identity.API.Services;

namespace StoreEnterpriseApp.Identity.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ITokenService,TokenService>();

            services.AddJwtConfiguration(configuration);

            return services;
        }
    }
}
