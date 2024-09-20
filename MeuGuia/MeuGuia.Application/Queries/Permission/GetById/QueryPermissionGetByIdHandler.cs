using AutoMapper;

using MediatR;

using MeuGuia.Application.Helper;
using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Queries.Permission.GetById;

public class QueryPermissionGetByIdHandler : CreateBaseCommand, IRequestHandler<QueryPermissionGetByIdRequest, QueryPermissionGetByIdResponse>
{

    private readonly IRepositoryPermission _iRepositoryPermission;

    public QueryPermissionGetByIdHandler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryPermission iRepositoryPermission) : base(notificationError, iMapper)
    {
        _iRepositoryPermission = iRepositoryPermission;
    }

    public async Task<QueryPermissionGetByIdResponse> Handle(QueryPermissionGetByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _iRepositoryPermission.GetByIdAsync(request.Id);
        var item = await SimpleMapping<QueryPermissionGetByIdResponseItem>(result);
        return new QueryPermissionGetByIdResponse
        {
            Item = item
        };
    }
}
