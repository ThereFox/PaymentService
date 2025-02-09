using System.Data;
using Application.Interfaces.Stores;
using CSharpFunctionalExtensions;
using Dapper;
using Domain.Aggregates.Invoice;
using Domain.Events.Abstractions;
using Domain.ValueObjects;
using Persistense.Event;
using Persistense.Interfaces;

namespace Persistense.Stores;

public class InvoiceStore : IInvoiceStore
{
    private readonly IDbConnection _connection;

    private const string GetEventsCountSQL = @"
        SELECT COUNT(*) FROM Events
        Where EntityId = @Id
    ";
    
    private const string GetEventsByIdSQL = @"
        SELECT * FROM Events
        Where EntityId = @Id
    ";
    
    public async Task<Result<Invoice>> GetById(Guid id)
    {
        //get state ...
        var events = await _connection.QueryAsync<EventDTO>(GetEventsByIdSQL, new { Id = id });
        
        var invoice = new Invoice();
        invoice.Load((dynamic)events.ToList());
        
        return invoice;
    }

    public async Task<Result> Save(Invoice invoice)
    {
        if (invoice.State == InvoiceState.Initial)
        {
            return Result.Failure("Nothing to save");
        }


        if (invoice.InitVersion == invoice.CurrentVersion)
        {
            return Result.Failure("Nothing was changed");
        }
        
        var transaction = _connection.BeginTransaction();
        
        var versionInDb = await _connection.ExecuteScalarAsync<int>(GetEventsCountSQL, new { Id = invoice.Id.Value }, transaction);

        if (invoice.InitVersion != versionInDb)
        {
            return Result.Failure("concurrency error: state was changed between changes");
        }

        var events = invoice.Events.Skip(invoice.InitVersion).ToList();

        foreach (var eventData in events)
        {
            var saveResult = await saveEvent(eventData);

            if (saveResult.IsFailure)
            {
                transaction.Rollback();
                return Result.Failure(saveResult.Error);
            }
        }
        
        transaction.Commit();
        return Result.Success();
    }

    private Task<Result> saveEvent(DomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}