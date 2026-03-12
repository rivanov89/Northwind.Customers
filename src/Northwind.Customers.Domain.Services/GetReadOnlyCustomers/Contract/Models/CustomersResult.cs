using Northwind.Customers.Data.Models;
using Northwind.Customers.Infrastructure.Pagination;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Contract.Models;

public class CustomersResult(int totalDataCount, int pageSize, CustomerOrderCount[] data) : PaginatedResult<CustomerOrderCount>(totalDataCount, pageSize, data)
{
}