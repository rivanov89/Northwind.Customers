using Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using Northwind.Customers.Infrastructure.Pagination.Validation;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Decorators;

public class GetReadOnlyCustomersInputValidationDecorator : IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult, GeneralErrorResult>>
{
    private readonly IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult, GeneralErrorResult>> _next;
    private readonly IPagedQueryValidator<GetReadOnlyCustomersInputValidationDecorator> _validator;

    public GetReadOnlyCustomersInputValidationDecorator(IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult, GeneralErrorResult>> next,
        IPagedQueryValidator<GetReadOnlyCustomersInputValidationDecorator> validator)
    {
        _next = next;
        _validator = validator;
    }

    public async Task<OneOf<CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult, GeneralErrorResult>> ExecuteAsync(GetCustomersQuery query, CancellationToken token)
    {
        if (!_validator.IsValid(query, out var messages))
        {
            return new GetCustomersInputValidationResult(messages);
        }

        return
            await _next
                .ExecuteAsync(query, token);
    }
}