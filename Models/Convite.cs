using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortariaInteligente.Models
{
    public class Convite
    {
        [Display(Name = "Visitante")]
        public int VisitanteID { get; set; }
        public virtual Visitante Visitantes { get; set; }

        [Display(Name = "Reunião")]
        public int ReuniaoID { get; set; }
        public virtual Reuniao Reunioes { get; set; }

        public bool Confirmado { get; set; }

    }
}
