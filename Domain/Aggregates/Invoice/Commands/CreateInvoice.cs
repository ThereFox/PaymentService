using Domain.ValueObjects;

namespace Domain.Aggregates.Invoice.Commands;

public class CreateInvoice
{
    public List<InvoiceLineItem> Items { get; set; }
    public Guid OrderId { get; private set; }
}