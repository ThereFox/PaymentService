using Domain.Events.Abstractions;
using Domain.ValueObjects;

namespace Domain.Events.Realisations;

public class PaymentCreated : PaymentEvent
{
    public PaymentType Type { get; init; }
    public PaymentAmount Amount { get; init; }
    
    public PaymentCreated(Guid changedEntityId, PaymentType type, PaymentAmount amount) : base(changedEntityId, DateTime.UtcNow)
    {
        Type = type;
        Amount = amount;
    }
    public PaymentCreated(Guid changedEntityId, DateTime happenDateTime, PaymentType type, PaymentAmount amount) : base(changedEntityId, happenDateTime)
    {
        Type = type;
        Amount = amount;
    }
}