using Microsoft.EntityFrameworkCore;
using StoreEnterprise.Clients.API.Data;
using StoreEnterprise.WebAPI.CORE.Identity;


namespace StoreEnterprise.Clients.API.Configuration;
public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnectionString");
        services.AddDbContext<ClientContext>(opts => opts.UseSqlServer(connectionString));

        // Add services to the container.
        services.AddControllers();


        //API vai ser acessada por outros endpoints: CORS
        services.AddCors(opts =>
        {
            opts.AddPolicy("Total", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

    }
    public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");

        app.UseAuthConfiguration();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }

}
