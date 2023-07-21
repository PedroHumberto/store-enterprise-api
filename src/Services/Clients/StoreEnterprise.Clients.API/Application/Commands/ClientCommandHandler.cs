using Core.APP.Messages;
using FluentValidation.Results;
using MediatR;
using StoreEnterprise.Clients.API.Models;

namespace StoreEnterprise.Clients.API.Application.Commands
{
    
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
    {
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            //validar comando
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Client(message.Id, message.Name, message.Email, message.Cpf);
            //validações de negocio
            var clientExist = await _clientRepository.GetFromCpf(client.Cpf.Number);
            
            if (clientExist != null)//se já existe cliente
            {
                AddError("This CPF is already in use");
                return ValidationResult;
            }

            //persiste na base        
            _clientRepository.AddClient(client);
    
            return await PersistData(_clientRepository.UnitOfWork);
        }

    }
}