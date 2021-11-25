using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestaoDeVendas.Models.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Mapeamento
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(p => p.id);
            builder.Property(p => p.id).ValueGeneratedOnAdd();
            builder.Property(p => p.clienteID).IsRequired();
            builder.Property(p => p.dataVenda).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.total).HasColumnType("float").IsRequired();
            builder.Property(p => p.status).IsRequired();

            builder.HasMany(p => p.itemVendas).WithOne(p => p.venda).HasForeignKey(p => p.vendaID).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Venda");
        }
    }
}