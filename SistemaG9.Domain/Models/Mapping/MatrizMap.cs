using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class MatrizMap : EntityTypeConfiguration<Matriz>
    {
        public MatrizMap()
        {
            // Primary Key
            HasKey(t => t.MatrizId);

            // Properties
            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(25);

            // Table & Column Mappings
            ToTable("Matriz");
            Property(t => t.MatrizId).HasColumnName("MatrizId");
            Property(t => t.Nome).HasColumnName("Nome");
            Property(t => t.Niveis).HasColumnName("Niveis");
            Property(t => t.Integrantes).HasColumnName("Integrantes");
        }
    }
}
