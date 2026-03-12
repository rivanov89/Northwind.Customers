using Microsoft.EntityFrameworkCore;
using Northwind.Customers.Data.EntityFrameworkCore;
using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomers;

public class GetReadOnlyCustomersQueryHandler: IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult, GeneralErrorResult>>
{
    private readonly NorthwindContext _database;

    public GetReadOnlyCustomersQueryHandler(NorthwindContext database)
    {
        _database = database;
    }

    public async Task<OneOf<CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult, GeneralErrorResult>> ExecuteAsync(GetCustomersQuery query, CancellationToken token)
    {
        var queryable = _database
            .Customers
            .AsNoTracking();

        if (!string.IsNullOrEmpty(query.Name))
        {
            queryable = queryable.Where(c => c.CompanyName.Contains(query.Name));
        }

        var count = await queryable.CountAsync(token);

        var data = await queryable
            .Skip(query.PageIdx * query.PageSize)
            .Take(query.PageSize)
            .Select(c => new CustomerOrderCount
            {
                CustomerId = c.CustomerId,
                Name = c.CompanyName,
                OrderCount = c.Orders.Count
            })
            .ToArrayAsync(token);

        return new CustomersResult(count, query.PageSize, data);
    }
}