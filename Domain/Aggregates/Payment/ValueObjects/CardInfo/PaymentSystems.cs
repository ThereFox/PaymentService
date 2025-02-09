using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class PaymentSystems : ValueObject
{
    public static PaymentSystems Mir => new PaymentSystems(2);
    public static PaymentSystems Visa => new PaymentSystems(4);
    public static PaymentSystems MasterCard => new PaymentSystems(5);

    public int Id { get; init; }

    private PaymentSystems(int id)
    {
        Id = id;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}