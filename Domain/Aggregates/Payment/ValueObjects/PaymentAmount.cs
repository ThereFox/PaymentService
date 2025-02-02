using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class PaymentAmount :ValueObject
{
    private const decimal MinimumAmount = 1;
    
    public decimal Amount { get; protected set; }

    private PaymentAmount(decimal amount)
    {
        Amount = amount;
    }

    public static Result<PaymentAmount> Create(decimal amount)
    {
        if (amount < MinimumAmount)
        {
            return Result.Failure<PaymentAmount>("Amount is invalid");
        }

        return Result.Success(new PaymentAmount(amount));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}