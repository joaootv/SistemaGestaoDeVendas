using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestaoDeVendas.Models;
using SistemaGestaoDeVendas.Models.Dominio;

namespace SistemaGestaoDeVendas.Controllers
{
    [Authorize]
    public class VendasController : Controller
    {
        private readonly Contexto _context;

        public VendasController(Contexto context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Vendas.Include(v => v.cliente);
            return View(await contexto.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .Include(v => v.cliente)
                .FirstOrDefaultAsync(m => m.id == id);
            if (venda == null)
            {
                return NotFound();
            }

            var contexto = _context.ItensVendas.Include(i => i.produto).Include(i => i.venda);

            var model = new ViewModel();
            model.ItemVenda = contexto;
            model.Venda = venda;

            return View(model); 
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {

            var status = Enum.GetValues(typeof(Status))
                           .Cast<Status>()
                           .Select(e => new SelectListItem
                           {
                               Value = e.ToString(),
                               Text = e.ToString()
                           });

            ViewBag.status = status;
            ViewData["clienteID"] = new SelectList(_context.Clientes, "id", "nome");

            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,clienteID,dataVenda,total,status")] Venda venda)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Vendas", action = "Details", id = venda.id });
            }
            ViewData["clienteID"] = new SelectList(_context.Clientes, "id", "nome", venda.clienteID);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            var status = Enum.GetValues(typeof(Status))
                          .Cast<Status>()
                          .Select(e => new SelectListItem
                          {
                              Value = e.ToString(),
                              Text = e.ToString()
                          });

            ViewBag.status = status;
            ViewData["clienteID"] = new SelectList(_context.Clientes, "id", "nome", venda.clienteID);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,clienteID,dataVenda,total,status")] Venda venda)
        {
            IEnumerable<ItemVenda> itemVendas = _context.ItensVendas.ToList();

            foreach (var item in itemVendas)
            {
                if (item.vendaID == venda.id)
                {
                   venda.total += (float) item.subTotal;
                }
            }
                     
            if (id != venda.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToRoute(new { controller = "Vendas", action = "Details", id = venda.id });
            }
            ViewData["clienteID"] = new SelectList(_context.Clientes, "id", "nome", venda.clienteID);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .Include(v => v.cliente)
                .FirstOrDefaultAsync(m => m.id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var venda = await _context.Vendas.FindAsync(id);
            if (venda.status.ToString().ToLower().Equals("aberta") == true)
            {
                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
            }
             return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.id == id);
        }
    }
}