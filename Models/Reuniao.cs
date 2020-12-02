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

        [Required, Display(Name = "Organizador"), ForeignKey("VisitadoID")]
        public int VisitadoID { get; set; }

        [Display(Name = "Organizador")]
        public virtual Visitado Visitados { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Assunto")]
        public string ReuniaoNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Data"), DataType(DataType.Date)]
        public DateTime ReuniaoData { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Hora"), DataType(DataType.Time)]
        public DateTime ReuniaoHora { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Sala")]
        public string ReuniaoSala { get; set; }

        public virtual IList<Convite> Convites { get; set; }

    }
}
