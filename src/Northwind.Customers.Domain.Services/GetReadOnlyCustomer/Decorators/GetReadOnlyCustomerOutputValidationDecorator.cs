using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Decorators;

public class GetReadOnlyCustomerOutputValidationDecorator : IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>>
{
    private readonly IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> _next;

    public GetReadOnlyCustomerOutputValidationDecorator(IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> next)
    {
        _next = next;
    }

    public async Task<OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> ExecuteAsync(GetCustomerQuery query, CancellationToken token)
    {
        var customerResult = await _next.ExecuteAsync(query, token);

        return customerResult
            .Match<OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult,
                GeneralErrorResult>>(
                customer =>
                {
                    if (customer is null)
                    {
                        return new GetCustomerOutputValidationResult(["Customer not found."]);
                    }

                    return customer;
                },
                inputValidationError => inputValidationError,
                outputValidationError => outputValidationError,
                generalError => generalError);
    }
}