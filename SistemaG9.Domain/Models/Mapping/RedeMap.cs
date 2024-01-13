using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class RedeMap : EntityTypeConfiguration<Rede>
    {
        public RedeMap()
        {
            // Primary Key
            HasKey(t => t.RedeId);

            // Properties
            Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(150);

            Property(t => t.Apelido)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.RecebedorId)
                .IsRequired();

            Property(t => t.ApelidoRecebedor)
                //.IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Rede");
            Property(t => t.RedeId).HasColumnName("RedeId");
            Property(t => t.MatrizId).HasColumnName("MatrizId");
            Property(t => t.Nivel).HasColumnName("Nivel");
            Property(t => t.ClienteId).HasColumnName("ClienteId");
            Property(t => t.Login).HasColumnName("Login");
            Property(t => t.Apelido).HasColumnName("Apelido");
            Property(t => t.RecebedorId).HasColumnName("RecebedorId");
            Property(t => t.ApelidoRecebedor).HasColumnName("ApelidoRecebedor");
            Property(t => t.Posicao).HasColumnName("Posicao");
            Property(t => t.Status).HasColumnName("Status");

            // Relationships
            HasRequired(t => t.Cliente)
                .WithMany(t => t.Redes)
                .HasForeignKey(d => d.ClienteId);

            HasRequired(t => t.Patrocinador)
                .WithMany(t => t.Redes1)
                .HasForeignKey(d => d.ClienteId);
            //this.HasRequired(t => t.Matriz)
            //    .WithMany(t => t.Redes)
            //    .HasForeignKey(d => d.MatrizId);

        }
    }
}
