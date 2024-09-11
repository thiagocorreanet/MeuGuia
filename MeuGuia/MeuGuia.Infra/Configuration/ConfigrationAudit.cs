using MeuGuia.Domain.Entitie;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace MeuGuia.Infra.Configuration;

public class ConfigrationAudit : IEntityTypeConfiguration<AuditProcess>
{
    public void Configure(EntityTypeBuilder<AuditProcess> builder)
    {
        builder.ToTable("AuditsProcess");

        builder.HasKey(c => c.Id)
            .HasName("PK_AUDITSPROCESS");

        builder.Property(c => c.Id)
            .HasColumnName("AuditProcessId")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária da auditoria.");

        builder.Property(c => c.Type)
           .HasColumnName("Type")
           .HasColumnOrder(4)
           .IsRequired()
           .HasColumnType("VARCHAR(20)")
           .HasComment("Tipo da auditoria");

        builder.Property(c => c.Table)
           .HasColumnName("Table")
           .HasColumnOrder(5)
           .IsRequired()
           .HasColumnType("VARCHAR(100)")
           .HasComment("Tabela na qual vai ser exibida na auditoria");

        builder.Property(c => c.OldValues)
           .HasColumnName("OldValues")
           .HasColumnOrder(6)
           .HasColumnType("VARCHAR(MAX)")
           .HasComment("Valor antigo");

        builder.Property(c => c.NewValues)
           .HasColumnName("NewValues")
           .HasColumnOrder(7)
           .HasColumnType("VARCHAR(MAX)")
           .HasComment("Novo valor");

        builder.Property(c => c.AffectedColumns)
           .HasColumnName("AffectedColumns")
           .HasColumnOrder(8)
           .HasColumnType("VARCHAR(MAX)")
           .HasComment("Colunas que foram afetadas");

        builder.Property(c => c.PrimaryKey)
           .HasColumnName("PrimaryKey")
           .HasColumnOrder(9)
           .HasColumnType("VARCHAR(50)")
           .HasComment("Primary key da tabela");

        builder.Property(c => c.CreationDate)
           .HasColumnName("CreationDate")
           .HasColumnOrder(10)
           .IsRequired()
           .HasColumnType("DATETIME2")
           .HasDefaultValueSql("GETDATE()")
           .HasComment("Data de criação da empresa")
           .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(c => c.ModificationDate)
            .HasColumnName("ModificationDate")
            .HasColumnOrder(11)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização da empresa")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.HasIndex(c => c.Id)
            .HasDatabaseName("IX_AUDITSPROCESS_USERID")
            .IsUnique();
    }
}
