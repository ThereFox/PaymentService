using Domain.Events.Abstractions;

namespace Domain.Events.Realisations;

public class PaymentVerifyed : PaymentEvent
{
    public PaymentVerifyed(Guid changedEntityId) : base(changedEntityId)
    {
    }
}