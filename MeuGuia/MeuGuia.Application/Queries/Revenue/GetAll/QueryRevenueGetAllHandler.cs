
using AutoMapper;
using MediatR;
using MeuGuia.Application.Helper;
using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Queries.Revenue.GetAll;

public class QueryRevenueGetAllHandler : CreateBaseCommand, IRequestHandler<QueryRevenueGetAllRequest, QueryRevenueGetAllResponse>
{
    private readonly IRepositoryRevenue _iRepositoryRevenue;

    public QueryRevenueGetAllHandler(INotificationError notificationError, IMapper iMapper, IRepositoryRevenue iRepositoryRevenue) : base(notificationError, iMapper)
    {
        _iRepositoryRevenue = iRepositoryRevenue;
    }

    public async Task<QueryRevenueGetAllResponse> Handle(QueryRevenueGetAllRequest request, CancellationToken cancellationToken)
    {
        var result = await _iRepositoryRevenue.GetAllAsync();
        var responseItems = await MappingList<QueryRevenueGetAllResponseItems>(result);

        return new QueryRevenueGetAllResponse()
        {
            Items = responseItems,
            Total = responseItems.Sum(x => x.Value).ToString("C2")
        };
    }

    
}
