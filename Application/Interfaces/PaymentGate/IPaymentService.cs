using CSharpFunctionalExtensions;
using Domain;

namespace Application.Interfaces.PaymentGate;

public interface IPaymentService
{
    public Task<Result> Create(Payment payment);
    public Task<Result> Capture(Payment payment, decimal amount);
    public Task<Result> Cancel(Payment payment);
    public Task<Result> GetState(Payment payment);
}