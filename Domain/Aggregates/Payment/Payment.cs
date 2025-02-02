using CSharpFunctionalExtensions;
using Domain.Commands;
using Domain.Events.Abstractions;
using Domain.Events.Realisations;
using Domain.Results;
using Domain.ValueObjects;

namespace Domain;

public class Payment
{
    private readonly List<PaymentEvent> _events = new();
    
    public Guid? Id { get; private set; }
    
    public long Version { get; protected set; } = 0;
    public IReadOnlyList<PaymentEvent> Events => _events;
    
    public PaymentState State { get; protected set; } = PaymentState.Initial;
    public PaymentType PaymentType { get; protected set; }
    public PaymentAmount Amount { get; protected set; }

    public Payment()
    {
        
    }

    public void Apply(PaymentCreated @event)
    {
        State = PaymentState.Created;
        PaymentType = @event.Type;
        Amount = @event.Amount;
    }
    
    public ResultWithEvent Process(CreateImmediantlyPayment command)
    {
        if (State != PaymentState.Initial)
        {
            return Result.Failure("Is not initial state for create payment").AsFailureWithoutEvent();
        }
        
        var validateAmount = PaymentAmount.Create(command.Amount);

        if (validateAmount.IsFailure)
        {
            return Result.Failure(validateAmount.Error).AsFailureWithoutEvent();
        }
        
        var createdEvent = new PaymentCreated(Guid.NewGuid(), PaymentType.Captured, validateAmount.Value);
        
        return Result.Success().WithEvent([createdEvent]);
    }
    
    
    
}