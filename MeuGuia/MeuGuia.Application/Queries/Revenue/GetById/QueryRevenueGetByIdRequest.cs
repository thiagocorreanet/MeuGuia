using System;
using MediatR;

namespace MeuGuia.Application.Queries.Revenue.GetById;

public class QueryRevenueGetByIdRequest : IRequest<QueryRevenueGetByIdResponse>
{
    public QueryRevenueGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}

public class QueryRevenueGetByIdResponse
{
    public QueryRevenueGetByIdResponseItem Item { get; set; } = null!;
}

public class QueryRevenueGetByIdResponseItem
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public double Value { get; set; }
}
