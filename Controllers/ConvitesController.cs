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
    public class ConvitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConvitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Convites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Convites.Include(c => c.Reunioes).Include(c => c.Visitantes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Convites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convite = await _context.Convites
                .Include(c => c.Reunioes)
                .Include(c => c.Visitantes)
                .FirstOrDefaultAsync(m => m.ReuniaoID == id);
            if (convite == null)
            {
                return NotFound();
            }

            return View(convite);
        }

        // GET: Convites/Create
        public IActionResult Create()
        {
            ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome");
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "DocumentoNumero");
            return View();
        }

        // POST: Convites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitanteID,ReuniaoID")] Convite convite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(convite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome", convite.ReuniaoID);
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "DocumentoNumero", convite.VisitanteID);
            return View(convite);
        }

        // GET: Convites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convite = await _context.Convites.FindAsync(id);
            if (convite == null)
            {
                return NotFound();
            }
            ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome", convite.ReuniaoID);
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "DocumentoNumero", convite.VisitanteID);
            return View(convite);
        }

        // POST: Convites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisitanteID,ReuniaoID")] Convite convite)
        {
            if (id != convite.ReuniaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(convite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConviteExists(convite.ReuniaoID))
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
            ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome", convite.ReuniaoID);
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "DocumentoNumero", convite.VisitanteID);
            return View(convite);
        }

        // GET: Convites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convite = await _context.Convites
                .Include(c => c.Reunioes)
                .Include(c => c.Visitantes)
                .FirstOrDefaultAsync(m => m.ReuniaoID == id);
            if (convite == null)
            {
                return NotFound();
            }

            return View(convite);
        }

        // POST: Convites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var convite = await _context.Convites.FindAsync(id);
            _context.Convites.Remove(convite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConviteExists(int id)
        {
            return _context.Convites.Any(e => e.ReuniaoID == id);
        }
    }
}
