using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortariaInteligente.Data;
using PortariaInteligente.Models;

namespace PortariaInteligente.Controllers
{
    public class VisitadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visitados
        public async Task<IActionResult> Index(string nome, string email)
        {
            var visitados = from m in _context.Visitados select m;

            if (!String.IsNullOrEmpty(nome))
            {
                visitados = visitados.Where(s => s.VisitadoNome.Contains(nome));
            }
             if (!String.IsNullOrEmpty(email))
            {
                visitados = visitados.Where(s => s.VisitadoEmail.Contains(email));
            }
            return View(await visitados.ToListAsync());
        }

        // GET: Visitados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitado = await _context.Visitados
                .FirstOrDefaultAsync(m => m.VisitadoID == id);
            if (visitado == null)
            {
                return NotFound();
            }

            return View(visitado);
        }

        // GET: Visitados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visitados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitadoID,VisitadoNome,VisitadoEmail,VisitadoCel")] Visitado visitado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visitado);
        }

        // GET: Visitados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitado = await _context.Visitados.FindAsync(id);
            if (visitado == null)
            {
                return NotFound();
            }
            return View(visitado);
        }

        // POST: Visitados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitadoID,VisitadoNome,VisitadoEmail,VisitadoCel")] Visitado visitado)
        {
            if (id != visitado.VisitadoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitadoExists(visitado.VisitadoID))
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
            return View(visitado);
        }

        // GET: Visitados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitado = await _context.Visitados
                .FirstOrDefaultAsync(m => m.VisitadoID == id);
            if (visitado == null)
            {
                return NotFound();
            }

            return View(visitado);
        }

        // POST: Visitados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitado = await _context.Visitados.FindAsync(id);
            _context.Visitados.Remove(visitado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitadoExists(int id)
        {
            return _context.Visitados.Any(e => e.VisitadoID == id);
        }
    }
}
