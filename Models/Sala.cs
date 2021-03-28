using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortariaInteligente.Models
{
    public class Sala
    {
        [Key]
        public int SalaID { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Sala")]
        public string SalaNome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Lugares")]
        public int QtdLugares { get; set; }

    }
}
