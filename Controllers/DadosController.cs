using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaGestaoDeVendas.Models;
using SistemaGestaoDeVendas.Models.Dominio;
using ExcelDataReader;
using System.Text;

namespace SistemaGestaoDeVendas.Controllers
{

    public class DadosController : Controller
    {
        private readonly Contexto _contexto;

        public DadosController(Contexto context)
        {
            _contexto = context;
        }

        public IActionResult gerarClientes()
        {
            Random randNum = new Random();

            string[] vNomeMas = { "Miguel", "Arthur", "Bernardo", "Heitor", "Davi", "Lorenzo", "Théo", "Pedro", "Gabriel", "Enzo", "Matheus", "Lucas", "Benjamin", "Nicolas", "Guilherme", "Rafael", "Joaquim", "Samuel", "Enzo Gabriel", "João Miguel", "Henrique", "Gustavo", "Murilo", "Pedro Henrique", "Pietro", "Lucca", "Felipe", "João Pedro", "Isaac", "Benício", "Daniel", "Anthony", "Leonardo", "Davi Lucca", "Bryan", "Eduardo", "João Lucas", "Victor", "João", "Cauã", "Antônio", "Vicente", "Caleb", "Gael", "Bento", "Caio", "Emanuel", "Vinícius", "João Guilherme", "Davi Lucas", "Noah", "João Gabriel", "João Victor", "Luiz Miguel", "Francisco", "Kaique", "Otávio", "Augusto", "Levi", "Yuri", "Enrico", "Thiago", "Ian", "Victor Hugo", "Thomas", "Henry", "Luiz Felipe", "Ryan", "Arthur Miguel", "Davi Luiz", "Nathan", "Pedro Lucas", "Davi Miguel", "Raul", "Pedro Miguel", "Luiz Henrique", "Luan", "Erick", "Martin", "Bruno", "Rodrigo", "Luiz Gustavo", "Arthur Gabriel", "Breno", "Kauê", "Enzo Miguel", "Fernando", "Arthur Henrique", "Luiz Otávio", "Carlos Eduardo", "Tomás", "Lucas Gabriel", "André", "José", "Yago", "Danilo", "Anthony Gabriel", "Ruan", "Miguel Henrique", "Oliver" };
            string[] vNomeFem = { "Alice", "Sophia", "Helena", "Valentina", "Laura", "Isabella", "Manuela", "Júlia", "Heloísa", "Luiza", "Maria Luiza", "Lorena", "Lívia", "Giovanna", "Maria Eduarda", "Beatriz", "Maria Clara", "Cecília", "Eloá", "Lara", "Maria Júlia", "Isadora", "Mariana", "Emanuelly", "Ana Júlia", "Ana Luiza", "Ana Clara", "Melissa", "Yasmin", "Maria Alice", "Isabelly", "Lavínia", "Esther", "Sarah", "Elisa", "Antonella", "Rafaela", "Maria Cecília", "Liz", "Marina", "Nicole", "Maitê", "Isis", "Alícia", "Luna", "Rebeca", "Agatha", "Letícia", "Maria-", "Gabriela", "Ana Laura", "Catarina", "Clara", "Ana Beatriz", "Vitória", "Olívia", "Maria Fernanda", "Emilly", "Maria Valentina", "Milena", "Maria Helena", "Bianca", "Larissa", "Mirella", "Maria Flor", "Allana", "Ana Sophia", "Clarice", "Pietra", "Maria Vitória", "Maya", "Laís", "Ayla", "Ana Lívia", "Eduarda", "Mariah", "Stella", "Ana", "Gabrielly", "Sophie", "Carolina", "Maria Laura", "Maria Heloísa", "Maria Sophia", "Fernanda", "Malu", "Analu", "Amanda", "Aurora", "Maria Isis", "Louise", "Heloise", "Ana Vitória", "Ana Cecília", "Ana Liz", "Joana", "Luana", "Antônia", "Isabel", "Bruna" };
            string[] vMunicipio = { "Assis", "Candido Mota", "Taruma", "Paraguaçu", "Palmital", "Pedrinhas", "Maracai", "Cruzalia" };
            string[] vDominio = { "uol", "hotmail", "dominio", "outlook", "gmail", "yahoo" };
            for (int i = 0; i < 50; i++)
            {
                Cliente cliente = new Cliente();

                cliente.nome = (i % 2 == 0) ? vNomeMas[i / 2] : vNomeFem[i / 2];
                cliente.cpf = randNum.Next(1, 999).ToString() + "." + randNum.Next(100, 999).ToString() + "." + randNum.Next(100, 999).ToString() + "-" + randNum.Next(1, 99);
                cliente.endereco = "Endereço " + randNum.Next(1, 6).ToString();    //(randNum.Next() % 6)+1; 
                cliente.bairro = "Bairro " + randNum.Next(1, 6).ToString();    //(randNum.Next() % 6)+1; 
                cliente.cidade = vMunicipio[randNum.Next() % 8];
                cliente.uf = "SP";
                cliente.telefone = "(18) " + randNum.Next(99000, 99999).ToString() + "-" + randNum.Next(1234, 9999).ToString(); 
                cliente.email = cliente.nome.ToLower() + "@" + vDominio[randNum.Next() % 6].ToLower() + ".com.br";
                

                _contexto.Clientes.Add(cliente);
            }
            _contexto.SaveChanges();

            return View(_contexto.Clientes.OrderBy(o => o.nome).ToList());
        }

