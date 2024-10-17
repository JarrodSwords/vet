using EventStore.Client;

namespace Vets.Registration.EventStoreDb;

public class VeterinarianRepository
{
    private readonly EventStoreClient _client;

    public VeterinarianRepository(EventStoreClient client)
    {
        _client = client;
    }

    /*
     * todo: figure out category vs entity streams in eventstoredb.
     *
     * i don't think it's supported. but it may be okay to subscribe to all,
     * assuming the stores are segregated
     */
    public async Task<Veterinarian> FindAsync(AggregateStreamId streamId)
    {
        var stream = _client.FindAggregate(streamId);

        var builder = await new VeterinarianBuilder()
            .Load(stream)
            .BuildAsync();

        return builder.GetVeterinarian();
    }
}
