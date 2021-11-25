using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestaoDeVendas.Models;
using SistemaGestaoDeVendas.Models.Dominio;

namespace SistemaGestaoDeVendas.Controllers
{
    public class ItemVendasController : Controller
    {
        private readonly Contexto _context;

        public ItemVendasController(Contexto context)
        {
            _context = context;
        }

        // GET: ItemVendas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.ItensVendas.Include(i => i.produto).Include(i => i.venda);
            return View(await contexto.ToListAsync());
        }

        // GET: ItemVendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemVenda = await _context.ItensVendas
                .Include(i => i.produto)
                .Include(i => i.venda)
                .FirstOrDefaultAsync(m => m.id == id);
            if (itemVenda == null)
            {
                return NotFound();
            }

            return View(itemVenda);
        }

        // GET: ItemVendas/AddProdutos
        public IActionResult AddProdutos()
        {
            
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "nomeTam");
            
            ViewData["vendaID"] = new SelectList(_context.Vendas, "id", "id");

            return View();
        }

        // POST: ItemVendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProdutos([Bind("id,quantidade,preco,produtoID,vendaID")] ItemVenda itemVenda)
        {
            Produto produto = _context.Produtos.Find(itemVenda.produtoID);
            Venda venda = _context.Vendas.Find(itemVenda.vendaID);

            itemVenda.preco = produto.preco;

            if (venda.status.ToString().ToLower().Equals("aberta") == true)
            {
                if (itemVenda.quantidade <= produto.quantidade)
                {
                    produto.quantidade -= itemVenda.quantidade;
                    
                    
                    venda.total += (float) itemVenda.subTotal;
                    

                    if (ModelState.IsValid)
                    {
                        _context.Add(itemVenda);
                        await _context.SaveChangesAsync();
                        return RedirectToRoute(new { controller = "Vendas", action = "Details", id = itemVenda.vendaID });
                    }
                }
            }
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "nomeTam", itemVenda.produtoID);
            ViewData["vendaID"] = new SelectList(_context.Vendas, "id", "id", itemVenda.vendaID);
            return View(itemVenda);
        }

        // GET: ItemVendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemVenda = await _context.ItensVendas
                .Include(i => i.produto)
                .Include(i => i.venda)
                .FirstOrDefaultAsync(m => m.id == id);
            if (itemVenda == null)
            {
                return NotFound();
            }

            return View(itemVenda);
        }

        // POST: ItemVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            var itemVenda = await _context.ItensVendas.FindAsync(id);
            Venda venda = _context.Vendas.Find(itemVenda.vendaID);

            if (venda.status.ToString().ToLower().Equals("aberta") == true)
            {
                Produto produto = _context.Produtos.Find(itemVenda.produtoID);
                produto.quantidade += itemVenda.quantidade;

                venda.total -= (float)itemVenda.subTotal;

                _context.ItensVendas.Remove(itemVenda);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Vendas", action = "Details", id = itemVenda.vendaID });
            } else
            {
                return null;
            }
        }

        private bool ItemVendaExists(int id)
        {
            return _context.ItensVendas.Any(e => e.id == id);
        }
    }
}