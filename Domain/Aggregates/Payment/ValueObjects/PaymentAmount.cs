using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class PaymentAmount : ValueObject
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

    public static PaymentAmount FromLinePosition(IList<ShiftLineItem> positions)
    {
        if (positions == null || positions.Count == 0)
        {
            throw new InvalidCastException("Empty line positions");
        }

        var positionsAmountRaw = positions.Sum(ex => ex.Quantity * ex.UnitPrice);

        var targetAmount = Math.Floor(positionsAmountRaw);

        return new PaymentAmount(targetAmount);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}