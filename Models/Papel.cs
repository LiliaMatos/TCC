using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortariaInteligente.Models
{
    public class Papel
    {
        [Key] public int PapelID { get; set; }

        [Required]
        [Display(Name = "Nome do Papel")]
        public string PapelNome { get; set; }

        public virtual ICollection<PapelVisitado> PapeisVisitados{ get; set; }
        public ICollection<Visitante> Visitantes { get; set; }

    }
}

