using MediatR;

using MeuGuia.Domain.Entitie;


namespace MeuGuia.Application.Commands.Claim.Create;

public class CreateClaimCommandRequest : IRequest<IdentityUserClaimCustom>
{
    public required string Email { get; set; }
    public required string ClaimType { get; set; }
    public required string ClaimValue { get; set; }
}

