namespace Domain.Events.Abstractions;

public class PaymentEvent : DomainEvent
{
    public PaymentEvent(Guid changedEntityId, DateTime happenDateTime) : base(changedEntityId, happenDateTime)
    {
    }
}