using Domain.Events.Abstractions;

namespace Domain.Events.Realisations;

public class PaymentVerifyed : PaymentEvent
{
    public PaymentVerifyed(Guid changedEntityId) : base(changedEntityId, DateTime.UtcNow)
    {
    }
    public PaymentVerifyed(Guid changedEntityId, DateTime happenDateTime) : base(changedEntityId, happenDateTime)
    {
    }
}