using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortariaInteligente.Models
{
    public class Visitado
    {
        [Key]
        public int VisitadoID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
        Display(Name = "Visitante"),
        RegularExpression(@"^[A-Z]+[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÒÖÚÇÑ""'\s-]*$", ErrorMessage = "Insira o nome com pelo menos uma letra maiúscula e mais de 5 caracteres."),
        StringLength(150, MinimumLength = 5, ErrorMessage = "O campo tem que ter no mínimo 5 caracteres.")]
        public string VisitadoNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
        Display(Name = "E-mail"),
        RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Insira o @ e domínio do e-mail."),
        DataType(DataType.EmailAddress)]
        public string VisitadoEmail { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
         Display(Name = "Celular"),
         DataType(DataType.PhoneNumber),
         StringLength(15, MinimumLength = 15, ErrorMessage = "Insira um telefone no formato (99)9999-999.")]
        public string VisitadoCel { get; set; }

        [Display(Name = "Papel")]
        public Papel Papeis { get; set; }

        public virtual ICollection<Reuniao> Reunioes { get; set; }

    }
}

