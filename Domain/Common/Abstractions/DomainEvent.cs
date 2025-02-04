namespace Domain.Events.Abstractions;

public abstract class DomainEvent
{
    public Guid ChangedEntityId { get; init; }

    public DomainEvent(Guid changedEntityId)
    {
        ChangedEntityId = changedEntityId;
    }
}