
using Core.APP.Data;
using Microsoft.EntityFrameworkCore;
using StoreEnterprise.Clients.API.Models;

namespace StoreEnterprise.Clients.API.Data
{
    public class ClientContext : DbContext, IUnitOfWork
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses{ get; set; }


         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Evitar que crie um campo com varchar(max) caso deixe passar algum campo
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                    e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            //onde ouver relacionamento, desliga o cascade behaviour.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;


            //Mapeia a entidade de mapeamento
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
        }


        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}