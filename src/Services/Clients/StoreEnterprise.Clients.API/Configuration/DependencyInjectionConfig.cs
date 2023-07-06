using StoreEnterprise.Clients.API.Data;

namespace StoreEnterprise.Clients.API.Configuration;
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ClientContext>();
    }

}
