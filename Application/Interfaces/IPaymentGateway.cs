namespace Application.Interfaces;

public interface IPaymentGateway
{
    public Task<Result> Create();
    public Task<Result> Capture();
}