using Core.APP.Messages;
using FluentValidation.Results;
using MediatR;
using StoreEnterprise.Clients.API.Models;

namespace StoreEnterprise.Clients.API.Application.Commands
{
    
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
    {

        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            //validar comando
            if (!message.IsValid()) return message.ValidationResult;

            //validações de negocio

            var client = new Client(message.Id, message.Name, message.Email, message.Cpf);

            
            //presiste na base

            /*
            if (true)//se já existe cliente
            {
                AddError("This CPF already exist");
                return ValidationResult;
            }*/
            
            return message.ValidationResult;

        }

    }
}