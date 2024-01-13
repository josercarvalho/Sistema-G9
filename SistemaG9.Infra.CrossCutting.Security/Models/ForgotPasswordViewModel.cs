using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Infra.CrossCutting.Security.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
