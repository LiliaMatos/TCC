using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortariaInteligente.Models
{
    public class Visitado
    {
        [Key]
        public int VisitadoID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Nome")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(150, MinimumLength = 5)]
        public string VisitadoNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string VisitadoEmail { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Celular")]
        [DataType(DataType.PhoneNumber)]
        public string VisitadoCel { get; set; }

        public virtual ICollection<Reuniao> Reunioes { get; set; }
        public virtual ICollection<PapelVisitado> PapeisVisitados { get; set; }

    }
}

