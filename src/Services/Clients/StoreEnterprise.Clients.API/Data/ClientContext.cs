
using Core.APP.Data;
using Core.APP.DomainObjects;
using Core.APP.Mediator;
using Microsoft.EntityFrameworkCore;
using StoreEnterprise.Clients.API.Models;

namespace StoreEnterprise.Clients.API.Data
{
    public class ClientContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ClientContext(DbContextOptions<ClientContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }


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
            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublishEvents(this);

            return success;
        }


    }
    public static class MediatorExtension
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            //Na memoria das entidades, busca as entidades, e dentro da entidade vai verificar se existe uma notificação.
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

            //Fazer um select many para todos os eventos e transformar em uma lista.
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Events) //Select de todos os eventos que está na memoria do contexto
                .ToList();

            //Foreach para zerar os eventos, pois já tenho em memoria
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.CleanEvents());
            
            //cria uma task para cada evento
            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);

        }
    }
}