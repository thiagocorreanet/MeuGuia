using MediatR;

using System.Text.Json.Serialization;

namespace MeuGuia.Application.Commands.Permission.Create;

public class CreatePermissionCommandRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid Token { get; set; } = Guid.NewGuid();

    [JsonIgnore]
    public int? AspNetUserClaimId { get; set; }

    [JsonIgnore]
    public string? UserId { get; set; }

    public required string Email { get;  set; } 
    public required string Page { get;  set; } 
    public required string Role { get;  set; } 
}
