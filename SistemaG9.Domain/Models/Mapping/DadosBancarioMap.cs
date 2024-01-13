using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class DadosBancarioMap : EntityTypeConfiguration<DadosBancarioViewModel>
    {
        public DadosBancarioMap()
        {
            // Primary Key
            this.HasKey(t => t.DadosId);

            // Properties
            this.Property(t => t.Agencia)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.Conta)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.Operacao)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("DadosBancarios");
            this.Property(t => t.DadosId).HasColumnName("DadosId");
            this.Property(t => t.ClienteId).HasColumnName("ClienteId");
            this.Property(t => t.BancoId).HasColumnName("BancoId");
            this.Property(t => t.TipoConta).HasColumnName("TipoConta");
            this.Property(t => t.Agencia).HasColumnName("Agencia");
            this.Property(t => t.Conta).HasColumnName("Conta");
            this.Property(t => t.Operacao).HasColumnName("Operacao");

            // Relationships
            this.HasRequired(t => t.Banco)
                .WithMany(t => t.DadosBancarios)
                .HasForeignKey(d => d.BancoId);
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.DadosBancarios)
                .HasForeignKey(d => d.ClienteId);

        }
    }
}
