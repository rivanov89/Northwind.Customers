using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Decorators;

public class GetReadOnlyCustomerInputValidationDecorator : IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>>
{
    private readonly IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> _next;

    public GetReadOnlyCustomerInputValidationDecorator(IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> next)
    {
        _next = next;
    }

    public async Task<OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> ExecuteAsync(GetCustomerQuery query, CancellationToken token)
    {
        if (string.IsNullOrEmpty(query.CustomerId))
        {
            //TODO: Optimized this with single valued optimized collection.
            return new GetCustomerInputValidationResult(["CustomerId is required."]);
        }

        return
            await _next
                .ExecuteAsync(query, token);
    }
}