namespace Vets.Registration;

public partial class Veterinarian
{
    public void Apply(VeterinarianRegistered e)
    {
        Name = e.Name;
        Surname = e.Surname;
        BirthDate = e.BirthDate;
    }
}

public partial class Service
{
    public async Task<Result> HandleAsync(RegisterVeterinarian c)
    {
        var streamId = new AggregateStreamId(Category);
        var (name, surname, birthDate) = c;
        var @event = new VeterinarianRegistered(name, surname, birthDate);

        return await _store.PushAsync(streamId, @event);
    }
}

public record RegisterVeterinarian(
    string Name,
    string Surname,
    DateTime BirthDate
);

public record VeterinarianRegistered(
    string Name,
    string Surname,
    DateTime BirthDate
) : Event;
