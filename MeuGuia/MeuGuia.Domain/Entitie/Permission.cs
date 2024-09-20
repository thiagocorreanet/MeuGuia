namespace MeuGuia.Domain.Entitie;

public sealed class Permission : Base
{
    public Guid Token { get; private set; }
    public int AspNetUserClaimId { get; private set; }
    public string UserId { get; private set; } = null!;
    public string Page { get; private set; } = null!;
    public string Role { get; private set; } = null!;
}
