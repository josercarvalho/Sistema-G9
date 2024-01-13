using System.Collections.Generic;

namespace SistemaG9.Infra.CrossCutting.Security.Models
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<string> Providers { get; set; }
    }
}
