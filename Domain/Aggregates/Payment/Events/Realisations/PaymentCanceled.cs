using Domain.Events.Abstractions;

namespace Domain.Events.Realisations;

public class PaymentCanceled : PaymentEvent
{
    public PaymentCanceled(Guid changedEntityId) : base(changedEntityId, DateTime.UtcNow)
    {
    }
    
    public PaymentCanceled(Guid changedEntityId, DateTime happenDateTime) : base(changedEntityId, happenDateTime)
    {
    }
}