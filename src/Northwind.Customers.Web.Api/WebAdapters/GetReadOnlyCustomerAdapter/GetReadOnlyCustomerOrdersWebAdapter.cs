using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using Northwind.Customers.Infrastructure.Pagination;
using Northwind.Customers.Web.Api.WebAdapters.Models;
using OneOf;

namespace Northwind.Customers.Web.Api.WebAdapters.GetReadOnlyCustomerAdapter;

public class GetReadOnlyCustomerOrdersWebAdapter : IGetReadOnlyCustomerOrdersWebAdapter
{
    private readonly IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>,
            OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult,
            GeneralErrorResult>>
        _queryHandler;

    public GetReadOnlyCustomerOrdersWebAdapter(
        IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>,
            OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult,
            GeneralErrorResult>> queryHandler)
    {
        _queryHandler = queryHandler;
    }

    public async Task<IResult> GetCustomerOrdersByIdAsync(string id, int pageIndex, int pageSize, CancellationToken token)
    {
        var result = await _queryHandler.ExecuteAsync(new GetCustomerOrdersQuery(pageSize, pageIndex, id), token);
        return result.Match(
            paginatedResult => Results.Ok(new PaginatedResponse<OrderResponse>(paginatedResult.PageCount,
                paginatedResult.Data.Select(o => new OrderResponse
                {
                    AnyProductDiscontinued = o.AnyProductDiscontinued,
                    InStockIssue = o.InStockIssue,
                    ProductCount = o.ProductCount,
                    Total = o.Total
                }).ToArray())),
            inputValidationError =>
                Results.BadRequest(inputValidationError.ValidationFailureMessages),
            outputValidationResult => Results.NotFound(outputValidationResult.ValidationFailureMessages),
            _ => Results.InternalServerError());
    }
}