using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Infra.CrossCutting.Security.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
