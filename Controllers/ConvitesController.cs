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
        public async Task<IActionResult> Details(string id)
        {
            var chaves = id.Split("_");
            var visitanteID = int.Parse(chaves[0]);
            var reuniaoID = int.Parse(chaves[1]);

            if (id == null)
            {
                return NotFound();
            }

            var convite = await _context.Convites
                .Include(c => c.Reunioes)
                .Include(c => c.Visitantes)
                .FirstOrDefaultAsync(m => m.ReuniaoID == reuniaoID && m.VisitanteID == visitanteID);

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
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "VisitanteNome");
            return View();
        }

        // POST: Convites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitanteID,ReuniaoID")] Convite convite)
        {
            if (ConviteExists(convite.VisitanteID +"_"+convite.ReuniaoID))
            {
                ModelState.AddModelError("VisitanteID", "Já existe um convite para este visitante");
            }

            else if (ModelState.IsValid)
            {
                _context.Add(convite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome", convite.ReuniaoID);
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "VisitanteNome", convite.VisitanteID);
            return View(convite);
        }

        // GET: Convites/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaves = id.Split("_");
            var visitanteID = int.Parse(chaves[0]);
            var reuniaoID = int.Parse(chaves[1]);

            var convite = await _context.Convites.FindAsync(reuniaoID, visitanteID);
            if (convite == null)
            {
                return NotFound();
            }
            ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome", convite.ReuniaoID);
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "VisitanteNome", convite.VisitanteID);
            return View(convite);
        }

        // POST: Convites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VisitanteID,ReuniaoID")] Convite convite)
        {
  

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(convite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConviteExists(id))
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
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "VisitanteNome", convite.VisitanteID);
            return View(convite);
        }

        // GET: Convites/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var chaves = id.Split("_");
            var visitanteID = int.Parse(chaves[0]);
            var reuniaoID = int.Parse(chaves[1]);

            if (id == null)
            {
                return NotFound();
            }

            var convite = await _context.Convites
                .Include(c => c.Reunioes)
                .Include(c => c.Visitantes)
                .FirstOrDefaultAsync(m => m.ReuniaoID == reuniaoID && m.VisitanteID == visitanteID);

            if (convite == null)
            {
                return NotFound();
            }

            return View(convite);
        }

        // POST: Convites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chaves = id.Split("_");
            var visitanteID = int.Parse(chaves[0]);
            var reuniaoID = int.Parse(chaves[1]);

            var convite = await _context.Convites.FindAsync(reuniaoID, visitanteID);
            _context.Convites.Remove(convite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConviteExists(string id)
        {

            var chaves = id.Split("_");
            var visitanteID = int.Parse(chaves[0]);
            var reuniaoID = int.Parse(chaves[1]);

            return _context.Convites.Any(e => e.ReuniaoID == reuniaoID && e.VisitanteID == visitanteID) ;
        }
    }
}
