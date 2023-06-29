using Microsoft.Extensions.Configuration;
using StoreEnterprise.WebAPI.CORE.Identity;
using StoreEnterpriseApp.Identity.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Environment configuration 
IWebHostEnvironment environment = builder.Environment;
builder.Configuration.SetBasePath(environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsetings.{environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

if (environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddApiConfiguration();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);


app.Run();
