using MediatR;

namespace MeuGuia.Application.Queries.Permission.GetById;

public class QueryPermissionGetByIdRequest : IRequest<QueryPermissionGetByIdResponse>
{
    public QueryPermissionGetByIdRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}

public class QueryPermissionGetByIdResponse
{
    public QueryPermissionGetByIdResponseItem Item { get; set; } = null!;
}
public class QueryPermissionGetByIdResponseItem
{
    public int Id { get; set; }
    public Guid Token { get; set; }
    public int AspNetUserClaimId { get; set; }
    public required string UserId { get; set; }
    public required string Page { get; set; }
    public required string Role { get; set; }
}