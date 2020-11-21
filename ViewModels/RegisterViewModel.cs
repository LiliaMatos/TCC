using System.ComponentModel.DataAnnotations;

namespace PortariaInteligente.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password",
            ErrorMessage = "Senha e confirmação de senha dão diferentes")]
        public string ConfirmPassword { get; set; }
    }
}
