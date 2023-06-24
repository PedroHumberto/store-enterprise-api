using Catalog.API.Models;
using Core.APP.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base (options)
        {
            
        }

        public DbSet<Product> Products { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Evitar que crie um campo com varchar(max) caso deixe passar algum campo
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                    e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");


            //Mapeia a entidade de mapeamento
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            //Se o commit deu certo ele vai retornar 1, não preciso ficar dando commit nos dados direto no repositorio
            return await base.SaveChangesAsync() > 0;
        }
    }
}
