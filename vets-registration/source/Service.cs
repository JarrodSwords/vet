namespace Vets.Registration;

public partial class Service
{
    private const string Category = "veterinarian-registration";
    private readonly Dictionary<Type, Func<object, Task<Result>>> _handlers = new();
    private readonly IMessageStore _store;

    public Service(IMessageStore store)
    {
        _store = store;

        RegisterCommandHandlers();
    }

    public async Task<Result> HandleAsync(object command)
    {
        if (_handlers.TryGetValue(command.GetType(), out var handler))
            return await handler(command);

        return Success(); // no handler? no problem
    }

    private void Register<T>(Func<object, Task<Result>> handler)
    {
        _handlers.TryAdd(typeof(T), command => handler((T) command));
    }

    private void RegisterCommandHandlers()
    {
        Register<RegisterVeterinarian>(HandleAsync);
    }
}
