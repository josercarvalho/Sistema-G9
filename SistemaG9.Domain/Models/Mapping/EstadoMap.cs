using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class EstadoMap : EntityTypeConfiguration<Estados>
    {
        public EstadoMap()
        {
            // Primary Key
            HasKey(t => t.EstadoId);

            // Properties
            Property(t => t.Sigla)
                .IsRequired()
                .HasMaxLength(150);

            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Estado");
            Property(t => t.EstadoId).HasColumnName("EstadoId");
            Property(t => t.PaisId).HasColumnName("PaisId");
            Property(t => t.Sigla).HasColumnName("Sigla");
            Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            HasRequired(t => t.Pai)
                .WithMany(t => t.Estadoes)
                .HasForeignKey(d => d.PaisId);

        }
    }
}
