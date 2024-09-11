namespace MeuGuia.Domain.Entitie;

public abstract class Base
{
    protected Base()
    {
        CreationDate = DateTime.Now;
        ModificationDate = DateTime.Now;
    }

    public int Id { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime ModificationDate { get; private set; }
}
