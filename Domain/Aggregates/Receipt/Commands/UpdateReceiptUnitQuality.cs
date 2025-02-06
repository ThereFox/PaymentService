namespace Domain.Aggregates.Receipt.Commands;

public record UpdateReceiptUnitQuality
(
    Guid lineId,
    int quantity
);