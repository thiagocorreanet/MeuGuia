using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Infra.Context;

namespace MeuGuia.Infra.Repository;

public class RepositoryRevenue : RepositoryBase<Revenue>, IRepositoryRevenue
{
    public RepositoryRevenue(MeuGuiaContext context) : base(context)
    {
    }
}
