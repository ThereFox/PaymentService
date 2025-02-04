using Domain.Events.Abstractions;

namespace Domain.Events.Realisations;

public class PaymentInitialised : PaymentEvent
{
    public Guid OrderId { get; init; }
    
    public PaymentInitialised(Guid orderId, Guid changedEntityId) : base(changedEntityId)
    {
        OrderId = orderId;
    }
    
}