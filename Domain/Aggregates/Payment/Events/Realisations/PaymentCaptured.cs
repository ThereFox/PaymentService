using Domain.Events.Abstractions;

namespace Domain.Events.Realisations;

public class PaymentCaptured : PaymentEvent
{
    public decimal Amount { get; init; }
    
    public PaymentCaptured(Guid changedEntityId, decimal amount) : base(changedEntityId)
    {
        Amount = amount;
    }
}