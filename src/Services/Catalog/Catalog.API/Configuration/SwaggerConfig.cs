using Microsoft.OpenApi.Models;

namespace Catalog.API.Configuration
{
    public static class SwaggerConfig
    {

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Store Enterprise",
                    Description = "Esta API foi feita para estudo de arquitetura de software, ensinada no curso ASP.NET Core Enterprise Apllications do Desenvolvedor.io",
                    Contact = new OpenApiContact() { Name = "Pedro Cardoso", Email = "pedrohumbertogc@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/cecill-2-1/") }
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
