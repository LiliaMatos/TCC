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
using System.Globalization;
using System.Threading;
using PortariaInteligente.ViewModels;

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
        public async Task<IActionResult> Index(DateTime data, string assunto, string visitado)
        {
            var reunioes = from m in _context.Reunioes
                           .Include("Visitados")
                           where m.ReuniaoData >= DateTime.Today
                           select m;
            //A mensagem nunca é exibida.
            if (reunioes.Count() < 1)
                TempData["Mensagem"] = "Não existem reuniões agendadas";
            //ModelState.AddModelError("ReuniaoData", "Não existem reuniões agendadas");
      

            if (!String.IsNullOrEmpty(assunto))
            {
                reunioes = reunioes.Where(s => s.ReuniaoNome.Contains(assunto));
            }
            if (!String.IsNullOrEmpty(visitado))
            {
                reunioes = reunioes.Where(s => s.Visitados.VisitadoNome.Contains(visitado));
            }
            if (data.Year != 0001)
            {
                reunioes = reunioes.Where(s => s.ReuniaoData == data);
            }
            return View(await reunioes.ToListAsync());
        }

        // GET: Reunioes/ExibirReubiaoViewModel
        public IActionResult ExibirReuniaoViewModel()
        {
            //Exercício com ViewModel
            /*Fazer uma caixa de seleção com a relação das reuniões existentes e a partir de ID daquela selecionada relacionar os convidados.
             Por hora mostra uma reunião com ID em hardcoding e todos os visitantes cadastrados
             */

            var reunioes = from m in _context.Reunioes.Include("Visitados").Include("Convites").Include("Visitantes") select m;

            //Selecionar uma reunião
            Reuniao reuniao = _context.Reunioes.FirstOrDefault(x => x.ReuniaoID == 68);

            //Criar uma lista para os convidados daquela reunião
            var visitantes = new List<Visitante>(from m in _context.Visitantes select m);


            var ViewModel = new ReuniaoViewModel
            {
                Reuniao = (Reuniao)reuniao,
                qtdVisitantes = 3,
                listaVisitantes = visitantes
            };
            return View(ViewModel);
        }

        // GET: Reunioes/Details/5
        public IActionResult Details(int id)
        {

            var Reuniao = GetReuniao(id);

            return View(Reuniao);
        }

        // GET: Reunioes/Create
        public IActionResult Create()
        {
            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome");
            ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "SalaNome");
            return View(new Reuniao());
        }

        // POST: Reunioes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReuniaoID,VisitadoID,ReuniaoNome,ReuniaoData,ReuniaoHora,SalaID, Convites")] Reuniao reuniao)
        {
            //Testa se a data é anterior a data atual para não permitir criação de reuniõe no passado, mas não está funcionando, o resultado dá sempre zero.

            if (reuniao.ReuniaoData < DateTime.Today)
                ModelState.AddModelError("ReuniaoData", "A data encontra-se no passado!");

            if (ModelState.IsValid)
            {
                _context.Add(reuniao);
                //int idReuniao = reuniao.ReuniaoID;
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateConvites", new { ReuniaoID = reuniao.ReuniaoID });

            }
            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome");
            ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "SalaNome");
            return View(reuniao);
        }

        // GET:  Reunioes/CreateConvites
        public IActionResult CreateConvites(int ReuniaoID)
        {
            ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome");
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "VisitanteNome");
            ViewData["Mensagem"] = TempData["Mensagem"];
            return View(new Convite { ReuniaoID = ReuniaoID });
        }

        // POST: Reunioes/CreateConvites
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConvites([Bind("VisitanteID,ReuniaoID")] Convite convite)
        {
            if (ConviteExists(convite.VisitanteID + "_" + convite.ReuniaoID))
            {
                ModelState.AddModelError("VisitanteID", "Já existe um convite para este visitante");
            }

            else if (ModelState.IsValid)
            {
                _context.Add(convite);
                await _context.SaveChangesAsync();
                //Depois de salvar retorna para o Index de Convites
                TempData["Mensagem"] = "Convite adicionado.";
                return RedirectToAction(nameof(CreateConvites), new { ReuniaoID = convite.ReuniaoID });
            }
            ViewData["ReuniaoID"] = new SelectList(_context.Reunioes, "ReuniaoID", "ReuniaoNome", convite.ReuniaoID);
            ViewData["VisitanteID"] = new SelectList(_context.Visitantes, "VisitanteID", "VisitanteNome", convite.VisitanteID);
            return View(convite);
        }


        // GET: Reunioes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniao = TempData["Model"] ?? GetReuniao(id.Value);
            if (reuniao == null)
            {
                return NotFound();
            }

            ViewData["VisitanteID"] = new SelectList(_context.Visitantes.Select(x => new
            {
                x.VisitanteID,
                VisitanteNome = x.VisitanteNome + " - " + x.VisitanteEmail
            }), "VisitanteID", "VisitanteNome");

            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome");
            ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "SalaNome");
            return View(reuniao);
        }

        // POST: Reunioes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReuniaoID,VisitadoID,SalaID,ReuniaoNome,ReuniaoData,ReuniaoHora,Convites")] Reuniao reuniao)
        //ReuniaoSala, 
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

            ViewData["VisitanteID"] = new SelectList(_context.Visitantes.Select(x => new
            {
                x.VisitanteID,
                VisitanteNome = x.VisitanteNome + " - " + x.VisitanteEmail
            }), "VisitanteID", "VisitanteNome");

            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome", reuniao.VisitadoID);
            ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "SalaNome", reuniao.SalaID);
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

        // GET: PainelPortaria
        public async Task<IActionResult> PainelPortaria(string visitante, string visitado)
        {

            var convites = from m in _context.Convites.Include("Visitantes").Include("Reunioes").Include("Reunioes.Visitados") where m.Reunioes.ReuniaoData == DateTime.Today && m.LiberadoPortaria == false select m;

            if (!String.IsNullOrEmpty(visitante))
            {
                convites = convites.Where(s => s.Visitantes.VisitanteNome.Contains(visitante));
            }
            if (!String.IsNullOrEmpty(visitado))
            {
                convites = convites.Where(s => s.Reunioes.Visitados.VisitadoNome.Contains(visitado));
            }

            return View(await convites.ToListAsync());
        }

        private bool ReuniaoExists(int id)
        {
            return _context.Reunioes.Any(e => e.ReuniaoID == id);
        }
        public Reuniao GetReuniao(int id)
        {
            Reuniao listaTodasReunioes = _context.Reunioes.
                Include("Visitados").
                Include("Convites").
                Include("Convites.Visitantes").
                Include("Salas").
                FirstOrDefault(x => x.ReuniaoID == id);
            return (listaTodasReunioes);
        }
        private bool ConviteExists(string id)
        {

            var chaves = id.Split("_");
            var visitanteID = int.Parse(chaves[0]);
            var reuniaoID = int.Parse(chaves[1]);

            return _context.Convites.Any(e => e.ReuniaoID == reuniaoID && e.VisitanteID == visitanteID);
        }


        // POST: Reunioes/AddConvite
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddConvite([Bind("ReuniaoID,VisitadoID,ReuniaoNome,ReuniaoData,ReuniaoHora, Convites")] Reuniao reuniao)
        //ReuniaoSala,
        {
            reuniao.Convites.Add(new Convite());

            ViewData["VisitanteID"] = new SelectList(_context.Visitantes.Select(x => new
            {
                x.VisitanteID,
                VisitanteNome = x.VisitanteNome + " - " + x.VisitanteEmail
            }), "VisitanteID", "VisitanteNome");

            ViewData["VisitadoID"] = new SelectList(_context.Visitados, "VisitadoID", "VisitadoNome");

            if (reuniao.ReuniaoID == 0)
            {
                return View(nameof(Create), reuniao);
            }
            else
            {
                TempData["Model"] = reuniao;
                return RedirectToAction(nameof(CreateConvites), new { id = reuniao.ReuniaoID });

            }

        }


        // GET: Reunioes/ExibirConvites
        public async Task<IActionResult> ExibirConvites(string id)
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


        // POST: Reunioes/ConfirmarVisita
        public async Task<IActionResult> ConfirmarVisita(string id)
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

            convite.LiberadoPortaria = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("PainelPortaria");
        }
    }
}
