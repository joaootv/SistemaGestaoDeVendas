using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestaoDeVendas.Models.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Mapeamento
{
    public class ItemVendaMap : IEntityTypeConfiguration<ItemVenda>
    {
        public void Configure(EntityTypeBuilder<ItemVenda> builder)
        {
            builder.HasKey(p => p.id);
            builder.Property(p => p.id).ValueGeneratedOnAdd();
            builder.Property(p => p.quantidade).HasColumnType("Int");
            builder.Property(p => p.preco).HasColumnType("float");
            builder.Property(p => p.produtoID).IsRequired();
            builder.Property(p => p.vendaID).IsRequired();

            builder.ToTable("ItemVenda");
        }
    }
}