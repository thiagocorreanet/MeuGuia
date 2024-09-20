using AutoMapper;

using MediatR;

using MeuGuia.Application.Helper;
using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Queries.Permission.GetAll;

public class QueryPermissionGetAllHandler : CreateBaseCommand, IRequestHandler<QueryPermissionGetAllRequest, QueryPermissionGetAllResponse>
{
    private readonly IRepositoryPermission _iRepositoryPermission;

    public QueryPermissionGetAllHandler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryPermission iRepositoryPermission) : base(notificationError, iMapper)
    {
        _iRepositoryPermission = iRepositoryPermission;
    }

    public async Task<QueryPermissionGetAllResponse> Handle(QueryPermissionGetAllRequest request, CancellationToken cancellationToken)
    {
        var result = await _iRepositoryPermission.GetAllAsync();
        var responseItems = await MappingList<QueryPermissionGetAllResponseItems>(result);

        return new QueryPermissionGetAllResponse()
        {
            Items = responseItems
        };
    }
}
