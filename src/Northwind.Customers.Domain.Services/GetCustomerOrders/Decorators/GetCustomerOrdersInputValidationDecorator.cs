using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using Northwind.Customers.Infrastructure.Pagination;
using Northwind.Customers.Infrastructure.Pagination.Validation;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetCustomerOrders.Decorators;

public class GetCustomerOrdersInputValidationDecorator : IQuery<GetCustomerOrdersQuery,
    OneOf<PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult,
        OrderTotalProductCountOutputValidationResult, GeneralErrorResult>>
{
    private readonly IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>,
            OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult,
            GeneralErrorResult>>
        _next;

    private readonly IPagedQueryValidator<GetCustomerOrdersInputValidationDecorator> _validator;

    public GetCustomerOrdersInputValidationDecorator(
        IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>,
            OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult,
            GeneralErrorResult>> next,
        IPagedQueryValidator<GetCustomerOrdersInputValidationDecorator> validator)
    {
        _next = next;
        _validator = validator;
    }

    public async
        Task<OneOf<PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult,
            OrderTotalProductCountOutputValidationResult, GeneralErrorResult>> ExecuteAsync(
            GetCustomerOrdersQuery query, CancellationToken token)
    {
        if (!_validator.IsValid(query, out var messages) || messages != null)
        {
            return new OrderTotalProductCountInputValidationResult(messages);
        }

        return await _next.ExecuteAsync(query, token);
    }

}