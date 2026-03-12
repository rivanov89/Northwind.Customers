namespace Northwind.Customers.Infrastructure.Abstractions
{
    public interface IQuery<in TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest query, CancellationToken token);
    }
}
