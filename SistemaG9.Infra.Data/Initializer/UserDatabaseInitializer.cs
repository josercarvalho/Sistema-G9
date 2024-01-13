using System;
using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Infra.Data.Initializer
{
    public class UserDatabaseInitializer
    {
        public static List<PerfilUsuario> GetPerfisUsuarios()
        {
            var perfisUsuario = new List<PerfilUsuario>
            {
                new PerfilUsuario
                {
                    PerfilUsuarioId = 1,
                    DataCadastro = DateTime.Now,
                    FlAdminMaster =true,
                    FlAtivo = true,
                    NomPerfil = "Administrador Master"
                },
                new PerfilUsuario
                {
                    PerfilUsuarioId = 2,
                    DataCadastro = DateTime.Now,
                    FlAdminMaster =true,
                    FlAtivo = true,
                    NomPerfil = "Usuário Master"
                },
                new PerfilUsuario
                {
                    PerfilUsuarioId = 3,
                    DataCadastro = DateTime.Now,
                    FlAdminMaster =true,
                    FlAtivo = true,
                    NomPerfil = "Usuário Padrão"
                }
            };
            return perfisUsuario;
        }

        public static List<Clientes> GetUsuarios()
        {
            var clientes = new List<Clientes>
            {
                new Clientes               {
                    ClienteId = 1,
                    Nome = "Administrador Master do Sistema",
                    Login = "admin",
                    DataCadastro = DateTime.Now,
                    Email = "admin@sistemag9.com.br",
                    PerfilUsuarioId = 1,
                    Senha = "@Senha2016"
                },
                new Clientes             {
                    ClienteId = 2,
                    Nome = "José Ribeiro",
                    Login = "josercarvalho",
                    DataCadastro = DateTime.Now,
                    Email = "josercarvalho@gmail.com",
                    PerfilUsuarioId = 3,
                    Senha = "@Senha2017",
                }
            };
            return clientes;
        }
    }
}
