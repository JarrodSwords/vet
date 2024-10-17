namespace Vets.Registration;

public abstract record Event(Guid Id)
{
    protected Event(Guid? id = default) : this(id ?? NewGuid())
    {
    }
}
