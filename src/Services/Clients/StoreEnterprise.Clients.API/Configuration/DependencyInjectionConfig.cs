using Core.APP.Mediator;
using FluentValidation.Results;
using MediatR;
using StoreEnterprise.Clients.API.Application.Commands;
using StoreEnterprise.Clients.API.Data;

namespace StoreEnterprise.Clients.API.Configuration;
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        //define RegisterClientCommand entregue via IRequestHandler que retornara ValidationResult sera manipulado pelo ClientCommandHandler
        services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>,ClientCommandHandler>(); 
        services.AddScoped<ClientContext>();
    }

}
