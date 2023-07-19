using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.APP.Data;

namespace StoreEnterprise.Clients.API.Models
{
    public interface IClientRepository : IRepository<Client>
    {
        void AddClient(Client client);
        Task<IEnumerable<Client>> GetAllClients();

        Task<Client> GetFromCpf(string cpf);
    }
}