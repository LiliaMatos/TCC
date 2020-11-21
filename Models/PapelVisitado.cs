using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortariaInteligente.Models
{
    public class PapelVisitado
    {
        public int VisitadoID { get; set; }
        public virtual Visitado Visitados { get; set; }


        public int PapelID { get; set; }
        public virtual Papel Papeis { get; set; }
    }
}
