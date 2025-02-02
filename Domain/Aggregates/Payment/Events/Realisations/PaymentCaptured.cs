using Domain.Events.Abstractions;

namespace Domain.Events.Realisations;

public class PaymentCaptured : PaymentEvent
{
    public PaymentCaptured(Guid changedEntityId) : base(changedEntityId, DateTime.UtcNow)
    {
    }
    public PaymentCaptured(Guid changedEntityId, DateTime happenDateTime) : base(changedEntityId, happenDateTime)
    {
    }
}