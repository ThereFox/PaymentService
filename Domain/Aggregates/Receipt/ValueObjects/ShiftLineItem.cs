using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class ShiftLineItem : ValueObject
{
    public Guid Id { get; protected set; }
    public string Title { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return Quantity;
    }

    public ShiftLineItem(Guid id, string title, decimal unitPrice, int quantity)
    {
        Id = id;
        Title = title;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static Result<ShiftLineItem> Create(Guid id, string title, decimal unitPrice, int quantity)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<ShiftLineItem>("Shift line item title cannot be null or empty.");
        }

        if (quantity <= 0)
        {
            return Result.Failure<ShiftLineItem>("quantity must be greater than zero.");
        }

        if (unitPrice < 0)
        {
            return Result.Failure<ShiftLineItem>(
                "position unit price cannot be negative. If it is discount is must not be like a position");
        }

        return Result.Success(new ShiftLineItem(id, title, unitPrice, quantity));
    }
}