﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaGestaoDeVendas.Models;

namespace SistemaGestaoDeVendas.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20211125011939_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.Cliente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("bairro")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("cidade")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("email")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("endereco")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("uf")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("id");

                    b.HasIndex("cpf")
                        .IsUnique();

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.ItemVenda", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("preco")
                        .HasColumnType("float");

                    b.Property<int>("produtoID")
                        .HasColumnType("int");

                    b.Property<int>("quantidade")
                        .HasColumnType("Int");

                    b.Property<int>("vendaID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("produtoID");

                    b.HasIndex("vendaID");

                    b.ToTable("ItemVenda");
                });

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.Produto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("categoria")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<double>("preco")
                        .HasColumnType("float");

                    b.Property<int>("quantidade")
                        .HasColumnType("int");

                    b.Property<string>("tamanho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.Venda", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("clienteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("dataVenda")
                        .HasColumnType("datetime");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<double>("total")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("clienteID");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.ItemVenda", b =>
                {
                    b.HasOne("SistemaGestaoDeVendas.Models.Dominio.Produto", "produto")
                        .WithMany("itemVendas")
                        .HasForeignKey("produtoID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SistemaGestaoDeVendas.Models.Dominio.Venda", "venda")
                        .WithMany("itemVendas")
                        .HasForeignKey("vendaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("produto");

                    b.Navigation("venda");
                });

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.Venda", b =>
                {
                    b.HasOne("SistemaGestaoDeVendas.Models.Dominio.Cliente", "cliente")
                        .WithMany("vendas")
                        .HasForeignKey("clienteID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("cliente");
                });

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.Cliente", b =>
                {
                    b.Navigation("vendas");
                });

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.Produto", b =>
                {
                    b.Navigation("itemVendas");
                });

            modelBuilder.Entity("SistemaGestaoDeVendas.Models.Dominio.Venda", b =>
                {
                    b.Navigation("itemVendas");
                });
#pragma warning restore 612, 618
        }
    }
}