namespace Vets.Registration;

public partial class Veterinarian
{
    private readonly Dictionary<Type, Action<object>> _handlers = new();

    private Veterinarian()
    {
        Name = string.Empty;
        Surname = string.Empty;

        RegisterEventHandlers();
    }

    public void Apply(object e)
    {
        if (_handlers.TryGetValue(e.GetType(), out var handler))
            handler(e);
    }

    private void Register<T>(Action<object> handler)
    {
        _handlers.TryAdd(typeof(T), x => handler((T) x));
    }

    private void RegisterEventHandlers()
    {
        Register<VeterinarianRegistered>(Apply);
    }

    public abstract class Builder
    {
        protected Veterinarian Vet;

        protected Builder()
        {
            Vet = new();
        }
    }
}
