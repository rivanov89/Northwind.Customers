using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Contract.Models;

public class GetCustomersQuery : BasePagedQuery
{
    public GetCustomersQuery(int pageSize, int pageIdx, string? name) : base(pageSize, pageIdx)
    {
        Name = name;
    }

    public string? Name { get; }
}