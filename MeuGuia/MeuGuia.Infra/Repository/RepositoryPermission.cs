using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Infra.Context;

namespace MeuGuia.Infra.Repository;

public class RepositoryPermission : RepositoryBase<Permission>, IRepositoryPermission
{
    public RepositoryPermission(MeuGuiaContext context) : base(context)
    {
    }
}
