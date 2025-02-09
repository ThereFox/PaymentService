using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class CardNumber : ValueObject
{
    public string Number { get; }


    private CardNumber(string number)
    {
        Number = number;
    }

    private static Result<CardNumber> Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            return Result.Failure<CardNumber>("Card number cannot be empty.");
        }

        if (number.Contains(' '))
        {
            return Result.Failure<CardNumber>("Card number cannot contain spaces.");
        }

        if (number.Length != 16)
        {
            return Result.Failure<CardNumber>("Card number must be exactly 16 characters.");
        }

        if (checkNumberValid(number) == false)
        {
            return Result.Failure<CardNumber>("Card number is not valid.");
        }

        return Result.Success(new CardNumber(number));
    }

    private static bool checkNumberValid(string number)
    {
        var lastNumber = number[15];

        var evenSum = 0;
        var unevenSum = 0;

        for (int i = 0; i < 15; i++)
        {
            if ((i + 1) % 2 == 0)
            {
                evenSum += int.Parse(number[i].ToString());
            }
            else
            {
                unevenSum += int.Parse(number[i].ToString());
            }
        }

        var convertedEven = evenSum * 2 > 9 ? evenSum * 2 - 9 : evenSum * 2;

        var controlSum = unevenSum + convertedEven;

        var lastNunmberOfControlSumm = controlSum % 10;

        var avaitLastNumber = lastNunmberOfControlSumm == 0 ? 0 : 10 - lastNunmberOfControlSumm;

        return avaitLastNumber == lastNumber;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}