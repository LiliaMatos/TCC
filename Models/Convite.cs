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
        [Display(Name = "Visitante")]
        public virtual Visitante Visitantes { get; set; }

        [Display(Name = "Reunião")]
        public int ReuniaoID { get; set; }

        [Display(Name = "Reunião")]
        public virtual Reuniao Reunioes { get; set; }

        public bool LiberadoPortaria { get; set; }
        public bool LiberadoRecepcao { get; set; }

    }
}
