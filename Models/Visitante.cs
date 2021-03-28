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
         Display(Name = "Nome"),
         RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$"),
         StringLength(150, MinimumLength = 5)]
        public string VisitanteNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
         Display(Name = "E-mail"),
         DataType(DataType.EmailAddress)]
       
        public string VisitanteEmail { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
         Display(Name = "Celular"),
         DataType(DataType.PhoneNumber),
         StringLength(15, MinimumLength = 15)]
         public string VisitanteCel { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório"),
         Display(Name = "Empresa")]
        public string Empresa { get; set; }

        [Required, ForeignKey("PapelID")]
        public int PapelID { get; set; }
        public virtual Papel Papeis { get; set; }

        [Required, ForeignKey("DocumentoID"), Display(Name = "Tipo Docto")]
        public int DocumentoID { get; set; }
        public virtual Documento Documentos { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"), Display(Name = "Número")]
        public string DocumentoNumero { get; set; }

        [Display(Name = "Marca")]
        public string CarroMarca { get; set; }

        [Display(Name = "Cor")]
        public string CarroCor { get; set; }

        [Display(Name = "Modelo")]
        public string CarroModelo { get; set; }
        
        [Display(Name = "Placa")]
        public string CarroPlaca { get; set; }

        public virtual ICollection<Convite> Convites { get; set; }

        public List<Visitante> ListaVisitantes { get; set; }


    }


}