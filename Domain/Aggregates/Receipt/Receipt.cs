using Domain.ValueObjects;

namespace Domain.Aggregates.Receipt;

public class Receipt
{
    private List<ShiftLineItem> _items;
    
    public Guid? Id { get; set; }
    public Guid? OrderId { get; set; }
    public Guid? PaymentId { get; set; }
    public IReadOnlyList<ShiftLineItem> Items => _items; 
    
}