using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models;

public class OrderTotalProductCountInputValidationResult: BaseValidationResult
{
    public OrderTotalProductCountInputValidationResult(IEnumerable<string> validationFailureMessages) : base(validationFailureMessages)
    {
    }
}