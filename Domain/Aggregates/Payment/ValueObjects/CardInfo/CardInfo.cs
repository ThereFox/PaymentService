namespace Domain.ValueObjects;

public class CardInfo
{
    public PaymentSystems PaymentSystems { get; }
    public CardNumber CardNumber { get; }
    public BankIdNumber BankIdNumber { get; }
}