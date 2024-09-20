using MediatR;

namespace MeuGuia.Application.Commands.Permission.Update;

public class UpdatePermissionCommandRequest : IRequest<bool>
{
    public int Id { get; set; } 
    public Guid Token { get; set; } 
    public int AspNetUserClaimId { get; set; }
    public required string UserId { get; set; }
    public required string Page { get; set; }
    public required string Role { get; set; }
}
