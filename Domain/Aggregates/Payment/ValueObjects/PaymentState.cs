using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class PaymentState : ValueObject
{
    public static PaymentState Initial => new PaymentState(0);
    public static PaymentState Created => new PaymentState(1);
    public static PaymentState Wait_Pending => new PaymentState(2);
    public static PaymentState Wait_Capture => new PaymentState(3);
    public static PaymentState Commited => new PaymentState(4);
    public static PaymentState Canceled => new PaymentState(5);
    public static PaymentState Rejected => new PaymentState(6);
    public static PaymentState Returned => new PaymentState(7);


    public int Id { get; init; }

    private static List<PaymentState> _allStates = [Created, Commited, Rejected];

    private PaymentState(int id)
    {
        Id = id;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}