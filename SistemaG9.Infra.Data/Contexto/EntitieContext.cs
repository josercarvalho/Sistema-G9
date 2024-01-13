using SistemaG9.Domain.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using SistemaG9.Domain.Models.Mapping;

namespace SistemaG9.Infra.Data.Contexto
{
    public class EntitieContext : DbContext 
    {
        public EntitieContext()
            : base("sistemag_data9Context")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Arquivos> Arquivo { get; set; }
        public DbSet<Bancos> Bancos { get; set; }
        public DbSet<Cidades> Cidade { get; set; }
        public DbSet<Clientes> Cliente { get; set; }
        public DbSet<DadosBancarioViewModel> DadoBancarios { get; set; }
        public DbSet<Doacoes> Doacao { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Estados> Estado { get; set; }
        public DbSet<Matriz> Matriz { get; set; }
        public DbSet<Nivel> Niveis { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        public DbSet<Rede> Rede { get; set; }
        public DbSet<Reentrada> Reentradas { get; set; }
        public DbSet<UpLine> UpLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.ReflectedType != null && p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(150));

            modelBuilder.Configurations.Add(new ArquivoMap());
            modelBuilder.Configurations.Add(new BancoMap());
            modelBuilder.Configurations.Add(new CidadeMap());
            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new DadosBancarioMap());
            modelBuilder.Configurations.Add(new DoacaoMap());
            modelBuilder.Configurations.Add(new EntradaMap());
            modelBuilder.Configurations.Add(new EstadoMap());
            modelBuilder.Configurations.Add(new MatrizMap());
            modelBuilder.Configurations.Add(new NivelMap());
            modelBuilder.Configurations.Add(new PaisMap());
            modelBuilder.Configurations.Add(new PerfilUsuarioMap());
            modelBuilder.Configurations.Add(new RedeMap());
            modelBuilder.Configurations.Add(new ReentradaMap());
            modelBuilder.Configurations.Add(new UpLineMap());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }

        //public System.Data.Entity.DbSet<SistemaG9.MVC.ViewModels.CadastroViewModel> CadastroViewModels { get; set; }
    }
}
