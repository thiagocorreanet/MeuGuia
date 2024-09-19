
using AutoMapper;
using MediatR;
using MeuGuia.Application.Helper;
using MeuGuia.Application.Queries.Revenue.GetById;
using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Queries.Revenue.GetAll;

public class QueryRevenueGetByIdHendler : CreateBaseCommand, IRequestHandler<QueryRevenueGetByIdRequest, QueryRevenueGetByIdResponse>
{
    private readonly IRepositoryRevenue _iRepositoryRevenue;

    public QueryRevenueGetByIdHendler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryRevenue iRepositoryRevenue) : base(notificationError, iMapper, helperIdentity)
    {
        _iRepositoryRevenue = iRepositoryRevenue;
    }

    public async Task<QueryRevenueGetByIdResponse> Handle(QueryRevenueGetByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _iRepositoryRevenue.GetByIdAsync(request.Id);
        var item = await SimpleMapping<QueryRevenueGetByIdResponseItem>(result);
        return new QueryRevenueGetByIdResponse
        {
            Item = item
        };
    }
}
