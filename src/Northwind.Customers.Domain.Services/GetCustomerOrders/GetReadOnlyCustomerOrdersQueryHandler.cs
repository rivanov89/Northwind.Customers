using Microsoft.EntityFrameworkCore;
using Northwind.Customers.Data.EntityFrameworkCore;
using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using Northwind.Customers.Infrastructure.Pagination;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetCustomerOrders;

public class GetReadOnlyCustomerOrdersQueryHandler : IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult, GeneralErrorResult>>
{
    private readonly NorthwindContext _database;

    public GetReadOnlyCustomerOrdersQueryHandler(NorthwindContext database)
    {
        _database = database;
    }

    public async Task<OneOf<PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult, GeneralErrorResult>> ExecuteAsync(GetCustomerOrdersQuery query, CancellationToken token)
    {
        var pageSize = query.PageSize;
        var queryable = _database.Orders
            .AsNoTracking()
            .Where(order => order.CustomerId == query.CustomerId);
        var count = await queryable.CountAsync(token);
        var data = await queryable
            .Skip(query.PageIdx * pageSize)
            .Take(pageSize)
            .Select(order => new OrderTotalProductCount
            {
                CustomerId = order.CustomerId,
                ProductCount = order.OrderDetails.Sum(detail => detail.Quantity),
                Total = order.OrderDetails.Sum(detail =>
                    detail.Quantity * detail.UnitPrice * (1 - (decimal)detail.Discount)) + order.Freight ?? 0,
                AnyProductDiscontinued = order.OrderDetails.Any(detail => detail.Product.Discontinued),
                InStockIssue = order.OrderDetails.Any(detail => detail.Quantity > detail.Product.UnitsInStock)
            })
            .ToArrayAsync(token);
        return new PaginatedResult<OrderTotalProductCount>(count, pageSize, data);
    }
}
