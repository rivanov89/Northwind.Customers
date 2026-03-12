using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Contract.Models;

public class GetCustomersInputValidationResult: BaseValidationResult
{
    public GetCustomersInputValidationResult(IEnumerable<string> validationFailureMessages) : base(validationFailureMessages)
    {
    }
}