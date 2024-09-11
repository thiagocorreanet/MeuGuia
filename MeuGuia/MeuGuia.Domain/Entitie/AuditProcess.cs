namespace MeuGuia.Domain.Entitie;

public class AuditProcess : Base
{
    public AuditProcess() 
    {
    }

    public string Type { get; set; } = null!;
    public string Table { get; set; } = null!;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? AffectedColumns { get; set; }
    public string? PrimaryKey { get; set; }
}
