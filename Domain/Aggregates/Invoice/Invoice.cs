using CSharpFunctionalExtensions;
using Domain.Aggregates.Invoice.Commands;
using Domain.Aggregates.Invoice.Events;
using Domain.Events.Abstractions;
using Domain.Results;
using Domain.ValueObjects;

namespace Domain.Aggregates.Invoice;

public class Invoice
{
    private const decimal MinimalPrice = 100;

    private readonly List<DomainEvent> _events;
    private List<InvoiceLineItem> _items;
    
    public Guid? Id { get; private set; }
    public InvoiceState State { get; private set; }
    public IReadOnlyCollection<InvoiceLineItem> Items => _items;
    public IReadOnlyList<DomainEvent> Events => _events;
    
    public Guid? OrderId { get; private set; }

    public Invoice()
    {
        
    }
    
    public void Apply(InvoiceCreated @event)
    {
        Id = @event.ChangedEntityId;
        _items = @event.Items.ToList();
        OrderId = @event.OrderId;
        State = InvoiceState.Created;
        _events.Add(@event);
    }
    public void Apply(InvoiceApproved @event)
    {
        State = InvoiceState.Approved;
        _events.Add(@event);
    }
    public void Apply(InvoiceRejected @event)
    {
        State = InvoiceState.Rejected;
        _events.Add(@event);
    }

    public ResultWithEvent Process(CreateInvoice command)
    {
        if (State != InvoiceState.Initial)
        {
            return Result.Failure("Invoice is not initial").AsFailureWithoutEvent();
        }

        if (command.Items.Count == 0)
        {
            return Result.Failure("Invoice items cannot be empty").AsFailureWithoutEvent();
        }

        if (command.Items.Sum(ex => ex.Quantity * ex.DefaultUnitPrice) < MinimalPrice)
        {
            return Result.Failure("Summ of items low than minimum price").AsFailureWithoutEvent();
        }

        return Result.Success().WithEvent(
            [
                new InvoiceCreated(
                    Guid.NewGuid(),
                    command.OrderId,
                    command.Items.ToList()
                    )
            ]);
    }
    public ResultWithEvent Process(RejectInvoice command)
    {
        if (State != InvoiceState.Created)
        {
            return Result.Failure("Invoice in current state cant be rejected").AsFailureWithoutEvent();
        }

        return Result.Success().WithEvent([new InvoiceRejected(Id.Value)]);
    }
    public ResultWithEvent Process(ApproveInvoice command)
    {
        if (State != InvoiceState.Created)
        {
            return Result.Failure("Invoice in current state cant be approved").AsFailureWithoutEvent();
        }

        return Result.Success().WithEvent([new InvoiceApproved(Id.Value)]);
    }
}