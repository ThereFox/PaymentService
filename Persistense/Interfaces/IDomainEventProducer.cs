using CSharpFunctionalExtensions;
using Domain.Events.Abstractions;

namespace Persistense.Interfaces;

public interface IDomainEventProducer<TEvent>
    where TEvent : DomainEvent
{
    public Task<Result> Produce(TEvent domainEvent);
}