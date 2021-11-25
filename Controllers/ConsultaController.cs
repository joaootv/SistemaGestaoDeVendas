using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestaoDeVendas.Models.Dominio;
using SistemaGestaoDeVendas.Models.Consulta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SistemaGestaoDeVendas.Models.Consulta;
using SistemaGestaoDeVendas.Models;
using SistemaGestaoDeVendas.Extra;

namespace SistemaGestaoDeVendas.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly Contexto _context;

        public ConsultaController(Contexto context)
        {
            _context = context;
        }

        public IActionResult PivotVendas()
        {
            IEnumerable<VendaGrp> lstItemByVenda = from item in _context.Vendas
                                                  .ToList()
                                                   group item by new { item.dataVenda.Year, item.dataVenda.Month}
                                                  into grupo
                                                   orderby grupo.Key.Year, grupo.Key.Month
                                                   select new VendaGrp
                                                   {
                                                       ano = grupo.Key.Year,
                                                       mes = grupo.Key.Month,
                                                       total = (float)grupo.Count()
                                                   };
            var pivotTableVenda = lstItemByVenda.ToList().ToPivotTable(
                                                            item => item.mes,
                                                            item => item.ano,
                                                            items => items.Any() ? items.Sum(x=>x.total) : 0
                                                          );
            List<PivotVendas> lista = new List<PivotVendas>();
            lista = (from DataRow linha in pivotTableVenda.Rows
                     select new PivotVendas()
                     {
                         ano = Convert.ToInt32(linha[0]),
                         mes1 = Convert.ToInt32(linha[1]),
                         mes2 = Convert.ToInt32(linha[2]),
                         mes3 = Convert.ToInt32(linha[3]),
                         mes4 = Convert.ToInt32(linha[4]),
                         mes5 = Convert.ToInt32(linha[5]),
                         mes6 = Convert.ToInt32(linha[6]),
                         mes7 = Convert.ToInt32(linha[7]),
                         mes8 = Convert.ToInt32(linha[8]),
                         mes9 = Convert.ToInt32(linha[9]),
                         mes10 = Convert.ToInt32(linha[10]),
                         mes11 = Convert.ToInt32(linha[11]),
                         mes12 = Convert.ToInt32(linha[12])
                     }).ToList();
            return View(lista);

        }

        [HttpGet("/Consulta/AgruparItemVendas")]
        public IActionResult AgruparVendas()
        {
            IEnumerable<VendaGrp> lstItemByVenda = from item in _context.ItensVendas
                                                   .Include(v => v.venda)
                                                   .Include(c => c.venda.cliente)
                                                   .ToList()
                                                   group item by new {item.venda.cliente.nome, item.venda.clienteID}
                                                   into grupo
                                                   orderby grupo.Key.nome, grupo.Key.clienteID
                                                   select new VendaGrp
                                                   {
                                                       cliente = grupo.Key.nome,
                                                       qtdeProduto = grupo.Count(),
                                                       total = (float) grupo.Sum(p => p.quantidade * p.preco)
                                                   };
            return View(lstItemByVenda);
        }   

        [HttpGet("/Consulta/ListarItensVendidoCliente/{cli_id}")]
        public IActionResult ListarItensVendidoCliente(int cli_id)
        {
            IEnumerable<ItensVendidos> lstItens = from item in _context.ItensVendas
                                                  .Include(v => v.venda)
                                                  .Include(c => c.venda.cliente)
                                                  .Include(p => p.produto)
                                                  .Where(c => c.venda.clienteID == cli_id)
                                                  .OrderBy(cli => cli.venda.cliente.nome)
                                                  .ThenBy(idv => idv.venda)
                                                  .ThenByDescending(dt => dt.venda.dataVenda)
                                                  .ToList()
                                                  select new ItensVendidos {
                                                      id = item.id,
                                                      cliente = item.venda.cliente.nome,
                                                      venda = item.venda.id,
                                                      data = item.venda.dataVenda,
                                                      itemVendas = item.produto.nomeTam,
                                                      quantidade = item.quantidade,
                                                      valor = (float) item.preco,
                                                      total = (float) item.subTotal
                                                  };
            return View(lstItens);
        }
    }
}
