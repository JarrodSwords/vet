using EventStore.Client;

namespace Vets.Registration.EventStoreDb;

public class VeterinarianBuilder : Veterinarian.Builder
{
    private IAsyncEnumerable<ResolvedEvent>? _stream;

    public async Task<VeterinarianBuilder> BuildAsync()
    {
        await foreach (var e in _stream)
            e.Parse().Then(Vet.Apply);

        return this;
    }

    public Veterinarian GetVeterinarian() => Vet;

    public VeterinarianBuilder Load(IAsyncEnumerable<ResolvedEvent> events)
    {
        _stream = events;
        return this;
    }
}
