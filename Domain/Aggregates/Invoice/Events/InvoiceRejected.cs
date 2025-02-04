using Domain.Events.Abstractions;

namespace Domain.Aggregates.Invoice.Events;

public class InvoiceRejected : DomainEvent
{
    public InvoiceRejected(Guid changedEntityId) : base(changedEntityId)
    {
    }
}