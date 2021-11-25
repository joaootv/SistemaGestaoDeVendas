using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestaoDeVendas.Models.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Mapeamento
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(p => p.id);
            builder.Property(p => p.id).ValueGeneratedOnAdd();
            builder.Property(p => p.nome).HasMaxLength(35).IsRequired();
            builder.Property(p => p.cpf).HasMaxLength(14).IsRequired();
            builder.HasIndex(p => p.cpf).IsUnique();
            builder.Property(p => p.endereco).HasMaxLength(50).IsRequired();
            builder.Property(p => p.bairro).HasMaxLength(25).IsRequired();
            builder.Property(p => p.cidade).HasMaxLength(25).IsRequired();
            builder.Property(p => p.uf).HasMaxLength(2).IsRequired();
            builder.Property(p => p.telefone).HasMaxLength(15).IsRequired();
            builder.Property(p => p.email).HasMaxLength(35);

            builder.HasMany(p => p.vendas).WithOne(p => p.cliente).HasForeignKey(p => p.clienteID).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Cliente");
        }
    }
}