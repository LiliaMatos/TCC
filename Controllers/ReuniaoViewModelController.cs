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
    public class ReuniaoViewModelController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ReuniaoViewModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<Reuniao> GetReuniao(int id)
        {
            List<Reuniao> listaTodasReunioes = _context.Reunioes.ToList();

            List<Reuniao> listaReuniaoID = listaTodasReunioes.Where(x => x.ReuniaoID == id).ToList();

            return (listaReuniaoID);
        }
        public List<Visitante> GetVisitante(string nome)
        {
            List<Visitante> listaTodosVisitantes = _context.Visitantes.ToList();

            //List<Visitante> listaVisitanteNome = listaTodosVisitantes.Where(x => x.VisitanteNome.Contains(nome).ToList();
            var a = from visitante in listaTodosVisitantes 
                    where visitante.VisitanteNome.Contains(nome) 
                    select visitante;

            List<Visitante> listaVisitanteNome = new List<Visitante>();

            foreach (var item in a)
            {
                listaVisitanteNome.Add(item);
            }

            return (listaVisitanteNome);
        }
    }
}
