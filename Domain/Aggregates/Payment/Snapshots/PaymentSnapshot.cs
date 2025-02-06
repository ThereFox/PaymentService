using Domain.ValueObjects;

namespace Domain.Snapshots;

public record PaymentSnapshot(
    Guid Id,
    long Version,
    PaymentType Type,
    CardInfo Card,
    PaymentState State,
    List<ShiftLineItem> Items
);