using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class PaisMap : EntityTypeConfiguration<Pais>
    {
        public PaisMap()
        {
            // Primary Key
            HasKey(t => t.PaisId);

            // Properties
            Property(t => t.Sigla)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            ToTable("Pais");
            Property(t => t.PaisId).HasColumnName("PaisId");
            Property(t => t.Sigla).HasColumnName("Sigla");
            Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
