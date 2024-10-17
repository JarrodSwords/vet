namespace Vets.Registration;

public interface IMessageStore
{
    Task<Result> PushAsync(AggregateStreamId streamId, Event @event, ulong revision = default);
}
