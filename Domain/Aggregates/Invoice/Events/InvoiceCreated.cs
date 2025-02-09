using Domain.Events.Abstractions;
using Domain.ValueObjects;

namespace Domain.Aggregates.Invoice.Events;

public class InvoiceCreated : DomainEvent
{
    public Guid OrderId { get; set; }
    public IReadOnlyList<InvoiceLineItem> Items { get; set; }

    public InvoiceCreated(Guid changedEntityId, Guid orderId, List<InvoiceLineItem> items) : base(changedEntityId)
    {
        OrderId = orderId;
        Items = items;
    }
}