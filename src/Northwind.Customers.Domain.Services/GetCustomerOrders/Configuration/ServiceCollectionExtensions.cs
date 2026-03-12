using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models;
using Northwind.Customers.Domain.Services.GetCustomerOrders.Decorators;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using Northwind.Customers.Infrastructure.Pagination;
using Northwind.Customers.Infrastructure.Pagination.Validation;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetCustomerOrders.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGetCustomerOrders(this IServiceCollection services,
        IConfiguration configuration)
    {
        return
            services
                .Configure<PagedQueryValidationOptions<GetCustomerOrdersInputValidationDecorator>>(
                    configuration.GetSection("OrdersPagedQueryValidationOptions"))
                .AddSingleton<IPagedQueryValidator<GetCustomerOrdersInputValidationDecorator>,
                    PaginatedResultValidator<GetCustomerOrdersInputValidationDecorator>>()
                .AddSingleton<IErrorResponseFactory<OneOf<PaginatedResult<OrderTotalProductCount>,
                        OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult,
                        GeneralErrorResult>>,
                    GeneralErrorFactory<PaginatedResult<OrderTotalProductCount>,
                        OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult>>()
                .AddScoped<IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>,
                    OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult,
                    GeneralErrorResult>>, GetReadOnlyCustomerOrdersQueryHandler>()
                .Decorate<IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>,
                    OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult,
                    GeneralErrorResult>>, GetCustomerOrdersInputValidationDecorator>()
                .Decorate<IQuery<GetCustomerOrdersQuery, OneOf<PaginatedResult<OrderTotalProductCount>,
                        OrderTotalProductCountInputValidationResult, OrderTotalProductCountOutputValidationResult,
                        GeneralErrorResult>>,
                    GeneralErrorHandlingQueryDecorator<GetCustomerOrdersQuery, OneOf<
                        PaginatedResult<OrderTotalProductCount>, OrderTotalProductCountInputValidationResult,
                        OrderTotalProductCountOutputValidationResult, GeneralErrorResult>>>();
    }
}