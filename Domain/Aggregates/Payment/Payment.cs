using CSharpFunctionalExtensions;
using Domain.Commands;
using Domain.Events.Abstractions;
using Domain.Events.Realisations;
using Domain.Results;
using Domain.Snapshots;
using Domain.ValueObjects;

namespace Domain;

public class Payment
{
    private readonly List<PaymentEvent> _events = new();

    public Guid? Id { get; private set; }

    public PaymentAmount Amount { get; private set; }

    public long InitVersion { get; protected set; } = -1;
    public IReadOnlyList<PaymentEvent> Events => _events;

    public PaymentState State { get; protected set; } = PaymentState.Initial;
    public PaymentType PaymentType { get; protected set; }

    public CardInfo CardForCredit { get; protected set; }

    public Payment()
    {
    }

    public Payment(PaymentSnapshot snapshot)
    {
        Id = snapshot.Id;
        InitVersion = snapshot.Version;
        CardForCredit = snapshot.Card;
        PaymentType = snapshot.Type;
        State = snapshot.State;
    }

    public void Apply(PaymentCreated @event)
    {
        State = PaymentState.Created;
        PaymentType = @event.Type;
        CardForCredit = @event.Card;
        _events.Add(@event);
    }

    public void Apply(PaymentInitialised @event)
    {
        State = PaymentState.Wait_Pending;
        _events.Add(@event);
    }

    public void Apply(PaymentVerifyed @event)
    {
        if (PaymentType == PaymentType.Captured)
        {
            State = PaymentState.Commited;
        }
        else
        {
            State = PaymentState.Wait_Capture;
        }

        _events.Add(@event);
    }

    public void Apply(PaymentCanceled @event)
    {
        State = PaymentState.Canceled;
        _events.Add(@event);
    }

    public void Apply(PaymentCaptured @event)
    {
        State = PaymentState.Commited;
        Amount = PaymentAmount.Create(@event.Amount).Value;
        _events.Add(@event);
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

    public ResultWithEvent Process(CreateTwoFasePayment command)
    {
        if (State != PaymentState.Initial)
        {
            return Result.Failure("Is not initial state for create payment").AsFailureWithoutEvent();
        }

        var validateAmount = PaymentAmount.Create(command.DefaultAmount);

        if (validateAmount.IsFailure)
        {
            return Result.Failure(validateAmount.Error).AsFailureWithoutEvent();
        }

        var createdEvent = new PaymentCreated(Guid.NewGuid(), PaymentType.TwoState, validateAmount.Value);

        return Result.Success().WithEvent([createdEvent]);
    }

    public ResultWithEvent Process(InitialisePayment command)
    {
        if (State == PaymentState.Initial)
        {
            return Result.Failure("Payment an't created").AsFailureWithoutEvent();
        }

        if (State != PaymentState.Created)
        {
            return Result.Failure("payment already initialised").AsFailureWithoutEvent();
        }

        var eventData = new PaymentInitialised(command.OrderId, Id.Value);

        return Result.Success().WithEvent([eventData]);
    }

    public ResultWithEvent Process(CancelPayment command)
    {
        if (State != PaymentState.Wait_Pending)
        {
            return Result.Failure("Payment dont wait pending").AsFailureWithoutEvent();
        }

        var eventData = new PaymentCanceled(Id.Value);

        return Result.Success().WithEvent([eventData]);
    }

    public ResultWithEvent Process(VerifyPayment command)
    {
        if (State != PaymentState.Wait_Pending)
        {
            return Result.Failure("Payment dont wait pending").AsFailureWithoutEvent();
        }

        var eventData = new PaymentVerifyed(Id.Value);

        return Result.Success().WithEvent([eventData]);
    }

    public ResultWithEvent Process(CapturePayment command)
    {
        if (State != PaymentState.Wait_Capture)
        {
            return Result.Failure("Payment dont wait capture").AsFailureWithoutEvent();
        }

        var eventData = new PaymentCaptured(Id.Value, command.Amount);

        return Result.Success().WithEvent([eventData]);
    }
}