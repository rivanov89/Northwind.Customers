using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Contract.Models;

public class GetCustomersOutputValidationResult : BaseValidationResult
{
    public GetCustomersOutputValidationResult(string[] validationFailureMessages) : base(validationFailureMessages)
    {
    }
}