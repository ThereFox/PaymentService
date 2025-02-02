using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class PaymentType : ValueObject
{
    public static PaymentType Captured => new PaymentType(1);
    public static PaymentType TwoState => new PaymentType(2);
    
    public int Id { get; protected set; }

    private PaymentType(int id)
    {
        Id = id;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}