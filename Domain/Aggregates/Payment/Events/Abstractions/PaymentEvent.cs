namespace Domain.Events.Abstractions;

public class PaymentEvent : DomainEvent
{
    public PaymentEvent(Guid changedEntityId) : base(changedEntityId)
    {
    }
}