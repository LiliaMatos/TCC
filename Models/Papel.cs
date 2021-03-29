using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortariaInteligente.Models
{
    public enum Papel
    {
        [Display(Name = "Visitante")]
        Visitante = 1,
        [Display(Name = "Visitado")]
        Visitado = 2,
        [Display(Name = "Portaria")]
        Portaria = 3,
        [Display(Name = "Recepção")]
        Recepcao = 4,
        [Display(Name = "Administrador")]
        Administrador = 5

    }
}

