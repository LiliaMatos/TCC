using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortariaInteligente.Data;
using PortariaInteligente.Models;

namespace PortariaInteligente.Controllers
{
    public class ReunioesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReunioesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reunioes
        public async Task<IActionResult> Index(string data, string assunto, string visitado)
        {
            //var applicationDbContext = _context.Reunioes.Include(r => r.Visitados);

            var reunioes = from m in _context.Reunioes select m;
            var vistidados = from m in _context.Visitados select m;

            if (!String.IsNullOrEmpty(data))
            {
                reunioes = reunioes.Where(s => s.ReuniaoData.ToString(data).Contains(data));
            }
            if (!String.IsNullOrEmpty(assunto))
            {
                reunioes = reunioes.Where(s => s.ReuniaoNome.Contains(assunto));
            }
            if (!String.IsNullOrEmpty(visitado))
            {
               // reunioes = reunioes.Where(s => s.VisitadoID.Contains(visitado));
            }
            return View(await reunioes.ToListAsync());

            //return View(await applicationDbContext.ToListAsync());



        }

        // GET: Reunioes/Details/5
        public IActionResult Details(int id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var reuniao = await _context.Reunioes
                .Include(r => r.Visitados)
                .FirstOrDefaultAsync(m => m.ReuniaoID == id);
            if (reuniao == null)
            {
                return NotFound();
            }
               return View(reuniao);
             */

            dynamic dy = new ExpandoObject();
            dy.reuniao = GetReuniao(id);
            dy.convites = GetConvites(id);
            return View(dy);         
        }

        // GET: Reunioes/Create
        public IActionResult Create()
        {
            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome");
            return View();
        }

        // POST: Reunioes/Create
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReuniaoID,VisitadoID,ReuniaoNome,ReuniaoData,ReuniaoHora,ReuniaoSala")] Reuniao reuniao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reuniao);
                int idReuniao = reuniao.ReuniaoID;
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Convites", new { id = idReuniao });
               
            }
            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome", reuniao.VisitadoID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Reunioes/Convites/5
        public IActionResult Convites(int id)
        {
            List<Reuniao> listaReuniaoID = GetReuniao(id);            
            /*
            var reuniao = await _context.Reunioes.FindAsync(id);
            if (reuniao == null)
            {
                return NotFound();
            }
            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome", reuniao.VisitadoID);
            return View(reuniao);

            */
            //ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome");
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "VisitanteNome");
            return View();
           
        }

        // POST: Reunioes/Convites
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Convites([Bind("VisitanteID,ReuniaoID")] Convite convite, int id)
        {
            if (ModelState.IsValid)
            {
                convite.ReuniaoID = id;
                _context.Add(convite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Convites));
            }
            //ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome", convite.ReuniaoID);
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "VisitanteNome", convite.VisitanteID);
            // return View(convite);
            return RedirectToAction(nameof(Convites));
        }

        // GET: Reunioes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniao = await _context.Reunioes.FindAsync(id);
            if (reuniao == null)
            {
                return NotFound();
            }
            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome", reuniao.VisitadoID);
            return View(reuniao);
        }

        // POST: Reunioes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReuniaoID,VisitadoID,ReuniaoNome,ReuniaoData,ReuniaoHora,ReuniaoSala")] Reuniao reuniao)
        {
            if (id != reuniao.ReuniaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reuniao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReuniaoExists(reuniao.ReuniaoID))
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
            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome", reuniao.VisitadoID);
            return View(reuniao);
        }

        // GET: Reunioes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniao = await _context.Reunioes
                .Include(r => r.Visitados)
                .FirstOrDefaultAsync(m => m.ReuniaoID == id);
            if (reuniao == null)
            {
                return NotFound();
            }

            return View(reuniao);
        }

        // POST: Reunioes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reuniao = await _context.Reunioes.FindAsync(id);
            _context.Reunioes.Remove(reuniao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReuniaoExists(int id)
        {
            return _context.Reunioes.Any(e => e.ReuniaoID == id);
        }
        public List<Reuniao> GetReuniao(int id)
        {
            List<Reuniao> listaTodasReunioes = _context.Reunioes.ToList();

            List<Reuniao> listaReuniaoID = listaTodasReunioes.Where(x => x.ReuniaoID == id).ToList();
            //Fazer um Join com a tabela visitado e buscar o nome, e-mail dele

            return (listaReuniaoID);
        }
        public List<Convite> GetConvites(int id)
        {
            List<Convite> listaTodosConvites = _context.Convites.ToList();

            List<Convite> listaConvitesID = listaTodosConvites.Where(x => x.ReuniaoID == id).ToList();
            //Fazer um Join com a tabela visitante o buscar o nome, e-mail e empresa dele

            return (listaConvitesID);
        }
    }
}
