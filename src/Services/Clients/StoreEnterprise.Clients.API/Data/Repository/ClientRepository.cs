using Core.APP.Data;
using Microsoft.EntityFrameworkCore;
using StoreEnterprise.Clients.API.Data;
using StoreEnterprise.Clients.API.Models;

namespace Namespace;
public class ClientRepository : IClientRepository
{
    private readonly ClientContext _context;

    public ClientRepository(ClientContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;
    public void AddClient(Client client)
    {
        _context.Clients.Add(client);
    }
    public async Task<IEnumerable<Client>> GetAllClients()
    {
        return await _context.Clients.AsNoTracking().ToListAsync();
    }

    public async Task<Client> GetFromCpf(string cpf)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }


}
