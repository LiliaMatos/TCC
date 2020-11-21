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
    public class PapeisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PapeisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Papeis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Papeis.ToListAsync());
        }

        // GET: Papeis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var papel = await _context.Papeis
                .FirstOrDefaultAsync(m => m.PapelID == id);
            if (papel == null)
            {
                return NotFound();
            }

            return View(papel);
        }

        // GET: Papeis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Papeis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PapelID,PapelNome")] Papel papel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(papel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(papel);
        }

        // GET: Papeis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var papel = await _context.Papeis.FindAsync(id);
            if (papel == null)
            {
                return NotFound();
            }
            return View(papel);
        }

        // POST: Papeis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PapelID,PapelNome")] Papel papel)
        {
            if (id != papel.PapelID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(papel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PapelExists(papel.PapelID))
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
            return View(papel);
        }

        // GET: Papeis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var papel = await _context.Papeis
                .FirstOrDefaultAsync(m => m.PapelID == id);
            if (papel == null)
            {
                return NotFound();
            }

            return View(papel);
        }

        // POST: Papeis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var papel = await _context.Papeis.FindAsync(id);
            _context.Papeis.Remove(papel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PapelExists(int id)
        {
            return _context.Papeis.Any(e => e.PapelID == id);
        }
    }
}
