namespace Domain.Aggregates.Receipt.Commands;

public record UpdateReceiptUnitPrice(
    Guid lineId,
    int unitPrice
);