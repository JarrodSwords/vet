namespace Vets.Registration;

public record AggregateStreamId(Guid AggregateId, string Category)
{
    public AggregateStreamId(
        string category,
        Guid? aggregateId = null
    ) : this(
        aggregateId ?? NewGuid(),
        category
    )
    {
    }

    public static implicit operator string(AggregateStreamId source) => $"{source.Category}-{source.AggregateId}";
}
