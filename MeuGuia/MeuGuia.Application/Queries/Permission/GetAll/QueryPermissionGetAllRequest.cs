using MediatR;

namespace MeuGuia.Application.Queries.Permission.GetAll;

public class QueryPermissionGetAllRequest : IRequest<QueryPermissionGetAllResponse>
{ }

public class QueryPermissionGetAllResponse
{
    public IEnumerable<QueryPermissionGetAllResponseItems> Items { get; set; } = null!;
}

public class QueryPermissionGetAllResponseItems
{
    public int Id { get; set; }
    public Guid Token { get; set; }
    public int AspNetUserClaimId { get; set; }
    public required string UserId { get; set; }
    public required string Page { get; set; }
    public required string Role { get; set; }
}
