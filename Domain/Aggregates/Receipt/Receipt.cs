using Domain.Events.Abstractions;
using Domain.ValueObjects;

namespace Domain.Aggregates.Receipt;

public class Receipt
{
    private List<ShiftLineItem> _items;
    private List<DomainEvent> _events;

    public Guid? Id { get; set; }
    public Guid? OrderId { get; set; }
    public Guid? PaymentId { get; set; }
    public IReadOnlyList<ShiftLineItem> Items => _items;
    public IReadOnlyList<DomainEvent> Events => _events;
}