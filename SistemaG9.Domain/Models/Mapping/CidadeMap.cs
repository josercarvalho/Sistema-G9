using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class CidadeMap : EntityTypeConfiguration<Cidades>
    {
        public CidadeMap()
        {
            // Primary Key
            HasKey(t => t.CidadeId);

            // Properties
            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Cidade");
            Property(t => t.CidadeId).HasColumnName("CidadeId");
            Property(t => t.EstadoId).HasColumnName("EstadoId");
            Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            HasRequired(t => t.Estado)
                .WithMany(t => t.Cidades)
                .HasForeignKey(d => d.EstadoId);

        }
    }
}
