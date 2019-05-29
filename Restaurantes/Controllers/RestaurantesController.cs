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
    public class RestaurantesController : Controller
    {
        private readonly cruceroContext _context;

        public RestaurantesController(cruceroContext context)
        {
            _context = context;
        }

        // GET: Restaurantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Restaurante.ToListAsync());
        }

        // GET: Restaurantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return View(restaurante);
        }

        // GET: Restaurantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Horario,Principal,Capacidad")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurante.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Horario,Principal,Capacidad")] Restaurante restaurante)
        {
            if (id != restaurante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestauranteExists(restaurante.Id))
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
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return View(restaurante);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurante = await _context.Restaurante.FindAsync(id);
            _context.Restaurante.Remove(restaurante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestauranteExists(int id)
        {
            return _context.Restaurante.Any(e => e.Id == id);
        }
    }
}
