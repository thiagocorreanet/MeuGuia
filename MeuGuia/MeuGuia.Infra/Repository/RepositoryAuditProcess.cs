using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Infra.Context;

namespace MeuGuia.Infra.Repository;

public class RepositoryAuditProcess : RepositoryBase<AuditProcess>, IRepositoryAudit
{
    public RepositoryAuditProcess(MeuGuiaContext context) : base(context)
    {
    }
}
