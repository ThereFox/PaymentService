namespace Domain.Commands;

public record ChangePaymentItems(
    Guid id,
    decimal unitPrice,
    int quantity
);