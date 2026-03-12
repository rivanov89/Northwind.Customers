using Microsoft.Extensions.DependencyInjection;
using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Contract.Models;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Decorators;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGetCustomer(this IServiceCollection services)
    {
        return
            services
                .AddSingleton<
                    IErrorResponseFactory<OneOf<Customer, GetCustomerInputValidationResult,
                        GetCustomerOutputValidationResult, GeneralErrorResult>>, GeneralErrorFactory<Customer,
                        GetCustomerInputValidationResult, GetCustomerOutputValidationResult>>()
                .AddScoped<IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult,
                    GetCustomerOutputValidationResult, GeneralErrorResult>>, GetReadOnlyCustomerQueryHandler>()
                .Decorate<IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult,
                        GetCustomerOutputValidationResult, GeneralErrorResult>>,
                    GetReadOnlyCustomerOutputValidationDecorator>()
                .Decorate<IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult,
                        GetCustomerOutputValidationResult, GeneralErrorResult>>,
                    GetReadOnlyCustomerInputValidationDecorator>()
                .Decorate<
                    IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult,
                        GetCustomerOutputValidationResult, GeneralErrorResult>>, GeneralErrorHandlingQueryDecorator<
                        GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult,
                            GetCustomerOutputValidationResult, GeneralErrorResult>>>();
    }
}