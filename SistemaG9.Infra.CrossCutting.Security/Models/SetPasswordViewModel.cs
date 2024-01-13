using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Infra.CrossCutting.Security.Models
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "As senhas não correspondem.")]
        public string ConfirmPassword { get; set; }
    }
}
