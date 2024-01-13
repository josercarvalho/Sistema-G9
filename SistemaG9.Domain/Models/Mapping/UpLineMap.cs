using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class UpLineMap : EntityTypeConfiguration<UpLine>
    {
        public UpLineMap()
        {
            // Primary Key
            this.HasKey(t => t.UpLineId);

            // Properties
            // Table & Column Mappings
            this.ToTable("UpLine");
            this.Property(t => t.UpLineId).HasColumnName("UpLineId");
            this.Property(t => t.ClienteId).HasColumnName("ClienteId");
            this.Property(t => t.OrigemId).HasColumnName("OrigemId");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.Nivel).HasColumnName("Nivel");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.UpLines)
                .HasForeignKey(d => d.ClienteId);

        }
    }
}
