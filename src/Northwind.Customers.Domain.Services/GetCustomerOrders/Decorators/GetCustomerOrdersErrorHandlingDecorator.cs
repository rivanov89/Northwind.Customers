using Microsoft.Extensions.Logging;
using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using Northwind.Customers.Infrastructure.Pagination;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetCustomerOrders.Decorators
{
    public class GetCustomerOrdersErrorHandlingDecorator : IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult, GeneralErrorResult>>
    {
        private readonly IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult, GeneralErrorResult>> _next;
        private readonly ILogger<GetCustomerOrdersErrorHandlingDecorator> _logger;

        public GetCustomerOrdersErrorHandlingDecorator(IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult, GeneralErrorResult>> next,
            ILogger<GetCustomerOrdersErrorHandlingDecorator> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task<OneOf<PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult, GeneralErrorResult>> ExecuteAsync(GetCustomerOrdersQuery query, CancellationToken token)
        {
            try
            {
                return await _next.ExecuteAsync(query, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting customer orders for customer {CustomerId}", query.CustomerId);
                return new GeneralErrorResult();
            }
        }
    }
}
