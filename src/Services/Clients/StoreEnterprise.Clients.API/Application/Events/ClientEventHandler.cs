using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace StoreEnterprise.Clients.API.Application.Events
{
    public class ClientEventHandler : INotificationHandler<ClientRegisteredEvent>
    {
        public Task Handle(ClientRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}