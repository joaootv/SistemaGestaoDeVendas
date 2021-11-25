using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestaoDeVendas.Models.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Mapeamento
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.id);
            builder.Property(p => p.id).ValueGeneratedOnAdd();
            builder.Property(p => p.nome);
            builder.Property(p => p.categoria).IsRequired(); ;
            builder.Property(p => p.tamanho).IsRequired(); ;
            builder.Property(p => p.preco).HasColumnType("float").IsRequired();
            builder.Property(p => p.quantidade).HasColumnType("int").IsRequired();


            builder.HasMany(p => p.itemVendas).WithOne(p => p.produto).HasForeignKey(p => p.produtoID).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Produto");
        }
    }
}