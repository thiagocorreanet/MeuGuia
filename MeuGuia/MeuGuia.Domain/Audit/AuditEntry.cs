using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Enum;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace MeuGuia.Domain.Audit;

public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }

    public EntityEntry Entry { get; }
    public string CreatedUserId { get; set; }
    public string ModificationUserId { get; set; }
    public string TableName { get; set; }
    public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
    public EAuditType EAuditType { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();
    public AuditProcess ToAudit()
    {
        // Passando variváel null para funcionar com a entity recebendo os parametros da base
        var audit = new AuditProcess();
        audit.Type = EAuditType.ToString();
        audit.Table = TableName;
        audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
        audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
        audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
        audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
        return audit;
    }
}
