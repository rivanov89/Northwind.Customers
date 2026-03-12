using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Contract.Models;

public class GetCustomerOutputValidationResult: BaseValidationResult
{
    public GetCustomerOutputValidationResult(string[] validationFailureMessages) : base(validationFailureMessages)
    {
    }
}