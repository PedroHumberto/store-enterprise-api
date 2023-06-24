using Catalog.API.Configuration;
using Catalog.API.Data;
using Catalog.API.Data.Repository;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();


//App builder

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.UseSwaggerConfiguration();

app.Run();
