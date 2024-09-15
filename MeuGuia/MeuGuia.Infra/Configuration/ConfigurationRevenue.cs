using MeuGuia.Domain.Entitie;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuGuia.Infra.Configuration;

public class ConfigurationRevenue : IEntityTypeConfiguration<Revenue>
{
    public void Configure(EntityTypeBuilder<Revenue> builder)
    {
        builder.ToTable("Revenues");

        builder.HasKey(c => c.Id)
            .HasName("PK_REVENUES");

        builder.Property(c => c.Id)
            .HasColumnName("RevenueId")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária da receita.");

        builder.Property(c => c.Description)
          .HasColumnName(nameof(Revenue.Description))
          .HasColumnOrder(2)
          .IsRequired()
          .HasColumnType("VARCHAR(50)")
          .HasComment("Descrição da receita.");

        builder.Property(c => c.Value)
         .HasColumnName(nameof(Revenue.Value))
         .HasColumnOrder(3)
         .IsRequired()
         .HasColumnType("DECIMAL(18, 2)")
         .HasComment("Valor da receita.");

        builder.Property(c => c.CreationDate)
          .HasColumnName(nameof(Revenue.CreationDate))
          .HasColumnOrder(4)
          .IsRequired()
          .HasColumnType("DATETIME2")
          .HasDefaultValueSql("GETDATE()")
          .HasComment("Data de criação do registro")
          .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(c => c.ModificationDate)
            .HasColumnName(nameof(Revenue.ModificationDate))
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização do registro")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.HasIndex(c => c.Id)
            .HasDatabaseName("IX_REVENUE_ID")
            .IsUnique();
    }
}
