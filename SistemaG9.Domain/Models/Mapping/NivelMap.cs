using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class NivelMap : EntityTypeConfiguration<Nivel>
    {
        public NivelMap()
        {
            // Primary Key
            HasKey(t => t.NivelId);

            // Properties
            // Table & Column Mappings
            ToTable("Nivel");
            Property(t => t.NivelId).HasColumnName("NivelId");
            Property(t => t.MatrizId).HasColumnName("MatrizId");
            Property(t => t.NivelMatriz).HasColumnName("Nivel");
            Property(t => t.Inicio).HasColumnName("Inicio");
            Property(t => t.Fim).HasColumnName("Fim");
            Property(t => t.Entradas).HasColumnName("Entradas");
            Property(t => t.ValorEntrada).HasColumnName("ValorEntrada");
            Property(t => t.Reentradas).HasColumnName("Reentradas");
            Property(t => t.ValorReentrada).HasColumnName("ValorReentrada");           
        }
    }
}
