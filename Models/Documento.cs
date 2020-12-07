using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortariaInteligente.Models
{
    public class Documento
    {
        [Key]
        public int DocumentoID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Tipo do Documento")]
        public string DocumentoNome { get; set; }
        public virtual ICollection<Visitante> Visitantes { get; set; }
    }
}
