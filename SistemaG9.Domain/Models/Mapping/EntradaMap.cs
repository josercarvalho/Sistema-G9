using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class EntradaMap : EntityTypeConfiguration<Entrada>
    {
        public EntradaMap()
        {
            // Primary Key
            HasKey(t => t.EntradaId);
            
            // Properties
            // Table & Column Mappings
            ToTable("Entradas");
            Property(t => t.EntradaId).HasColumnName("EntradaId");
            Property(t => t.Apelido).HasColumnName("Apelido");
            Property(t => t.Nivel).HasColumnName("Nivel");
            Property(t => t.Amigo).HasColumnName("Amigo");
            Property(t => t.Deposito).HasColumnName("Deposito");
            Property(t => t.Status).HasColumnName("Status");
            
            // Relationships
            HasRequired(t => t.Rede)
                .WithMany()
                .HasForeignKey(d => d.Apelido);
        }
    }
}
