using Microsoft.EntityFrameworkCore;
using SistemaGestaoDeVendas.Models.Dominio;
using SistemaGestaoDeVendas.Models.Mapeamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SistemaGestaoDeVendas.Models
{
    public class Contexto:DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ItemVenda> ItensVendas { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public object ItemVenda { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClienteMap());
            builder.ApplyConfiguration(new ProdutoMap());
            builder.ApplyConfiguration(new VendaMap());
            builder.ApplyConfiguration(new ItemVendaMap());
            
            
        }
    }
}
