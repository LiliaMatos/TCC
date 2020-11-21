using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortariaInteligente.Models
{
    public class Convite
    {
        public int VisitanteID { get; set; }
        public virtual Visitante Visitantes { get; set; }

        public int ReuniaoID { get; set; }
        public virtual Reuniao Reunioes { get; set; }

    }
}
