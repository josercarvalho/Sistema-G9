using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class ReentradaMap : EntityTypeConfiguration<Reentrada>
    {
        public ReentradaMap()
        {
            // Primary Key
            HasKey(t => t.ReentradaId);

            // Properties
            // Table & Column Mappings
            ToTable("Reentradas");
            Property(t => t.ReentradaId).HasColumnName("ReentradaId");
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
