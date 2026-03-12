using Northwind.Customers.Web.DomainModels;

namespace Northwind.Customers.Web.Services.Customers;

public interface ICustomerProxy
{
    Task<CustomerPaginatedResponse> GetCustomersAsync(string? name, int pageIdx, int pageSize, CancellationToken token);
    Task<CustomerDetailDto> GetCustomerByIdAsync(string customerId, CancellationToken token);
    Task<CustomerOrdersPaginatedResponse> GetCustomerOrdersAsync(string customerId, int pageIdx, int pageSize, CancellationToken token);
}