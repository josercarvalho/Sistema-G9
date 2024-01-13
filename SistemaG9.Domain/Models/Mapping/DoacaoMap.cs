using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class DoacaoMap : EntityTypeConfiguration<Doacoes>
    {
        public DoacaoMap()
        {
            // Primary Key
            HasKey(t => t.DoacoesId);
            Property(t => t.Comprovante).IsOptional().HasMaxLength(150);
            Property(t => t.AnexoExtensao).IsOptional().HasMaxLength(10);
            Property(t => t.AnexoTipo).IsOptional().HasMaxLength(15);
            Property(t => t.DataEnvio).IsOptional();

            // Properties
            // Table & Column Mappings
            ToTable("Doacao");
            Property(t => t.DoacoesId).HasColumnName("DoacoesId");
            Property(t => t.MatrizId).HasColumnName("MatrizId");
            Property(t => t.Nivel).HasColumnName("Nivel");
            Property(t => t.ClienteId).HasColumnName("ClienteId");
            Property(t => t.Doador).HasColumnName("Doador");
            Property(t => t.Recebedor).HasColumnName("Recebedor");
            Property(t => t.Tipo).HasColumnName("Tipo");
            Property(t => t.Valor).HasColumnName("Valor");
            Property(t => t.DataCadastro).HasColumnName("DataCadastro");
            Property(t => t.ExpiraData).IsOptional().HasColumnType("datetime2").HasColumnName("ExpiraData");
            Property(t => t.ConfirmaDoacao).IsOptional().HasColumnType("datetime2").HasColumnName("ConfirmaDoacao");
            Property(t => t.Comprovante).HasColumnName("Comprovante");
            Property(t => t.AnexoExtensao).HasColumnName("AnexoExtensao");
            Property(t => t.AnexoTipo).HasColumnName("AnexoTipo");
            Property(t => t.DataEnvio).IsOptional().HasColumnType("datetime2").HasColumnName("DataEnvio");
            Property(t => t.Status).HasColumnName("Status");

            // Relationships
            HasRequired(t => t.Cliente)
                .WithMany(t => t.Doacoes)
                .HasForeignKey(d => d.ClienteId);

            //HasRequired(t => t.DadosBancarios)
            //    .WithMany(t=>t.DadosId)
            //    .HasForeignKey(d => d.DoacoesId);
        }
    }
}
