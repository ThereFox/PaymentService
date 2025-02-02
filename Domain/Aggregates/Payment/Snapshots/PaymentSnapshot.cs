using Domain.ValueObjects;

namespace Domain.Snapshots;

public record PaymentSnapshot(
    Guid Id,
    long Version,
    PaymentType Type,
    PaymentState State,
    List<ShiftLineItem> Items
);