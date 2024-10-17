using System.Text.Json;
using EventStore.Client;
using Jgs.Errors;
using Jgs.Errors.Results;

namespace Vets.Registration.EventStoreDb;

public class MessageStore : IMessageStore
{
    private readonly EventStoreClient _client;

    public MessageStore(EventStoreClient client)
    {
        _client = client;
    }

    public async Task<Result> PushAsync(
        AggregateStreamId streamId,
        Event @event,
        ulong revision = default
    )
    {
        var ed = new EventData(
            Uuid.FromGuid(@event.Id),
            @event.GetType().Name,
            JsonSerializer.SerializeToUtf8Bytes(@event)
        );

        try
        {
            var result = await _client.AppendToStreamAsync(streamId, revision, new[] { ed });

            return Success();
        }
        catch (Exception e)
        {
            return new Error("whoops");
        }
    }
}
