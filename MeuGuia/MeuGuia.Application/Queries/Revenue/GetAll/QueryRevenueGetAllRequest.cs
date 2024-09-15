
using MediatR;

namespace MeuGuia.Application.Queries.Revenue.GetAll;

public class QueryRevenueGetAllRequest : IRequest<QueryRevenueGetAllResponse>
{ }

public class QueryRevenueGetAllResponse
{
    public IEnumerable<QueryRevenueGetAllResponseItems> Items {get; set;} = null!;
    public string Total { get; set; } = null!;
}

public class QueryRevenueGetAllResponseItems
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public double Value { get; set; }
}
