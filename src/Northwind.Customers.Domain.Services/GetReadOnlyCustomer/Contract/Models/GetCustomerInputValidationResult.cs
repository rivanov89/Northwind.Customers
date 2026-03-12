using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Contract.Models;

public class GetCustomerInputValidationResult: BaseValidationResult
{
    public GetCustomerInputValidationResult(string[] validationFailureMessages) : base(validationFailureMessages)
    {
    }
}