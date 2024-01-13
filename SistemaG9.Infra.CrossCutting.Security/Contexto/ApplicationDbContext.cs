using Microsoft.AspNet.Identity.EntityFramework;
using SistemaG9.Infra.CrossCutting.Security.Models;
using System;

namespace SistemaG9.Infra.CrossCutting.Security.Contexto
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public ApplicationDbContext()
            : base("SistemaG9Connection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
