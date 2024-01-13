using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class BancoMap : EntityTypeConfiguration<Bancos>
    {
        public BancoMap()
        {
            // Primary Key
            HasKey(t => t.BancoId);

            // Properties
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.BancoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            ToTable("Bancos");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.BancoId).HasColumnName("BancoId");
            Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
