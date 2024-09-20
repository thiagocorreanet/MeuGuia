using MeuGuia.Domain.Entitie;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuGuia.Infra.Configuration;

public class ConfigurationPermission : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(c => c.Id)
            .HasName("PK_PERMISSIONS");

        builder.Property(c => c.Id)
            .HasColumnName("PermissionId")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária da permissão.");

        builder.Property(c => c.Token)
            .HasColumnName(nameof(Permission.Token))
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("VARCHAR(36)")
            .HasComment("Token da tabela");

        builder.Property(c => c.AspNetUserClaimId)
           .HasColumnName(nameof(Permission.AspNetUserClaimId))
           .HasColumnOrder(3)
           .IsRequired()
           .HasColumnType("INT")
           .HasComment("Chave primária da tabela AspNetUserClaims");

        builder.Property(c => c.UserId)
            .HasColumnName(nameof(Permission.UserId))
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("VARCHAR(36)")
            .HasComment("Chave do usuário.");

        builder.Property(c => c.Page)
            .HasColumnName(nameof(Permission.Page))
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("VARCHAR(50)")
            .HasComment("Nome da página que o usuário terá acesso.");

        builder.Property(c => c.Role)
            .HasColumnName(nameof(Permission.Role))
            .HasColumnOrder(6)
            .IsRequired()
            .HasColumnType("VARCHAR(50)")
            .HasComment("Nome da permissão do usuário.");

        builder.Property(c => c.CreationDate)
            .HasColumnName(nameof(Permission.CreationDate))
            .HasColumnOrder(7)
            .IsRequired()
            .HasColumnType("DATETIME2")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação do registro")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(c => c.ModificationDate)
            .HasColumnName(nameof(Permission.ModificationDate))
            .HasColumnOrder(8)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização do registro")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.HasIndex(c => c.Id)
            .HasDatabaseName("IX_PERMISSION_ID")
            .IsUnique();
    }
}
