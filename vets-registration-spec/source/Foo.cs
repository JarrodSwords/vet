using System.Text;
using System.Text.Json;
using EventStore.Client;
using Jgs.Errors.Results;

namespace Vets.Registration.Spec;

public class Foo
{
    #region Setup

    private readonly Dictionary<Type, Func<object, Result>> _handlers = new();

    public Foo()
    {
        Add<VeterinarianRegistered>(Apply);
    }

    #endregion

    #region Implementation

    private void Add<T>(Func<T, Result> handler)
    {
        _handlers.Add(typeof(T), x => handler((T) x));
    }

    public Result Apply(VeterinarianRegistered e) => Result.Success();

    #endregion

    #region Requirements

    [Fact]
    public void Bar()
    {
        //const string connectionString = "esdb://admin:changeit@localhost:2113?tls=false&tlsVerifyCert=false";

        //var settings = EventStoreClientSettings.Create(connectionString);

        //var client = new EventStoreClient(settings);

        var @event = new VeterinarianRegistered("Jon", "Doe", new DateTime(2000, 1, 1));

        var jsonEvent = JsonSerializer.SerializeToUtf8Bytes(@event);

        var e1 = new EventData(
            Uuid.FromGuid(Guid.NewGuid()),
            @event.GetType().Name,
            jsonEvent
        );

        var str = Encoding.UTF8.GetString(jsonEvent);

        var type = typeof(VeterinarianRegistered).Assembly.GetTypes().Single(x => x.Name == e1.Type);

        var bar = JsonSerializer.Deserialize(str, type)!;

        if (_handlers.ContainsKey(type))
            _handlers[type](bar);

        var x = 10;
    }

    #endregion
}
