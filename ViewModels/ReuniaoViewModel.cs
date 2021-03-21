using PortariaInteligente.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PortariaInteligente.Data;

namespace PortariaInteligente.ViewModels
{
    public class ReuniaoViewModel
    {  
        public  Reuniao Reuniao{ get; set; }
        public int qtdVisitantes { get; set; }
        public List<Visitante> listaVisitantes { get; set; }
    }
}
