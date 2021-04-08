using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortariaInteligente.Models
{
    public class Visitante
    {
        [Key]
        public int VisitanteID { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório"),
        Display(Name = "Visitante"),        
        RegularExpression(@"^[A-Z]+[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÒÖÚÇÑ""'\s-]*$", ErrorMessage = "Insira o nome com pelo menos uma letra maiúscula e mais de 5 caracteres."),
        StringLength(150, MinimumLength = 5, ErrorMessage = "O campo tem que ter no mínimo 5 caracteres.")]
        public string VisitanteNome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório"),
        Display(Name = "E-mail"),
        RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Insira o @ e domínio do e-mail."),
        DataType(DataType.EmailAddress)]
        public string VisitanteEmail { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
         Display(Name = "Celular"),
         DataType(DataType.PhoneNumber),
         StringLength(15, MinimumLength = 15, ErrorMessage = "Insira um telefone no formato (99)9999-999.")]
         public string VisitanteCel { get; set; }

        
        [Required(ErrorMessage = "O campo {0} é obrigatório"),
        Display(Name = "Empresa")]
        public string Empresa { get; set; }


        [Display(Name = "Papel")]
        public Papel Papeis { get; set; }


        [ Display(Name = "Tipo do documento")]
        public Documento Documentos { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [ Display(Name = "Número")]
        public string DocumentoNumero { get; set; }


        [Display(Name = "Marca")]
        public string CarroMarca { get; set; }


        [Display(Name = "Cor")]
        public string CarroCor { get; set; }


        [Display(Name = "Modelo")]
        public string CarroModelo { get; set; }

        
        [Display(Name = "Placa")]
        public string CarroPlaca { get; set; }

        public bool LGPD { get; set; }
        public bool RegrasSegurança { get; set; }
        public bool TermoDeUso { get; set; }


        public virtual ICollection<Convite> Convites { get; set; }

        public List<Visitante> ListaVisitantes { get; set; }


    }


}