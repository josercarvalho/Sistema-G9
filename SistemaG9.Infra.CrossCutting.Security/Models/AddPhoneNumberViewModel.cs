using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Infra.CrossCutting.Security.Models
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número do Telefone")]
        public string Number { get; set; }
    }
}
