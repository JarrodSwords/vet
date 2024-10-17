using System.Text;
using System.Text.Json;
using EventStore.Client;
using Jgs.Errors;
using Jgs.Errors.Results;

namespace Vets.Registration.EventStoreDb;

public static class ResolvedEventExtensions
{
    public static readonly Type[] MessageTypes = typeof(Veterinarian).Assembly.GetTypes();

    public static Result<object> Parse(this ResolvedEvent source)
    {
        var type = MessageTypes.SingleOrDefault(x => string.CompareOrdinal(x.Name, source.Event.EventType) == 0);

        if (type is null)
            return new Error("message.not.supported");

        var payload = Encoding.UTF8.GetString(source.Event.Data.ToArray());

        return JsonSerializer.Deserialize(payload, type)!;
    }
}
