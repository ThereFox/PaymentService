using Domain.ValueObjects;

namespace Domain.Aggregates.Invoice.Commands;

public class CreateInvoice
{
    public List<InvoiceLineItem> Items { get; init; }
    public Guid OrderId { get; init; }
}