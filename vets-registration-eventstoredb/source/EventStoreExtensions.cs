using EventStore.Client;

namespace Vets.Registration.EventStoreDb;

public static class EventStoreExtensions
{
    public static EventStoreClient.ReadStreamResult FindAggregate(this EventStoreClient source, AggregateStreamId id) =>
        source.ReadStreamAsync(
            Direction.Forwards,
            id,
            StreamPosition.Start
        );
}
