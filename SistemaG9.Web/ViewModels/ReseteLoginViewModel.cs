using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Web.ViewModels
{
    public class ReseteLoginViewModel
    {
        [Required]
        [Display(Name = "Senha atual")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirma Senha")]
        [CompareAttribute("Senha", ErrorMessage = "Senhas não conferem. Corriga!")]
        public string ConfirmaSenha { get; set; }
    }
}