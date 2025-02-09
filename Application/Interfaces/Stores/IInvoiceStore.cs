using CSharpFunctionalExtensions;
using Domain.Aggregates.Invoice;

namespace Application.Interfaces.Stores;

public interface IInvoiceStore
{
    public Task<Result<Invoice>> GetById(Guid id);
    public Task<Result> Save(Invoice invoice);
}