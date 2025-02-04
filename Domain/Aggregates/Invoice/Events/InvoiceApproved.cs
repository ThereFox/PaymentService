using Domain.Events.Abstractions;

namespace Domain.Aggregates.Invoice.Events;

public class InvoiceApproved : DomainEvent
{
    public InvoiceApproved(Guid changedEntityId) : base(changedEntityId)
    {
    }
}