using SistemaG9.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaG9.Domain.Interfaces.Aplication
{
    public interface IDoacaoApp : IAppServiceBase<Doacoes>
    {
        bool ComprovanteEnviado(int id);
    }
}
