namespace Domain.Commands;

public record ChangePaymentAmount(
    decimal newAmount,
    string reason
);