using MeuGuia.Domain.Audit;
using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Enum;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MeuGuia.Infra.Context;

public class MeuGuiaContext : IdentityDbContext<IdentityUserCustom, IdentityRole, string, IdentityUserClaimCustom, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public MeuGuiaContext(DbContextOptions options) : base(options) { }

    public DbSet<AuditProcess> AuditsProcess => Set<AuditProcess>();
    public DbSet<Revenue> Revenues => Set<Revenue>();
    public DbSet<Permission> Permissions => Set<Permission>();

    /// <summary>
    /// Sobrescreve o método de configuração do modelo para aplicar configurações específicas do contexto.
    /// </summary>
    /// <param name="modelBuilder">Construtor do modelo.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuGuiaContext).Assembly);
    }


    /// <summary>
    /// Este método é chamado antes de salvar as alterações no banco de dados.
    /// Ele rastreia todas as alterações feitas nas entidades e registra essas alterações para fins de auditoria.
    /// </summary>
    /// <param name="userId">O ID do usuário que está fazendo as alterações.</param>
    public void OnBeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();
        var auditEntries = GetAuditEntries();
        foreach (var auditEntry in auditEntries)
        {
            AuditsProcess.Add(auditEntry.ToAudit());
        }
    }

    /// <summary>
    /// Este método obtém todas as entradas de auditoria para as alterações detectadas nas entidades.
    /// </summary>
    /// <param name="userId">O ID do usuário que está fazendo as alterações.</param>
    /// <returns>Uma lista de entradas de auditoria para as alterações detectadas.</returns>
    private List<AuditEntry> GetAuditEntries()
    {
        var auditEntries = new List<AuditEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (ShouldSkip(entry))
                continue;

            var auditEntry = CreateAuditEntry(entry);
            auditEntries.Add(auditEntry);
        }

        return auditEntries;
    }

    /// <summary>
    /// Este método verifica se uma entidade deve ser ignorada durante a auditoria.
    /// </summary>
    /// <param name="entry">A entidade a ser verificada.</param>
    /// <returns>Retorna verdadeiro se a entidade for do tipo Audit ou se seu estado for Detached ou Unchanged. Caso contrário, retorna falso.</returns>
    private bool ShouldSkip(EntityEntry entry)
    {
        return entry.Entity is AuditProcess || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged;
    }

    /// <summary>
    /// Este método cria uma entrada de auditoria para uma entidade.
    /// </summary>
    /// <param name="entry">A entidade para a qual a entrada de auditoria será criada.</param>
    /// <param name="userId">O ID do usuário que está fazendo as alterações.</param>
    /// <returns>Uma entrada de auditoria que registra as alterações feitas na entidade.</returns>
    private AuditEntry CreateAuditEntry(EntityEntry entry)
    {
        var auditEntry = new AuditEntry(entry)
        {
            TableName = entry.Entity.GetType().Name,
        };

        foreach (var property in entry.Properties)
        {
            HandlePropertyChanges(entry, auditEntry, property);
        }

        return auditEntry;
    }

    /// <summary>
    /// Este método manipula as alterações feitas em uma propriedade de uma entidade.
    /// </summary>
    /// <param name="entry">A entidade cujas alterações estão sendo manipuladas.</param>
    /// <param name="auditEntry">A entrada de auditoria na qual as alterações estão sendo registradas.</param>
    /// <param name="property">A propriedade que foi alterada.</param>
    private void HandlePropertyChanges(EntityEntry entry, AuditEntry auditEntry, PropertyEntry property)
    {
        string propertyName = property.Metadata.Name;
        if (property.Metadata.IsPrimaryKey())
        {
            auditEntry.KeyValues[propertyName] = property.CurrentValue;
            return;
        }

        switch (entry.State)
        {
            case EntityState.Added:
                HandleAddedState(auditEntry, property, propertyName);
                break;

            case EntityState.Deleted:
                HandleDeletedState(auditEntry, property, propertyName);
                break;

            case EntityState.Modified:
                HandleModifiedState(entry, auditEntry, property, propertyName);
                break;
        }
    }

    /// <summary>
    /// Este método manipula as alterações feitas em uma propriedade de uma entidade que foi adicionada.
    /// </summary>
    /// <param name="auditEntry">A entrada de auditoria na qual as alterações estão sendo registradas.</param>
    /// <param name="property">A propriedade que foi alterada.</param>
    /// <param name="propertyName">O nome da propriedade que foi alterada.</param>
    private void HandleAddedState(AuditEntry auditEntry, PropertyEntry property, string propertyName)
    {
        auditEntry.EAuditType = EAuditType.Create;
        auditEntry.NewValues[propertyName] = property.CurrentValue;
    }

    /// <summary>
    /// Este método manipula as alterações feitas em uma propriedade de uma entidade que foi excluída.
    /// </summary>
    /// <param name="auditEntry">A entrada de auditoria na qual as alterações estão sendo registradas.</param>
    /// <param name="property">A propriedade que foi alterada.</param>
    /// <param name="propertyName">O nome da propriedade que foi alterada.</param>
    private void HandleDeletedState(AuditEntry auditEntry, PropertyEntry property, string propertyName)
    {
        auditEntry.EAuditType = EAuditType.Delete;
        auditEntry.OldValues[propertyName] = property.OriginalValue;
    }

    /// <summary>
    /// Este método manipula as alterações feitas em uma propriedade de uma entidade que foi modificada.
    /// </summary>
    /// <param name="entry">A entidade cujas alterações estão sendo manipuladas.</param>
    /// <param name="auditEntry">A entrada de auditoria na qual as alterações estão sendo registradas.</param>
    /// <param name="property">A propriedade que foi alterada.</param>
    /// <param name="propertyName">O nome da propriedade que foi alterada.</param>
    private void HandleModifiedState(EntityEntry entry, AuditEntry auditEntry, PropertyEntry property, string propertyName)
    {
        if (property.IsModified)
        {
            var originalValues = entry.GetDatabaseValues();
            auditEntry.ChangedColumns.Add(propertyName);
            auditEntry.EAuditType = EAuditType.Update;
            auditEntry.OldValues[propertyName] = originalValues.GetValue<object>(propertyName);
            auditEntry.NewValues[propertyName] = property.CurrentValue;
        }
    }
}
