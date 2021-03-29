using System.ComponentModel.DataAnnotations;

namespace PortariaInteligente.Models
{
    public enum Documento
    {
        [Display(Name = "RG")]
        RG = 1,
        [Display(Name = "CNH")]
        CNH = 2,
        [Display(Name = "RNE")]
        RNE = 3,
        [Display(Name = "Passaporte")]
        Passaporte = 4


    }
}
