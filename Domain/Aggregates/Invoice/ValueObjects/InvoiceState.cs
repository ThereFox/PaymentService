using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class InvoiceState : ValueObject
{
    public static InvoiceState Initial => new InvoiceState(-1);
    public static InvoiceState Created => new InvoiceState(1);
    public static InvoiceState Approved => new InvoiceState(2);
    public static InvoiceState Rejected => new InvoiceState(3);

    public int Id { get; init; }

    public InvoiceState(int id)
    {
        Id = id;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}