using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Infra.CrossCutting.Security.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }
    }
}
