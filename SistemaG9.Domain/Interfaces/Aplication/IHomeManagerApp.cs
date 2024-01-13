using SistemaG9.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaG9.Domain.Interfaces.Aplication
{
    public interface IHomeManagerApp
    {
        /// <summary>
        /// Verificar e retornar doações a realizar
        /// </summary>
        /// <param name="nivel"></param>
        /// <param name="cliente"></param>
        /// <returns>
        /// Retorna Lista de doações pendentes
        /// </returns>
        List<Doacoes> VerificaDoacoesEmAberto(int nivel, string apelido);

        /// <summary>
        /// Verificar se possui Comprovantes a ser liberado
        /// </summary>
        /// <param name="nivel"></param>

    }
}