        public IActionResult gerarProdutos()
        {
            Random randNum = new Random();
            Encoding encode = Encoding.GetEncoding("iso-8859-1");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var stream = System.IO.File.Open("Produtos.xlsx", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);

            string valDesc;
            int tam;

            reader.Read(); //ler a primeira linha e ignora pois é o cabeçalho 

            do
            {
                while (reader.Read())
                {
                    Produto produto = new Produto();

                    valDesc = reader[0].ToString();  /// reader[0] significa a coluna A
                    tam = valDesc.Length;

                    try
                    {
                        if (tam > 0)
                            if (tam < 35)
                                produto.nome = valDesc;
                            else produto.nome = valDesc.Substring(0, 35);
                        else produto.nome = "nulo";
                    }
                    catch
                    {

                        produto.nome = "Campo com erro";
                    }


                    if (reader[1].ToString().ToLower().Equals("calca") == true)  // reader[1] significa coluna B
                        produto.categoria = Produto.Categoria.Calça; 
                    else if (reader[1].ToString().ToLower().Equals("camisa") == true)
                        produto.categoria = Produto.Categoria.Camisa;
                    else if (reader[1].ToString().ToLower().Equals("camiseta") == true)
                        produto.categoria = Produto.Categoria.Camiseta;
                    else if (reader[1].ToString().ToLower().Equals("moletom") == true)
                        produto.categoria = Produto.Categoria.Moletom;
                    else if (reader[1].ToString().ToLower().Equals("bone") == true)
                        produto.categoria = Produto.Categoria.Boné;
                    else if (reader[1].ToString().ToLower().Equals("calcado") == true)
                        produto.categoria = Produto.Categoria.Calçado;
                    else if (reader[1].ToString().ToLower().Equals("body") == true)
                        produto.categoria = Produto.Categoria.Body;
                    else if (reader[1].ToString().ToLower().Equals("chapeu") == true)
                        produto.categoria = Produto.Categoria.Chapéu;
                    else produto.categoria = Produto.Categoria.Outros; 


                    produto.tamanho = reader[2].ToString();

                    produto.quantidade = randNum.Next(0, 1000);

                    try
                    {
                        produto.preco = Convert.ToSingle(reader[3]); 
                    }
                    catch
                    {
                        produto.preco = 100;
                    }
                    _contexto.Produtos.Add(produto);

                }
                _contexto.SaveChanges();

            } while (reader.NextResult());

            return View(_contexto.Produtos.ToList());
        }


        public IActionResult gerarVendas()
        {
            Random randNUm = new Random();
            int prod;

            for (int i = 0; i < 100; i++)
            {
                Venda venda = new Venda();
                prod = randNUm.Next(1, 50);
                venda.clienteID = prod;

                DateTime data = Convert.ToDateTime("01/01/2015");
                venda.dataVenda = data.AddDays(randNUm.Next(1,2450));

                venda.status = Venda.Status.Aberta;

                venda.total = (float)0.0;

                _contexto.Vendas.Add(venda);
            }

            _contexto.SaveChanges();
            return View();
        }

        public IActionResult atualizarVendas()
        {
            IEnumerable<ItemVenda> itemVendas = _contexto.ItensVendas.ToList();
   
            for (int i = 0; i < 100; i++)
            {
                foreach (var item in itemVendas)
                {
                    if (item.vendaID == i)
                    {
                        Venda venda = _contexto.Vendas.Find(i);
                        venda.total += (float)item.subTotal;
                    }
                }
            }

            _contexto.SaveChanges();
            return View();
        }

        public IActionResult GerarItemVendas()
        {
            Random randNum = new Random();

            for (int i = 0; i < 100; i++)
            {
                ItemVenda itemVenda = new ItemVenda();

                itemVenda.vendaID = randNum.Next(1, 100);
                itemVenda.venda = _contexto.Vendas.Find(itemVenda.vendaID);

                itemVenda.produtoID = randNum.Next(1, 30);
                Produto produto = _contexto.Produtos.Find(itemVenda.produtoID);
                itemVenda.produto = produto;

                Venda venda = _contexto.Vendas.Find(itemVenda.vendaID);
                venda.status = Venda.Status.Finalizada;

                itemVenda.quantidade = randNum.Next(1, 3);
                itemVenda.preco = produto.preco;
                _contexto.ItensVendas.Add(itemVenda);
                
            }
            _contexto.SaveChanges();
            return View();
        }

    }
}