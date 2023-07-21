using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.APP.Messages;

namespace StoreEnterprise.Clients.API.Application.Events
{
    public class ClientRegisteredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public ClientRegisteredEvent(Guid id, string name, string email, string cpf)
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