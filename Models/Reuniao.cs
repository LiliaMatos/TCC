using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortariaInteligente.Models
{
    public class Reuniao
    {
        public Reuniao()
        {
            Convites = new List<Convite>();
            ReuniaoData = DateTime.Today;
        }

        [Key]
        public int ReuniaoID { get; set; }

        [Required]
        [ForeignKey("VisitadoID")]
        [Display(Name = "Visitado")]      
        public int VisitadoID { get; set; }


        [Display(Name = "Visitado")]
        public virtual Visitado Visitados { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Assunto")]
        public string ReuniaoNome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]        
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime ReuniaoData { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]      
        [ DataType(DataType.Time)]
        [Display(Name = "Hora")]
        public DateTime ReuniaoHora { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]   
        [ForeignKey("SalaID")]
        [Display(Name = "Sala")]
        public int SalaID { get; set; }   
        public virtual Sala Salas { get; set; }

        public virtual IList<Convite> Convites { get; set; }

    }
}
