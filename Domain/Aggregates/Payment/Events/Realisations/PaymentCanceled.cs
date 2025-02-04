using Domain.Events.Abstractions;

namespace Domain.Events.Realisations;

public class PaymentCanceled : PaymentEvent
{
    public PaymentCanceled(Guid changedEntityId) : base(changedEntityId)
    {
    }
}