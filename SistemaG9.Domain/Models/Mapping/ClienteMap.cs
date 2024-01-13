using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class ClienteMap : EntityTypeConfiguration<Clientes>
    {
        public ClienteMap()
        {
            // Primary Key
            HasKey(t => t.ClienteId);

            // Properties
            Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Senha)
                .IsRequired()
                .HasMaxLength(2048);

            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(200);

            Property(t => t.CPF)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(200);

            Property(t => t.Endereco)
                .IsRequired()
                .HasMaxLength(150);

            Property(t => t.Numero)
                .IsRequired()
                .HasMaxLength(150);

            Property(t => t.Complemento)
                .HasMaxLength(150);

            Property(t => t.Bairro)
                .IsRequired()
                .HasMaxLength(150);

            Property(t => t.CEP)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.Telefone)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Operadora)
                .IsRequired()
                .HasMaxLength(5);

            Property(t => t.WhatsApp)
                .HasMaxLength(20);

            Property(t => t.SKYPE)
                .HasMaxLength(150);

            // Table & Column Mappings
            ToTable("Cliente");
            Property(t => t.ClienteId).HasColumnName("ClienteId");
            Property(t => t.PerfilUsuarioId).HasColumnName("PerfilUsuarioId");
            Property(t => t.Login).HasColumnName("Login");
            Property(t => t.Senha).HasColumnName("Senha");
            Property(t => t.Nome).HasColumnName("Nome");
            Property(t => t.DataNascimento).HasColumnName("DataNascimento");
            Property(t => t.CPF).HasColumnName("CPF");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.Endereco).HasColumnName("Endereco");
            Property(t => t.Numero).HasColumnName("Numero");
            Property(t => t.Complemento).HasColumnName("Complemento");
            Property(t => t.Bairro).HasColumnName("Bairro");
            Property(t => t.CEP).HasColumnName("CEP");
            Property(t => t.PaisId).HasColumnName("PaisId");
            Property(t => t.CidadeId).HasColumnName("CidadeId");
            Property(t => t.EstadoId).HasColumnName("EstadoId");
            Property(t => t.Telefone).HasColumnName("Telefone");
            Property(t => t.Operadora).HasColumnName("Operadora");
            Property(t => t.WhatsApp).HasColumnName("WhatsApp");
            Property(t => t.SKYPE).HasColumnName("SKYPE");
            Property(t => t.DataCadastro).HasColumnName("DataCadastro");
            //Property(t => t.Reentradas).HasColumnName("Reentradas");
            Property(t => t.Status).HasColumnName("Status");

            // Relationships
            //this.HasRequired(t => t.Cidade)
            //    .WithMany(t => t.Clientes)
            //    .HasForeignKey(d => d.CidadeId);
            //this.HasRequired(t => t.Estado)
            //    .WithMany(t => t.Clientes)
            //    .HasForeignKey(d => d.EstadoId);
            //HasRequired(t => t.PerfilUsuario)
            //    .WithMany(t => t.Clientes)
            //    .HasForeignKey(d => d.PerfilUsuarioId);

        }
    }
}
