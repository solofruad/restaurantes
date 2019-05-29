using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurantes.Models;

namespace Restaurantes.Controllers
{
    public class PlatosController : Controller
    {
        private readonly cruceroContext _context;

        public PlatosController(cruceroContext context)
        {
            _context = context;
        }

        // GET: Platos
        public async Task<IActionResult> Index()
        {
            var cruceroContext = _context.Plato.Include(p => p.CategoriaNavigation).Include(p => p.MenuNavigation);
            return View(await cruceroContext.ToListAsync());
        }

        // GET: Platos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato
                .Include(p => p.CategoriaNavigation)
                .Include(p => p.MenuNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: Platos/Create
        public IActionResult Create()
        {
            ViewData["Categoria"] = new SelectList(_context.Categoria, "Id", "Nombre");
            ViewData["Menu"] = new SelectList(_context.Menu, "Id", "Nombre");
            return View();
        }

        // POST: Platos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Menu,Categoria")] Plato plato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoria"] = new SelectList(_context.Categoria, "Id", "Nombre", plato.Categoria);
            ViewData["Menu"] = new SelectList(_context.Menu, "Id", "Nombre", plato.Menu);
            return View(plato);
        }

        // GET: Platos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            ViewData["Categoria"] = new SelectList(_context.Categoria, "Id", "Nombre", plato.Categoria);
            ViewData["Menu"] = new SelectList(_context.Menu, "Id", "Nombre", plato.Menu);
            return View(plato);
        }

        // POST: Platos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Menu,Categoria")] Plato plato)
        {
            if (id != plato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoExists(plato.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoria"] = new SelectList(_context.Categoria, "Id", "Nombre", plato.Categoria);
            ViewData["Menu"] = new SelectList(_context.Menu, "Id", "Nombre", plato.Menu);
            return View(plato);
        }

        // GET: Platos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato
                .Include(p => p.CategoriaNavigation)
                .Include(p => p.MenuNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // POST: Platos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plato = await _context.Plato.FindAsync(id);
            _context.Plato.Remove(plato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatoExists(int id)
        {
            return _context.Plato.Any(e => e.Id == id);
        }
    }
}
