namespace MeuGuia.Domain.Entitie;

public sealed class Revenue : Base
{
    public Revenue(string description, decimal value)
    {
        Description = description;
        Value = value;
    }

    public string Description { get; private set; } = null!;
    public decimal Value { get; private set; }
}
