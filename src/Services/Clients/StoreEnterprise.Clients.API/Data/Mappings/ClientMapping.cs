using System.Formats.Tar;
using Core.APP.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreEnterprise.Clients.API.Models;


namespace StoreEnterprise.Clients.API.Data.Mappings;
public class ClientMapping : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        //o cpf pertence ao cliente, o cliente possui o CPF
        builder.OwnsOne(c => c.Cpf, tf =>
        {
            tf.Property(c => c.Number)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLen)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLen})");
        });

        builder.OwnsOne(c => c.Email, tf =>
        {
            tf.Property(e => e.EmailAddress)
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.EmailMaxLen})");                
        });

        // 1 : 1 => Client : Address
        builder.HasOne(c => c.Address)
            .WithOne(a => a.Client);

        builder.ToTable("Clients");


    }
}
