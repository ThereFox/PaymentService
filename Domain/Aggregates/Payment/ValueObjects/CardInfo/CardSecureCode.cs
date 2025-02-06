using CSharpFunctionalExtensions;

namespace Domain.ValueObjects;

public class CardSecureCode
{
    public string Code { get; }

    private CardSecureCode(string code)
    {
        Code = code;
    }
    
    public static Result<CardSecureCode> Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return Result.Failure<CardSecureCode>("Input are required");
        }

        if (code.Length != 3)
        {
            return Result.Failure<CardSecureCode>("Input have invalid length");
        }

        return Result.Success(new CardSecureCode(code));
    }
}