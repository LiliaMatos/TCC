using PortariaInteligente.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortariaInteligente.ViewModels
{
    public class ReuniaoViewModel
    {
       
        public List <Reuniao> listaReuniao { get; set; }

        /*[Required(ErrorMessage = "O campo {0} é obrigatório")]
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

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Qtd Visitantes")]*/
        public int qtdVisitantes { get; set; }
        public List<Visitante> listaVisitantes { get; set; }
    }
}
