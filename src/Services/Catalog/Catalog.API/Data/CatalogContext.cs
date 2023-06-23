using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base (options)
        {
            
        }

        DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Evitar que crie um campo com varchar(max) caso deixe passar algum campo
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                    e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");


            //Mapeia a entidade de mapeamento
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        } 
    }
}
