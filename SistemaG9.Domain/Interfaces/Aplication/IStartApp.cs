using SistemaG9.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaG9.Domain.Interfaces.Aplication
{
    public interface IStartApp
    {
        /// <summary>
        /// Verificar o Status do Cliente
        /// </summary>
        /// <param name="nivel"></param>
        /// <param name="apelido"></param>
        /// <returns>
        /// Retorna ATIVO ou INATIVO para operar Escritório
        /// </returns>
        Clientes ValidaStatus(int nivel, string apelido);

        /// <summary>
        /// Verificar Status do Cliente
        /// </summary>
        /// <param name="nivel"></param>
        /// <param name="apelido"></param>
        /// <returns>
        /// Retorna ATIVO ou INATIVO para operar Escritório
        /// </returns>

    }
}
