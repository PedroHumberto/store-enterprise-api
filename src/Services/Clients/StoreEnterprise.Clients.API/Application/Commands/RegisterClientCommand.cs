using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.APP.DomainObjects;
using Core.APP.Messages;

namespace StoreEnterprise.Clients.API.Application.Commands
{
    //realiza o transporte dos dados do cliente para o banco, aqui define quais dados irei levar
    public class RegisterClientCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterClientCommand(Guid id, string name, string email, string cpf)
        {
            //Para popular a informação do aggregate
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }
    }
}