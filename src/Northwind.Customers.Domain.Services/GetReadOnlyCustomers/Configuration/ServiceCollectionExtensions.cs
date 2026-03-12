using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Contract.Models;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Decorators;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using Northwind.Customers.Infrastructure.Pagination;
using Northwind.Customers.Infrastructure.Pagination.Validation;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGetCustomers(this IServiceCollection services, IConfiguration configuration)
        {
            return
                services
                    .Configure<PagedQueryValidationOptions<GetReadOnlyCustomersInputValidationDecorator>>(
                        configuration.GetSection("CustomersPagedQueryValidationOptions"))
                    .AddSingleton<IPagedQueryValidator<GetReadOnlyCustomersInputValidationDecorator>,
                        PaginatedResultValidator<GetReadOnlyCustomersInputValidationDecorator>>()
                    .AddSingleton<
                        IErrorResponseFactory<OneOf<CustomersResult, GetCustomersInputValidationResult,
                            GetCustomersOutputValidationResult, GeneralErrorResult>>, GeneralErrorFactory<
                            CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult>>()
                    .AddScoped<IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult,
                        GetCustomersOutputValidationResult, GeneralErrorResult>>, GetReadOnlyCustomersQueryHandler>()
                    .Decorate<IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult,
                            GetCustomersOutputValidationResult, GeneralErrorResult>>,
                        GetReadOnlyCustomersInputValidationDecorator>()
                    .Decorate<IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult,
                            GetCustomersOutputValidationResult, GeneralErrorResult>>,
                        GeneralErrorHandlingQueryDecorator<GetCustomersQuery, OneOf<CustomersResult,
                            GetCustomersInputValidationResult, GetCustomersOutputValidationResult,
                            GeneralErrorResult>>>();
        }
    }
}
