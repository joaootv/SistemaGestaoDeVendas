using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaGestaoDeVendas.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    endereco = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bairro = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    cidade = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    uf = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    categoria = table.Column<int>(type: "int", nullable: false),
                    tamanho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    preco = table.Column<double>(type: "float", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clienteID = table.Column<int>(type: "int", nullable: false),
                    dataVenda = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.id);
                    table.ForeignKey(
                        name: "FK_Venda_Cliente_clienteID",
                        column: x => x.clienteID,
                        principalTable: "Cliente",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ItemVenda",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantidade = table.Column<int>(type: "Int", nullable: false),
                    preco = table.Column<double>(type: "float", nullable: false),
                    produtoID = table.Column<int>(type: "int", nullable: false),
                    vendaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemVenda", x => x.id);
                    table.ForeignKey(
                        name: "FK_ItemVenda_Produto_produtoID",
                        column: x => x.produtoID,
                        principalTable: "Produto",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ItemVenda_Venda_vendaID",
                        column: x => x.vendaID,
                        principalTable: "Venda",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_cpf",
                table: "Cliente",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemVenda_produtoID",
                table: "ItemVenda",
                column: "produtoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemVenda_vendaID",
                table: "ItemVenda",
                column: "vendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_clienteID",
                table: "Venda",
                column: "clienteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemVenda");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
