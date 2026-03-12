using Northwind.Customers.Data.Models;

namespace Northwind.Customers.Data.Abstractions;

public interface ICustomerRepository
{
    Task<PaginatedResult<OrderTotalProductCount>> GetCustomerOrdersTotalProductCountAsync(string id, int pageIdx, int pageSize, CancellationToken token);
    Task<Customer?> GetCustomerAsync(string id, CancellationToken token);
    Task<PaginatedResult<CustomerOrderCount>> GetCustomerWithOrderCountAsync(string name, int pageIdx, int pageSize, CancellationToken token);
}