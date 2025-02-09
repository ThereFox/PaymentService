using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class ReceiptState : ValueObject
{
    public static ReceiptState Initial => new ReceiptState(-1);
    public static ReceiptState Precalculated => new ReceiptState(1);
    public static ReceiptState Wait_Approvement => new ReceiptState(2);
    public static ReceiptState Approved => new ReceiptState(3);
    public static ReceiptState Sended => new ReceiptState(4);
    public static ReceiptState Rejected => new ReceiptState(5);


    public int Id { get; private set; }

    private ReceiptState(int id)
    {
        Id = id;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}